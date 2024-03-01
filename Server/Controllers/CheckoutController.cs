using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Server.Data.Repositories.Interfaces;
using Shared.Models;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;
using Customer = Shared.Models.Customer;
using Order = Shared.Models.Order;
using Product = Shared.Models.Product;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class CheckoutController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;

    private static string s_wasmClientURL = string.Empty;

    public CheckoutController(IConfiguration configuration,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository)
    {
        _configuration = configuration;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    [HttpPost]
    public async Task<ActionResult> CheckoutOrder([FromBody] Product product, [FromServices] IServiceProvider sp)
    {
        var referer = Request.Headers.Referer;
        s_wasmClientURL = referer[0];

        var server = sp.GetRequiredService<IServer>();

        var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

        string? thisApiUrl = null;

        if (serverAddressesFeature is not null)
        {
            thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
        }

        if (thisApiUrl is not null)
        {
            var sessionId = await CheckOut(product, thisApiUrl);
            var pubKey = _configuration["Stripe:PubKey"];

            var checkoutOrderResponse = new CheckoutOrderResponse()
            {
                SessionId = sessionId,
                PubKey = pubKey
            };

            return Ok(checkoutOrderResponse);
        }
        else
        {
            return StatusCode(500);
        }
    }

    [NonAction]
    public async Task<string> CheckOut(Product product, string thisApiUrl)
    {
        var options = new SessionCreateOptions
        {
            SuccessUrl = $"{thisApiUrl}/checkout/success?sessionId=" + "{CHECKOUT_SESSION_ID}",
            CancelUrl = s_wasmClientURL + "failed",
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = product.Price,
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Title,
                            Description = product.Description,
                            Images = new List<string> { product.ImageUrl }
                        },
                    },
                    Quantity = 1
                },
            },
            Mode = "payment",
            Metadata = new Dictionary<string, string> { { "product_id", $"{product.Id}"} }
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);


        return session.Id;
    }

    [HttpGet("success")]
    public ActionResult CheckoutSuccess(string sessionId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);

        var total = session.AmountTotal.Value;
        var customerEmail = session.CustomerDetails.Email;

        return Redirect(s_wasmClientURL + "success");
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> Webhook()
    {
        
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        string endpointSecret = _configuration["Stripe:WHSecret"];
        try
        {
            var stripeEvent = EventUtility.ParseEvent(json);
            var signatureHeader = Request.Headers["Stripe-Signature"];

            stripeEvent = EventUtility.ConstructEvent(json,
                    signatureHeader, endpointSecret);

            if (stripeEvent.Type == Events.ChargeSucceeded)
            {
                var options = new Stripe.Checkout.SessionListOptions { Limit = 1 };
                var service = new Stripe.Checkout.SessionService();
                StripeList<Stripe.Checkout.Session> sessions = service.List(options);

                var session = sessions.FirstOrDefault();

                var sessionData = await service.GetAsync(session.Id);

                var charge = stripeEvent.Data.Object as Charge;

                var customer = new Customer
                {
                    FullName = charge.BillingDetails.Name,
                    Email = charge.BillingDetails.Email
                };

                await _customerRepository.CreateCustomer(customer);

                var order = new Order
                {
                    CustomerId = customer.Id,
                    ProductId = sessionData.Metadata["product_id"]
                };

                await _orderRepository.AddOrder(order);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            Console.WriteLine("Error: {0}", e.Message);
            return BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
    }
}


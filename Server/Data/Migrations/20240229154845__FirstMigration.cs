using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class _FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "OrderId", "Price", "Title" },
                values: new object[,]
                {
                    { "06f89d01-ddac-4521-9809-da230145cc78", "Product 19 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/19/500", null, 19000L, "Product 19" },
                    { "0ea9ed73-b665-4d66-9112-ff96bb695e32", "Product 13 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/13/500", null, 13000L, "Product 13" },
                    { "162ea4a5-f486-42cf-ad9c-03e4a3120662", "Product 9 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/9/500", null, 9000L, "Product 9" },
                    { "237eb942-da2d-45ab-bc35-9688e002cb2e", "Product 7 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/7/500", null, 7000L, "Product 7" },
                    { "25e17b63-99ba-4f14-b450-df123f772ab4", "Product 12 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/12/500", null, 12000L, "Product 12" },
                    { "26d6bf96-df92-4c87-a807-251b7a7b2505", "Product 3 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/3/500", null, 3000L, "Product 3" },
                    { "4b408c2a-967d-4308-b7a4-5cdff94deac7", "Product 10 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/10/500", null, 10000L, "Product 10" },
                    { "53fdffd5-e121-4242-9e36-47d28351e142", "Product 16 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/16/500", null, 16000L, "Product 16" },
                    { "54530ee8-56bd-443f-b708-ffdafcd8b2ef", "Product 6 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/6/500", null, 6000L, "Product 6" },
                    { "75efbcf7-f8f2-4ce2-8b46-5f2e62b1326b", "Product 5 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/5/500", null, 5000L, "Product 5" },
                    { "a2c0c32a-074a-4df7-a7cf-0187ec66d21a", "Product 17 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/17/500", null, 17000L, "Product 17" },
                    { "b5b96782-dbfc-4f8f-92d8-4f042311748d", "Product 20 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/20/500", null, 20000L, "Product 20" },
                    { "bc61ca26-012f-41f6-a8a3-a43954d3834c", "Product 14 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/14/500", null, 14000L, "Product 14" },
                    { "c2aa3e79-1483-41ec-ad28-a926b2c3ece2", "Product 8 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/8/500", null, 8000L, "Product 8" },
                    { "cd8066d1-dafd-4154-b929-94b515ca257e", "Product 2 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/2/500", null, 2000L, "Product 2" },
                    { "cea9a5bc-67d0-46c2-90a9-ec3097dfb46e", "Product 18 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/18/500", null, 18000L, "Product 18" },
                    { "daf2011a-00cd-4e5d-a315-7df66334bdd0", "Product 11 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/11/500", null, 11000L, "Product 11" },
                    { "e2c5ca0f-b9dd-45da-8f8b-c70bd60e2e94", "Product 15 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/15/500", null, 15000L, "Product 15" },
                    { "e5d37007-fd54-4dd0-ad6d-ea2ec08f737a", "Product 1 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/1/500", null, 1000L, "Product 1" },
                    { "f01116e0-ba0c-459d-9329-434c45290077", "Product 4 description. This is an amazing product with a price-quality balance you won't find anywhere ele.", "https://picsum.photos/id/4/500", null, 4000L, "Product 4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

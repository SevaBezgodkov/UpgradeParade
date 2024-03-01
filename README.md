Для теста необходимо развернуть бд и запустить оба проекта - Client, Server

Так как stripe оплата находиться в test mode необходимо запустить webhook ->
stripe listen --forward-to https://localhost:7065/checkout/webhook

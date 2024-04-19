using MiddlewareTest.Models;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseToken("12345678"); //იხილეთ TokenMiddleware და TokenExtensions კლასები

//როცა გაუშვებთ პროგრამას URL ში დაუმატეთ /?token=12345678 (ამ შემთხვევაში)

app.Run(async (context) => await context.Response.WriteAsync("Hello!"));

app.Run();
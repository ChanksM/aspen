using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text.Unicode;
using System.Security.Cryptography;
using MiddlewareComponents.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    // Adding data to HttpContext.Items
    context.Items["Message"] = "Hello from";

    // Pass the request to the next middleware
    await next.Invoke();
});

app.Use(async (context, next) =>
{
    // Adding data to HttpContext.Items
    context.Items["Message"] = context.Items["Message"]+"  middleware!";

    // Pass the request to the next middleware
    await next.Invoke();
});


app.Run();


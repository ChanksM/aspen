using Microsoft.EntityFrameworkCore;
using MVCApi.Models;
using MVCApi.Models.Data;

var builder = WebApplication.CreateBuilder();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/", async (HttpContext context) =>
{
    await context.Response.WriteAsync(File.ReadAllText(Path.Combine(builder.Environment.ContentRootPath, "Views", "Home", "Index.cshtml")));
});

app.MapGet("/api/users", async (ApplicationContext _db) => await _db.Persons.ToListAsync());

app.MapGet("/api/users/{id}", (string id, ApplicationContext _db) =>
{
    
    Person? user = _db.Persons.FirstOrDefault(u => u.Id == id);
    
    if (user == null) return Results.NotFound(new { message = "User not found" });

    
    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", async (string id, ApplicationContext _db) =>
{
    
    Person? user = _db.Persons.FirstOrDefault(u => u.Id == id);

    
    if (user == null) return Results.NotFound(new { message = "User not found" });

    _db.Persons.Remove(user);
    await  _db.SaveChangesAsync();
    return Results.Json(user);
});

app.MapPost("/api/users", async (Person user, ApplicationContext _db) => {

 
    user.Id = Guid.NewGuid().ToString();

    _db.Persons.Add(user);
    await _db.SaveChangesAsync();
    return user;
});

app.MapPut("/api/users", async (Person userData, ApplicationContext _db) => {

    
    var user = _db.Persons.FirstOrDefault(u => u.Id == userData.Id);
    
    if (user == null) return Results.NotFound(new { message = "User not found" });
   

    user.Age = userData.Age;
    user.Name = userData.Name;
    await _db.SaveChangesAsync();
    return Results.Json(user);
});

app.Run();


List<Person> users = new List<Person>
{
    new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41 },
    new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24 }
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/", async (HttpContext context) =>
{
    await context.Response.WriteAsync(File.ReadAllText(Path.Combine(builder.Environment.ContentRootPath, "Views", "Home", "Index.cshtml")));
});

app.MapGet("/api/users", () => users);

app.MapGet("/api/users/{id}", (string id) =>
{
    // Retrieve the user by id
    Person? user = users.FirstOrDefault(u => u.Id == id);
    // If not found, return a status code and an error message
    if (user == null) return Results.NotFound(new { message = "User not found" });

    // If user found, return the user
    return Results.Json(user);
});

app.MapDelete("/api/users/{id}", (string id) =>
{
    // Retrieve the user by id
    Person? user = users.FirstOrDefault(u => u.Id == id);

    // If not found, return a status code and an error message
    if (user == null) return Results.NotFound(new { message = "User not found" });

    // If user found, remove it
    users.Remove(user);
    return Results.Json(user);
});

app.MapPost("/api/users", (Person user) => {

    // Set id for the new user
    user.Id = Guid.NewGuid().ToString();
    // Add the user to the list
    users.Add(user);
    return user;
});

app.MapPut("/api/users", (Person userData) => {

    // Retrieve the user by id
    var user = users.FirstOrDefault(u => u.Id == userData.Id);
    // If not found, return a status code and an error message
    if (user == null) return Results.NotFound(new { message = "User not found" });
    // If user found, update its data and send it back to the client

    user.Age = userData.Age;
    user.Name = userData.Name;
    return Results.Json(user);
});

app.Run();

public class Person
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

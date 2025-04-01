using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Server=postgres;Port=5432;Database=blogs;User Id=admin;Password=example;";

builder.Services.AddDbContextPool<BloggingContext>(opt =>
    opt.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGet("/", (BloggingContext context) =>
{
  var result = context.Blogs.First();
  return Results.Ok(new { content = result });
});

app.Run();

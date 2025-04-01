using Microsoft.EntityFrameworkCore;
using QuoteService.Data;
using QuoteService.Models;

var envConnectionStringKey = "DB_CONNECTION_STRING";
var connectionString = Environment.GetEnvironmentVariable(envConnectionStringKey);
if (connectionString == null)
{
  Console.WriteLine($"Environment Variable undefined! Using empty string. Key: {envConnectionStringKey}");
  connectionString = "";
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseNpgsql(connectionString));

var unrestrictedCors = "UnrestricedOrigins";
builder.Services.AddCors(options =>
  options.AddPolicy(name: unrestrictedCors, policy => policy
    .WithOrigins("*") // Tells browser from any domains it's okay
    .AllowAnyMethod() // to perform all HTTP methods
    .AllowAnyHeader()) // with any headers
);

var app = builder.Build();

// Apply migration on each startup. Only for local development!
// More controlled application is required in production
using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  dbContext.Database.Migrate();
}

app.MapGet("/quotes", async (AppDbContext context) =>
{
  return await context.Quotes.ToListAsync();
});

app.MapPost("/quotes", (AppDbContext context, Quote newQuote) =>
{
  context.Quotes.Add(newQuote);
  context.SaveChanges();
  return Results.Created();
});

app.UseCors(unrestrictedCors);

app.Run();

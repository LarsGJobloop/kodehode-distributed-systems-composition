using Microsoft.EntityFrameworkCore;
using QuoteService.Data;
using QuoteService.Models;

var envConnectionStringKey = "DB_CONNECTION_STRING";
var connectionString = Environment.GetEnvironmentVariable(envConnectionStringKey) ??
  throw new ApplicationException($"Environment Variable undefined! Key: {envConnectionStringKey}");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

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

app.Run();

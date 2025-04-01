using Microsoft.EntityFrameworkCore;
using QuoteService.Models;

namespace QuoteService.Data;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<Quote> Quotes => Set<Quote>();
}

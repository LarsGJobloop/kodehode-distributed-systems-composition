
using Microsoft.EntityFrameworkCore;

public class BloggingContext(DbContextOptions<BloggingContext> options) : DbContext(options)
{
  public DbSet<Models.Blog> Blogs { get; set; }
  public DbSet<Models.Post> Posts { get; set; }
}
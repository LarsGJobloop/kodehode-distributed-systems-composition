namespace QuoteService.Models;

public class Quote
{
  public int Id { get; set; }
  public required string Content { get; set; }
  public required string Author { get; set; } = "Anonymous";
}

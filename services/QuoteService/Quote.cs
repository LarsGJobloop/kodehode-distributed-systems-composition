namespace QuoteService.Models;

public class Quote
{
  public int Id { get; set; }
  public string Content { get; init; }
  public string Author { get; init; }

  public Quote(string content, string? author = null)
  {
    if (string.IsNullOrWhiteSpace(content))
      throw new ArgumentException("Content cannot be empty.", nameof(content));

    Content = content;
    Author = string.IsNullOrWhiteSpace(author) ? "Anonymous" : author;
  }
}

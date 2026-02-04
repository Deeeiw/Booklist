namespace booklist_test.Models;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public int Pages { get; set; }
    public string? Status { get; set; }

    public Book(string title, string author, int pages, string status)
    {
        Title = title;
        Author = author;
        Pages = pages;
        Status = status;
    }
}
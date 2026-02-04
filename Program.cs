
using booklist_test;
    
var program = new Booklist();
program.StartPage();

class Booklist : BookContext
{
    public void StartPage()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the BOOKLIST book tracker program!\n" +
                          "What would you like to do?\n");

        DisplayBooks();
        
        Console.WriteLine("1. Add a new position\n" +
                          "2. Edit a position\n" +
                          "3. Remove a position\n" +
                          "4. Quit\n");

        var choice = Console.ReadLine();
        using var context = new BookContext();
        switch (choice)
        {
            case "1":
                AddBook();
                break;
            case "2":
                EditBook();
                break;
            case "3":
                RemoveBook();
                break;
            case "4":
                return;
            default:
                Console.Error.WriteLine("Invalid choice. Please choose only available options!");
                StartPage();
                break;
        }
    }

    private void DisplayBooks()
    {
        foreach (Book book in Books)
        {
            Console.WriteLine($"{Books.IndexOf(book) + 1} {book.Title} {book.Author} - {book.Pages} pages - Status: {book.Status}");
        }
    }

    private void EditBook()
    {
        Console.WriteLine("Enter an index of the book you want to edit: ");
        var index = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Which information would you like to edit?\n" +
                          "1. Title\n" +
                          "2. Author\n" +
                          "3. Pages\n" +
                          "4. Status\n" +
                          "5. Cancel edition");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine("Enter new title: ");
                var newtitle =  Console.ReadLine();
                Books[index - 1].Title = newtitle;
                StartPage();
                break;
            case "2":
                Console.WriteLine("Enter new author: ");
                var newauthor = Console.ReadLine();
                Books[index - 1].Author = newauthor;
                StartPage();
                break;
            case "3":
                Console.WriteLine("Enter new pages: ");
                var newpages = Convert.ToInt32(Console.ReadLine());
                Books[index - 1].Pages = newpages;
                StartPage();
                break;
            case "4":
                Console.WriteLine("Enter new status: ");
                var newstatus = Console.ReadLine();
                Books[index - 1].Status = newstatus;
                StartPage();
                break;
            case "5":
                StartPage();
                break;
            default:
                Console.Error.WriteLine("Invalid choice. Please choose only available options!");
                StartPage();
                break;
        }
    }
    
    private void RemoveBook()
    {
        Console.WriteLine("Enter an index of the book you want to remove: ");
        var index = Convert.ToInt32(Console.ReadLine());
        Books.RemoveAt(index - 1);
        StartPage();
    }
    
    private void AddBook()
    {
        Console.WriteLine("Enter title: ");
        var title =  Console.ReadLine();
        
        Console.WriteLine("Enter author: ");
        var author = Console.ReadLine();
        
        Console.WriteLine("Enter pages: ");
        int pages = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Enter status: ");
        var status  = Console.ReadLine();
        
        var book = new Book(title, author, pages, isRead: status);
        Books.Add(book);
        
        Console.WriteLine("Book added successfully!");
        StartPage();
    }
}

public class Book
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public int Pages { get; set; }
    public bool IsRead { get; set; }

    public Book(string title, string author, int pages, bool isRead)
    {
        Title = title;
        Author = author;
        Pages = pages;
        IsRead = isRead;
    }
}
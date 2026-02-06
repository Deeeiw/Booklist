using Microsoft.EntityFrameworkCore;
using booklist_test.Models;
using booklist_test.Data;

var program = new Booklist();
program.StartPage();

class Booklist
{
    public void StartPage()
    {
        using (var context = new BookContext())
        {
            context.Database.Migrate();
        }

        Console.Clear();
        Console.WriteLine("Welcome to the BOOKLIST book tracker program!\n" +
                          "What would you like to do?\n");

        DisplayBooks();
        
        Console.WriteLine("1. Add a new position\n" +
                          "2. Edit a position\n" +
                          "3. Remove a position\n" +
                          "4. Quit\n");

        var choice = Console.ReadLine();
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
        using var context = new BookContext();
        
        var books =  context.Books.ToList();
        if (!books.Any())
        {
            Console.WriteLine("No books are being tracked.");
        }
        
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id} {book.Title} {book.Author} - {book.Pages} pages - Status: {book.Status}");
        }
        Console.WriteLine();
    }

    private void EditBook()
    {
        using var context = new BookContext();

        Console.WriteLine("Enter the ID of the book you want to edit:");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var book = context.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        Console.WriteLine(
            "Which information would you like to edit?\n" +
            "1. Title\n" +
            "2. Author\n" +
            "3. Pages\n" +
            "4. Status\n" +
            "5. Cancel edition");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("Enter new title:");
                book.Title = Console.ReadLine();
                break;
            case "2":
                Console.WriteLine("Enter new author:");
                book.Author = Console.ReadLine();
                break;
            case "3":
                Console.WriteLine("Enter new pages:");
                if (int.TryParse(Console.ReadLine(), out int pages))
                    book.Pages = pages;
                break;
            case "4":
                Console.WriteLine("Enter new status:");
                book.Status = Console.ReadLine();
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        context.SaveChanges();
        Console.WriteLine("Book updated successfully.");
        
        StartPage();
    }

    
    private void RemoveBook()
    {
        using var context = new BookContext();
        Console.WriteLine("Enter the ID of the book you want to edit:");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }
        var book = context.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        context.Remove(book);
        context.SaveChanges();
        
        StartPage();
    }
    
    private void AddBook()
    {
        using var context = new BookContext();
        
        Console.WriteLine("Enter title: ");
        var title =  Console.ReadLine();
        
        Console.WriteLine("Enter author: ");
        var author = Console.ReadLine();
        
        Console.WriteLine("Enter pages: ");
        int pages = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Enter status: ");
        var status  = Console.ReadLine();
        
        context.Add(new Book(title, author, pages, status));
        context.SaveChanges();
        
        Console.WriteLine("Book added successfully!");
        StartPage();
    }
}
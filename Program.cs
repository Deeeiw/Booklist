using Microsoft.EntityFrameworkCore;
using booklist_test.Models;
using booklist_test.Data;

var program = new Booklist();
program.StartPage();

class Booklist
{
    private void CheckAuthor(string? input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            throw new EmptyInputException("Author cannot be empty!");
        }
        
        if (input.Length > 50)
        {
            throw new Exception("The length of the author must be less than 50 characters!");
        }
    }

    private void CheckTitle(string? input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            throw new EmptyInputException();
        }
        
        if (input.Length > 80)
        {
            throw new Exception("The length of the title must be less than 80 characters!");
        }
    }

    private void CheckStatus(string? input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            throw new EmptyInputException("Status cannot be empty!");
        }
        
        if (input.Length > 14)
        {
            throw new Exception("The length of status must be less than 14 characters!");
        }
    }
    
    public void StartPage()
    {
        using (var context = new BookContext())
        {
            context.Database.Migrate();
        }
        
        Console.WriteLine();
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
                StartPage();
                break;
        }
    }

    private void DisplayBooks()
    {
        using var context = new BookContext();
        
        var books =  context.Books.OrderBy(b => b.Id).ToList();
        
        if (!books.Any())
        {
            Console.WriteLine("No books are being tracked.");
        }
        
        for(int i = 0; books.Count > i; i++)
        {
            Console.WriteLine($"{i + 1} {books[i].Title} | {books[i].Author} - {books[i].Pages} pages - Status: {books[i].Status}");
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
            "5. Cancel");

        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter new title:");
                    string? newTitle = Console.ReadLine();
                    CheckTitle(newTitle);
                    book.Title = newTitle;
                    break;
                case "2":
                    Console.WriteLine("Enter new author:");
                    string? newAuthor = Console.ReadLine();
                    CheckAuthor(newAuthor);
                    book.Author = newAuthor;
                    break;
                case "3":
                    Console.WriteLine("Enter new pages:");
                    if (int.TryParse(Console.ReadLine(), out int pages))
                        book.Pages = pages;
                    break;
                case "4":
                    Console.WriteLine("Enter new status:");
                    string? newStatus = Console.ReadLine();
                    CheckStatus(newStatus);
                    book.Status = newStatus;
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            
            context.SaveChanges();
            Console.WriteLine("Book updated successfully.");
        }
        catch (EmptyInputException e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        catch (FormatException)
        {
            Console.WriteLine("The number of pages has to be a whole number!");
            return;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        
        StartPage();
    }
    
    private void RemoveBook()
    {
        using var context = new BookContext();
    
        Console.Clear();
        var books = context.Books.OrderBy(b => b.Id).ToList();
        
        if (books.Count == 0)
        {
            Console.WriteLine("No books to delete.");
            return;
        }
        
        for(int i = 0; books.Count > i; i++)
        {
            Console.WriteLine($"{i + 1} {books[i].Title} | {books[i].Author} - {books[i].Pages} pages - Status: {books[i].Status}");
        }
        
        Console.WriteLine("Enter the ID of the book you want to delete:");
        
        if (!int.TryParse(Console.ReadLine(), out int position) || position < 1 || position > books.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        context.Books.Remove(books[position - 1]);
        context.SaveChanges();
        
        StartPage();
    }
    
    private void AddBook()
    {
        using var context = new BookContext();

        try
        {
            Console.WriteLine("Enter title: ");
            var title = Console.ReadLine();
            CheckTitle(title);

            Console.WriteLine("Enter author: ");
            var author = Console.ReadLine();
            CheckAuthor(author);

            Console.WriteLine("Enter pages: ");
            int pages = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter status: (Completed | Reading | Plan to read)");
            var status = Console.ReadLine();
            CheckStatus(status);

            context.Add(new Book(title, author, pages, status));
            context.SaveChanges();
        }
        catch (EmptyInputException e)
        {
            Console.WriteLine(e.Message);
            AddBook();
        }
        catch (FormatException)
        {
            Console.WriteLine("The number of pages has to be a whole number!");
            AddBook();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            AddBook();
        }
        
        Console.WriteLine("Book added successfully!");
        StartPage();
    }
}
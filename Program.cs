
var program = new Booklist();
program.StartPage();

class Booklist
{
    private List<Book> Books { get; set; } = [];
    
    public void StartPage()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the BOOKLIST book tracker program!\n" +
                          "What would you like to do?\n");
        
        Console.WriteLine("1. See your list of books\n" +
                          "2. Add a new book\n" +
                          "3. Remove a book\n" +
                          "4. Quit\n");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                DisplayBooks();
                break;
            case "2":
                AddBook();
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

    private void RemoveBook()
    {
        DisplayBooks();
        Console.WriteLine("Enter an index of the book you want to remove: ");
        var index = Convert.ToInt32(Console.ReadLine());
        Books.RemoveAt(index - 1);
        DisplayBooks();
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
        
        var book = new Book(title, author, pages, status);
        Books.Add(book);
        
        Console.WriteLine("Book added successfully!");
        StartPage();
    }
}

class Book
{
    public string Title { get; }
    public string Author { get; }
    public int Pages { get; }
    public string Status { get; }

    public Book(string title, string author, int pages, string status)
    {
        Title = title;
        Author = author;
        Pages = pages;
        Status = status;
    }
}
namespace booklist_test.Models;

public class EmptyInputException : Exception
{
    public EmptyInputException() : base("The title cannot be empty!") { }
    
    public EmptyInputException(string? message) : base(message) { }
}
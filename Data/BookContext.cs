using Microsoft.EntityFrameworkCore;
using booklist_test.Models;

namespace booklist_test.Data;

public class BookContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=books.db");
    }
}
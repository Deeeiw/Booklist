using Microsoft.EntityFrameworkCore;

namespace booklist_test;

public class BookContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=BookTracker;Trusted_Connection=True;TrustServerCertificate=True");
    }
}
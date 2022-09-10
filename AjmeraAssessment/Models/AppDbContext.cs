using Microsoft.EntityFrameworkCore;
using AjmeraAssessment.Models;

namespace AjmeraAssessment.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(k => new { k.AuthorId, k.BookId });
            
            modelBuilder.Entity<BookAuthor>().
                HasOne(h => h.Book).WithMany(p => p.BookAuthors).HasForeignKey(p => p.BookId);

            modelBuilder.Entity<BookAuthor>().
               HasOne(h => h.Author).WithMany(p => p.BookAuthors).HasForeignKey(p => p.AuthorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AjmeraAssessment.Models.BookAuthor> BookAuthor { get; set; }
    }
}

using AjmeraAssessment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AjmeraAssessment.Interfaces
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext dbContext;

        public BookRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(Book book)
        {
            dbContext.Books.Remove(book);
            dbContext.SaveChanges();
        }

        public List<Book> GetAll()
        {
            return dbContext.Books.Include(c => c.BookAuthors).ToList();
        }

        public Book GetById(int id)
        {
            return dbContext.Books.Include(c => c.BookAuthors).ThenInclude(ba => ba.Author).FirstOrDefault(x=> x.Id == id);
         }

        public Book GetByName(string name)
        {
            return dbContext.Books.FirstOrDefault(x => x.Name == name);
        }

        public void Insert(Book book)
        {
            dbContext.Books.Add(book);
           // dbContext.SaveChanges();
            //return book;
        }

        public void Update(Book book)
        {
            dbContext.Books.Update(book);
            //bookAttached.State = EntityState.Modified;
            //dbContext.SaveChanges();
           // return book;
        }
    }
}

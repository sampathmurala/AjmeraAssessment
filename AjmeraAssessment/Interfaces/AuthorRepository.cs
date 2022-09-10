using AjmeraAssessment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AjmeraAssessment.Interfaces
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext dbContext;

        public AuthorRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(Author author)
        {
            dbContext.Authors.Remove(author);
        }

        public List<Author> GetAll()
        {
            return dbContext.Authors.Include(c => c.BookAuthors).ToList();
        }

        public Author GetById(int id)
        {
            return dbContext.Authors.Include(c=> c.BookAuthors).ThenInclude(ba=> ba.Book).FirstOrDefault(x=> x.Id == id);
        }

        public void Insert(Author author)
        {
            dbContext.Authors.Add(author);
            //dbContext.SaveChanges();
            //return author;
        }

        public void Update(Author author)
        {
              dbContext.Authors.Update(author);
            //authorAttached.State = EntityState.Modified;
            //dbContext.SaveChanges();
            //return author;
        }
    }
    
}

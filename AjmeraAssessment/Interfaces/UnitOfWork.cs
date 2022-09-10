using AjmeraAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraAssessment.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        private IBookRepository bookRepository;
        private IAuthorRepository authorRepository;


        public IBookRepository BookRepository
        {
            get
            {
                return bookRepository ??= new BookRepository(appDbContext);
            }
        }

        public IAuthorRepository AuthorRepository 
        {
            get
            {
                return authorRepository ??= new AuthorRepository(appDbContext);
            }
        }

        public void Save()
        {
            appDbContext.SaveChanges();
        }
    }
}

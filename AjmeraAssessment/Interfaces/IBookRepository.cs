using AjmeraAssessment.Models;
using System.Collections.Generic;

namespace AjmeraAssessment.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book GetById(int id);
        Book GetByName(string name);
        void Insert(Book book);
        void Update(Book book);
        void Delete(Book book);
    }
}

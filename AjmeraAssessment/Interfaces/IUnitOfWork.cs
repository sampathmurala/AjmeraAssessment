using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraAssessment.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        void Save();
    }
}

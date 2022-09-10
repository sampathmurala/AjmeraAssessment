using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraAssessment.Models
{
    public class Book
    {
        public int  Id { get; set; }
        public string  Name { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }

    }
}

using AjmeraAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraAssessment.ViewModels
{
    public class BookAuthorsViewModel
    {
        public Book Book{ get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}

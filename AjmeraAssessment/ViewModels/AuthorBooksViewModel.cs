using AjmeraAssessment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AjmeraAssessment.ViewModels
{
    public class AuthorBooksViewModel
    {
        public Author Author { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public string Disabled { get; set; }
    }
}

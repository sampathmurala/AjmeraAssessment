using AjmeraAssessment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraAssessment.ViewModels
{
    public class AddBookViewModel
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        //public int BookId { get; set; }
        //public string BookName { get; set; }
        public List<SelectListItem> Books { get; set; }

        public string[] SelectedBooks { get; set; }
    }
}

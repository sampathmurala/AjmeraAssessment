using AjmeraAssessment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraAssessment.ViewModels
{
    public class AddAuthorViewModel
    { 
        public string  Name { get; set; } 
        public List<SelectListItem> Books { get; set; } 
        public string[] SelectedBooks { get; set; }
    }
}

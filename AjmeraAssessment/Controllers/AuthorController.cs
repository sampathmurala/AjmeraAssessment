using AjmeraAssessment.Interfaces;
using AjmeraAssessment.Models;
using AjmeraAssessment.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraAssessment.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMemoryCache memoryCache;

        public AuthorController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            this.unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
        }

        // GET: Author
        [Route("Authors")]
        public ActionResult Index()
        {
            string cacheKey = "authorList";
            if (!memoryCache.TryGetValue(cacheKey, out List<Author> authors))
            {
                //Cache is not available, so get it from db
                authors = unitOfWork.AuthorRepository.GetAll();

                //add the result to cache and set time for expiration
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                memoryCache.Set(cacheKey, authors, cacheExpiryOptions);

            }
            return View(authors);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            Author author = unitOfWork.AuthorRepository.GetById(id);
            if(author == null)
            {
                return NotFound($"No Author found for id: {id}");
            }
            var selectedBooks = author.BookAuthors.Select(x => x.Book  ).ToList();

            AuthorBooksViewModel authorBooks = new AuthorBooksViewModel()
            {
                Author = author,
                Books = selectedBooks
            };
            return View(authorBooks);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                unitOfWork.AuthorRepository.Insert(author);
                unitOfWork.Save();
                return RedirectToAction("Index", "Author");
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            var model = unitOfWork.AuthorRepository.GetById(id);
            var authorBooks = model.BookAuthors.Select(x => x.Book).ToList();
            AuthorBooksViewModel authorBooksViewModel = new AuthorBooksViewModel() { 
                Author = model,
                Books = authorBooks,
                Disabled = (authorBooks.Count > 0) ? "disabled": ""
            };
            return View(authorBooksViewModel);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Author author)
        {
            try
            {
                unitOfWork.AuthorRepository.Delete(author);
                unitOfWork.Save();
                return RedirectToAction("Index", "Author");
            }
            catch
            {
                return View();
            }
        }
    }
}

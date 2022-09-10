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
    public class BookController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMemoryCache memoryCache;

        public BookController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            this.unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
        }

        // GET: BookController
        [Route("Books")]
        public ActionResult Index()
        {
            string cacheKey = "bookList";
            if (!memoryCache.TryGetValue(cacheKey, out List<Book> books))
            {
                //Cache is not available, so get it from db
                books = unitOfWork.BookRepository.GetAll();

                //add the result to cache and set time for expiration
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                memoryCache.Set(cacheKey, books, cacheExpiryOptions);

            }
           
             return View(books);
        }
          

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = unitOfWork.BookRepository.GetById(id);
            if (book == null)
            {
                return NotFound($"No Book found for id: {id}");
            }
            var selectedAuthors = book.BookAuthors.Select(x => x.Author).ToList();

            BookAuthorsViewModel bookAuthorsViewModel = new BookAuthorsViewModel() { 
                Book = book,
                Authors = selectedAuthors
            };
            return View(bookAuthorsViewModel);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }
         
        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book modelBook)
        {
            try
            {
                //return RedirectToAction(nameof(Index));
                unitOfWork.BookRepository.Insert(modelBook);
                unitOfWork.Save();
                return RedirectToAction("Index", "Book");
            }
            catch
            {
                return View();
            }
        }


        // GET: BookController/Add/5  //{authorid}
        [HttpGet]
        public ActionResult Add(int id)
        {
            Author author = unitOfWork.AuthorRepository.GetById(id);
            if (author == null)
                return NotFound();
            var allBooks = unitOfWork.BookRepository.GetAll();
            var selectedBooks = author.BookAuthors.Select(x => x.Book).ToList();
            var selectList = new List<SelectListItem>();
            foreach (var book in allBooks)
            {
                selectList.Add(new SelectListItem(book.Name, book.Id.ToString(), selectedBooks.Select(x => x.Id).Contains(book.Id)));
            }
            AddBookViewModel addBookViewModel   = new AddBookViewModel()
            {
                AuthorId = author.Id,
                AuthorName = author.Name,
                Books = selectList
            };
            return View(addBookViewModel);
        }
        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddBookViewModel addBookViewModel)
        {
            try
            {
                var author = unitOfWork.AuthorRepository.GetById(addBookViewModel.AuthorId);
                if (author == null)
                    return NotFound();
                var existingBooks = author.BookAuthors.Select(x => x.BookId).ToList();
                
                var selectedBooks = addBookViewModel.SelectedBooks.Where(x=> x != null).Select(int.Parse).ToList();
                
                //Remove existing and unselected books
                var toRemove = existingBooks.Except(selectedBooks).ToList();
                author.BookAuthors = author.BookAuthors.Where(x => !toRemove.Contains(x.BookId)).ToList();

                //Add newly selected books
                foreach (var item in selectedBooks.Except(existingBooks))
                {
                    author.BookAuthors.Add(new BookAuthor() { AuthorId = author.Id, BookId = item });
                } 
                 
                unitOfWork.Save();
                return RedirectToAction("Details", "Author", new { id = author.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookController/Edit/5
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

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = unitOfWork.BookRepository.GetById(id);
            
            return View(model);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Book book)
        {
            try
            {
                unitOfWork.BookRepository.Delete(book);
                unitOfWork.Save();
                return RedirectToAction("Index", "Book");
            }
            catch
            {
                return View();
            }
        }
    }
}

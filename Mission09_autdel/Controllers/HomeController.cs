using Microsoft.AspNetCore.Mvc;
using Mission09_autdel.Models;
using Mission09_autdel.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_autdel.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }

        // pageNum parameter used for pagination
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 10;

            // Loaded the information in our models to be used on the index page
            var booksViewModel = new BooksViewModel
            {
                Books = repo.Books.OrderBy(b => b.Title).Skip((pageNum - 1) * pageSize).Take(pageSize),
                PageInfo = new PageInfo
                {
                    TotalNumBooks = repo.Books.Count(),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(booksViewModel);
        }
    }
}

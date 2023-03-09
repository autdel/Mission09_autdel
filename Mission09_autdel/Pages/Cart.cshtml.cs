using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission09_autdel.Infrastructure;
using Mission09_autdel.Models;

namespace Mission09_autdel.Pages
{
    public class CartModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        public CartModel (IBookstoreRepository temp)
        {
            repo = temp;
        }

        // Needed objects and variables for cart info and return button
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int bookID, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookID);

            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddItem(b, 1);

            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}

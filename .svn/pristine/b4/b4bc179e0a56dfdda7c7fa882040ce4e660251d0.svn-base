using BookStore.DA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        // GET: Order
        public ActionResult Order(int? bookId)
        {
            if (!User.Role.ToUpper().Equals("CUSTOMER"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }

            ViewBag.BookId = bookId;

            List<Book> books = new List<Book>();

            if (bookId != null)
            {
                Book book = _db.Book.Where(m => m.Id == bookId).FirstOrDefault();
                books.Add(book);
            }
            else
            {
                int[] cartBookId = _db.Cart.Where(m => m.CustomerId == User.CustomerId).Select(m => m.BookId).ToArray();
                books = _db.Book.Where(m => cartBookId.Contains(m.Id)).ToList();
            }

            books.ForEach(sBook =>
            {
                int bookQtyInCart = this._db.Cart.Where(m => m.BookId == sBook.Id && m.CustomerId == User.CustomerId).Select(m => m.Quantity).FirstOrDefault();
                sBook.Quantity = bookQtyInCart;
            });
            return View(books);
        }
    }
}
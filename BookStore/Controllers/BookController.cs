using BookStore.DA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;

namespace BookStore.Controllers
{
    [Authorize]
    public class BookController : BaseController
    {
        // Returns all Books for ADMIN
        public ActionResult BookList(int? message, string searchString)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            // Get all active or inactive Books from database for ADMIN
            IEnumerable<Book> books = _db.Book.ToList();
            return View(books);
        }
        [HttpPost]
         public ActionResult BookList(Book book)
        {
            List<Book> obj;
            if (book != null)
            {
                obj = _db.Book.Where(m => m.Name.Contains(book.Name)).Select(m => m).ToList();
            }
            else
            {
                obj = _db.Book.ToList();
            }
            return PartialView("BookInfo", obj);
        }

        // GET: To Add or Edit a Book
        [Authorize]
        public ActionResult AddOrEdit(int? bookId)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }

            Book book = new Book();
           
            // If editing a book - get book details
            if (bookId != null)
            {
                book = _db.Book.Where(m => m.Id == bookId).Select(m => m).FirstOrDefault();
            }

            // Dropdown lists
            book.CategoryList = this._db.Category.ToList();
            book.BookStatusList = this._db.BookStatus.Where(b => b.IsActive == true).ToList();

            return View(book);
        }

        // POST: Save or Update Book
        [HttpPost]
        public ActionResult AddOrEdit(Book book)
        {
            // Dropdown lists
            book.CategoryList = this._db.Category.ToList();
            book.BookStatusList = this._db.BookStatus.Where(b => b.IsActive == true).ToList();

            // Validate Book model
            if (!ModelState.IsValid)
            {
                book.Error = "Fill all mandatory fields";
                return View(book);
            }

            // If Book model validation is success
            try
            {
                if (!string.IsNullOrEmpty(book.FilePath))
                {
                    // Convert Book Image to byte[]
                    Image img = Image.FromFile(book.FilePath);
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        img.Save(mStream, img.RawFormat);
                        book.Image = mStream.ToArray();
                    }
                }

                // dbState = 1 is for Add Book, dbState = 2 is for Update Book - success message
                if (book.Id == 0)
                {
                    book.CreatedBy = User.Id;
                    book.CreatedOn = DateTime.Now;
                }

                book.UpdatedBy = User.Id;
                book.UpdatedOn = DateTime.Now;

                int dbState = 0;

                if (book.Id == 0)
                {
                    // Save Book to DATABASE
                    _db.Book.Add(book);
                    _db.SaveChanges();

                    dbState = 1;
                }
                else if (book.Id > 0)
                {
                    // Update Book to DATABASE
                    _db.Entry(book).State = EntityState.Modified;
                    _db.SaveChanges();

                    dbState = 2;
                }

                // If book is saved successfully, redirect to Book List along with success message
                return RedirectToAction("BookList", new { message = dbState });

            }
            catch (Exception)
            {
                // If book is not saved, show the error message on the same view
                book.Error = "Unexpected error occurred. Contact administrator.";
            }
            return View(book);
        }
    }
}
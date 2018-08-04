using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BookStore.DA;

namespace BookStore.Controllers
{
    [Authorize]
    public class BookStatusController : BaseController
    {
        // Run all BookStatus to view
        public ActionResult BookStatusList(int? message)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            // Get all active or inactive BookStatus from database to show in the view for ADMIN
            IEnumerable<BookStatus> BookStatus = _db.BookStatus.ToList();
            return View(BookStatus);
        }
        // GET: To Create or Edit a BookStatus
        [Authorize]
        public ActionResult AddOrEdit(int? bookStatusId)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            BookStatus bookstatus = new BookStatus();

            if (bookStatusId != null)
            {
                bookstatus = _db.BookStatus.Where(m => m.Id == bookStatusId).Select(m => m).FirstOrDefault();
            }
            return View(bookstatus);
        }

        // POST: Save or Update BookStatus
        [HttpPost]
        public ActionResult AddOrEdit(BookStatus bookStatus)
        {
            // Validate BookStaus model
            if (!ModelState.IsValid)
            {
                bookStatus.Error = "Fill all mandatory fields";
                return View(bookStatus);
            }
            // If BookStatus model validation is success
            try
            {
                if (bookStatus.Id == 0)
                {
                    bookStatus.CreatedBy = User.Id;
                    bookStatus.CreatedOn = DateTime.Now;
                }
                bookStatus.UpdatedBy = User.Id;
                bookStatus.UpdatedOn = DateTime.Now;

                int dbState = 0;

                if (bookStatus.Id == 0)
                {
                    // Save BookStatus to DATABASE
                    _db.BookStatus.Add(bookStatus);
                    _db.SaveChanges();

                    dbState = 1;
                }
                else if (bookStatus.Id > 0)
                {
                    // Update BookStaus to DATABASE
                    _db.Entry(bookStatus).State = EntityState.Modified;
                    _db.SaveChanges();

                    dbState = 2;
                }
                return RedirectToAction("BookStatusList", new { message = dbState });
            }
            catch (Exception)
            {
                // If BookStauts is not saved, show the error message
                bookStatus.Error = "Unexpected error occurred. Contact administrator.";
            }
            return View(bookStatus);
        }
    }
}
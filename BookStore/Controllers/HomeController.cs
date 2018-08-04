﻿using BookStore.DA;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (User != null && User.Identity.IsAuthenticated == true && User.Role.ToUpper().Equals("CUSTOMER"))
            {
                ViewModel vm = new ViewModel();
                //To get most popular books.
                var query = _db.OrderDetails //.Where(m => m.Order.Status != 10)
                               .GroupBy(m => m.BookId) //.Where(m => m.Count() > 0)
                               .Select(m => new { BookId = m.Key, Count = m.Count() })
                               .OrderByDescending(m => m.Count)
                               .Take(5)
                               .ToList();
                int[] bookIds = query.Select(q => q.BookId).ToArray();
                vm.mostPopularBooks = this._db.Book.Where(m => bookIds.Contains(m.Id)).ToList();

                //To get browsing history books.
                int[] bIds = _db.BrowsingHistory.OrderByDescending(m => m.Date).Where(m => m.CustomerID == User.CustomerId).Select(m => m.BookID).ToArray();
                //int[] bIds = _db.BrowsingHistory.OrderByDescending(m => m.Date).Select(m => m.BookID).ToArray();
                vm.browsingHistoryBooks = new List<Book>();
                foreach (var bookId in bIds)
                {
                    if (vm.browsingHistoryBooks.Count() < 5)
                    {
                        Book book = this._db.Book.Find(bookId);

                        if (!vm.browsingHistoryBooks.Contains(book))
                        {
                            vm.browsingHistoryBooks.Add(book);
                        }
                    }
                }
                //To get all books
                vm.Books = _db.Book.Take(100).ToList();

                return View(vm);
            }
            else
            {
                ViewModel vm = new ViewModel();
                //To get most popular books.
                var query = _db.OrderDetails //.Where(m => m.Order.Status != 10)
                               .GroupBy(m => m.BookId) //.Where(m => m.Count() > 0)
                               .Select(m => new { BookId = m.Key, Count = m.Count() })
                               .OrderByDescending(m => m.Count)
                               .Take(5)
                               .ToList();
                int[] bookIds = query.Select(q => q.BookId).ToArray();
                vm.mostPopularBooks = this._db.Book.Where(m => bookIds.Contains(m.Id)).ToList();

                //To get customer recently viewed books.
                int[] books = _db.OrderDetails.OrderByDescending(m => m.CreatedOn).Select(m => m.BookId).ToArray();
                vm.recentPurchasedBooks = new List<Book>();

                foreach (var bookId in books)
                {
                    if (vm.recentPurchasedBooks.Count() < 5)
                    {
                        Book book = this._db.Book.Find(bookId);

                        if (!vm.recentPurchasedBooks.Contains(book))
                        {
                            vm.recentPurchasedBooks.Add(book);
                        }
                    }
                }
                //To get all books
                vm.Books = _db.Book.Take(100).ToList();

                return View(vm);
            }
        }
        //To get all the popular books list
        public ActionResult viewPopularBooks()
        {
            var query = _db.OrderDetails //.Where(m => m.Order.Status != 10)
                                       .GroupBy(m => m.BookId) //.Where(m => m.Count() > 0)
                                       .Select(m => new { BookId = m.Key, Count = m.Count() })
                                       .OrderByDescending(m => m.Count)
                                       .ToList();
            int[] bookIds = query.Select(q => q.BookId).ToArray();
            List<Book> mostPopularBooks = this._db.Book.Where(m => bookIds.Contains(m.Id)).ToList();

            return View(mostPopularBooks);
        }
        //To get all Recently purchsed books list
        public ActionResult recentPurchasedBooks()
        {
            int[] bookIds = _db.OrderDetails.OrderByDescending(m => m.CreatedOn).Select(m => m.BookId).ToArray();
            List<Book> recentPurchasedBooks = this._db.Book.Where(m => bookIds.Contains(m.Id)).ToList();
            return View(recentPurchasedBooks);
        }
        //To get all the customer recently viewed books list
        public ActionResult viewHistory()
        {
            IEnumerable<Book> books = _db.Book.ToList();
            if (User != null && User.Identity.IsAuthenticated == true && User.Role.ToUpper().Equals("CUSTOMER"))
            {
                int[] bookIds = _db.BrowsingHistory.OrderByDescending(m => m.Date).Where(m => m.CustomerID == User.CustomerId).Select(m => m.BookID).ToArray();
                List<Book> browsingHistoryBooks = this._db.Book.Where(m => bookIds.Contains(m.Id)).ToList();
                return View(browsingHistoryBooks);
            }
            return View(books);
        }
        //To delete the customer recently viewed books
        public ActionResult Delete(int BookId)
        {
            BrowsingHistory history = _db.BrowsingHistory.Where(m => m.BookID == BookId).FirstOrDefault();
            _db.BrowsingHistory.Remove(history);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //search
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string Name,FormCollection form)
        {
            if (string.IsNullOrEmpty(Name) == true)
            {
                return RedirectToAction("Index");
            }
            List<Book> book = _db.Book.Where(m => m.Name.Contains(Name)).ToList();
            return View(book);
        }
        //search Json method
        public JsonResult Test(string term)
        {
            if (term == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var result = _db.Book.Where(m => m.Name.Contains(term)).Select(m => m.Name).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}



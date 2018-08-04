using BookStore.DA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{

    public class BookInformationController : BaseController
    {
        // GET: BookInformation
        public ActionResult Details(int? Id)
        {
            Book book = _db.Book.Find(Id);
            if (book.Status > 1 && book.Quantity == 0)
                return View(book);
            else
                if (User != null && User.Identity.IsAuthenticated == true && User.Role.ToUpper().Equals("CUSTOMER"))
                {
                    BrowsingHistory bh = new BrowsingHistory();
                    bh.CustomerID = User.CustomerId;
                    bh.BookID = book.Id;
                    bh.Date = DateTime.Now;
                    bh.Price = book.UnitPrice;
                    this._db.BrowsingHistory.Add(bh);
                    this._db.SaveChanges();
                    return View(book);
                }
                return View(book);
        }

        // POST: BookInformation for cart
        [Authorize]
        public ActionResult AddToCart(int bookId)
        {
            Book book = _db.Book.Find(bookId);

            if (book != null)
            {
                try
                {
                    // check if the same item is already in the cart
                    Cart existingItem = this._db.Cart.Where(m => m.CustomerId == User.CustomerId && m.BookId == book.Id).FirstOrDefault();

                    if (existingItem == null)
                    {
                        Cart userCart = new Cart();
                        userCart.CustomerId = User.CustomerId;
                        userCart.BookId = book.Id;
                        userCart.Quantity = 1;
                        userCart.CreatedBy = User.Id;
                        userCart.CreatedOn = DateTime.Now;
                        userCart.UpdatedBy = User.Id;
                        userCart.UpdatedOn = DateTime.Now;

                        this._db.Cart.Add(userCart);
                        this._db.SaveChanges();
                    }
                    else
                    {
                        if(existingItem.Quantity ==5)
                        {
                            return RedirectToAction("ShoppingCart", "ShoppingCart");
                        }
                        existingItem.Quantity += 1;
                        existingItem.UpdatedBy = User.Id;
                        existingItem.UpdatedOn = DateTime.Now;

                        this._db.Entry(existingItem).State = EntityState.Modified;
                        this._db.SaveChanges();
                    }
                    return RedirectToAction("ShoppingCart", "ShoppingCart");
                }
                catch (Exception ex)
                {
                    // Error Message
                    book.Error = "Unexpected error occurred. Contact administrator.";
                }
            }
            return RedirectToAction("Details", new { Id = bookId });
        }
        //Decrese Quantity
        [Authorize]
        public ActionResult DecreaseQuantity(int bookId)
        {
            Book book = _db.Book.Find(bookId);

            if (book != null)
            {
                try
                {
                    // check if the same item is already in the cart
                    Cart existingItem = this._db.Cart.Where(m => m.CustomerId == User.CustomerId && m.BookId == book.Id).FirstOrDefault();
                    if (existingItem.Quantity == 1)
                    {
                        return RedirectToAction("ShoppingCart", "ShoppingCart");
                    }
                        existingItem.Quantity -= 1;
                        existingItem.UpdatedBy = User.Id;
                        existingItem.UpdatedOn = DateTime.Now;

                        this._db.Entry(existingItem).State = EntityState.Modified;
                        this._db.SaveChanges();
                    }
                catch (Exception ex)
                {
                    // Error Message
                    book.Error = "Unexpected error occurred. Contact administrator.";
                }
            }
            return RedirectToAction("ShoppingCart", "ShoppingCart");
        }
        // POST: BookInformation for order
        [Authorize]
        public ActionResult Buynow(int bookId)
        {
            Book book = _db.Book.Find(bookId);

            if (book != null)
            {
                try
                {
                    // check if the same item is already in the cart
                    Cart existingItem = this._db.Cart.Where(m => m.CustomerId == User.CustomerId && m.BookId == book.Id).FirstOrDefault();

                    if (existingItem == null)
                    {
                        Cart userCart = new Cart();
                        userCart.CustomerId = User.CustomerId;
                        userCart.BookId = book.Id;
                        userCart.Quantity = 1;
                        userCart.CreatedBy = User.Id;
                        userCart.CreatedOn = DateTime.Now;
                        userCart.UpdatedBy = User.Id;
                        userCart.UpdatedOn = DateTime.Now;

                        this._db.Cart.Add(userCart);
                        this._db.SaveChanges();
                    }
                    return RedirectToAction("Order", "Order", new { bookId = bookId });
                }
                catch (Exception ex)
                {
                    // Error Message
                    book.Error = "Unexpected error occurred. Contact administrator.";
                }
            }
            return RedirectToAction("Details", new { Id = bookId });
        }
    }
}
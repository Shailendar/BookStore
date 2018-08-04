using BookStore.DA;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace BookStore.Controllers
{
    [Authorize]
    public class ShoppingCartController : BaseController
    {
        public ActionResult ShoppingCart(int? bookId)
        {
            if (!User.Role.ToUpper().Equals("CUSTOMER"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            ViewBag.BookId = bookId;
            IEnumerable<Cart> userCart = this._db.Cart.Where(m => m.CustomerId == User.CustomerId).ToList();
            return View(userCart);
        }
        [HttpPost]
        public ActionResult ShoppingCart(List<Cart> cart)
        {
            foreach (var item in cart)
            {
                Cart existingItem = this._db.Cart.Find(item.Id);

                if (existingItem.Quantity != item.Quantity)
                {
                    existingItem.Quantity = item.Quantity;
                    _db.Entry(existingItem).State = EntityState.Modified;
                }
            }
            _db.SaveChanges();

            return RedirectToAction("ShoppingCart");
        }
        public ActionResult Delete(int? BookId)
        {
            Cart cart = _db.Cart.Where(m => m.BookId == BookId).FirstOrDefault();
            _db.Cart.Remove(cart);
            _db.SaveChanges();
            return RedirectToAction("ShoppingCart");
        }
    }
}

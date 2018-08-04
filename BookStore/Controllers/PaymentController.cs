using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        // GET: Payment
        public ActionResult PaymentMethod(decimal grandtotal,int? bookId)
        {
            if (!User.Role.ToUpper().Equals("CUSTOMER"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            ViewBag.BookId = bookId;
            ViewBag.grandtotal = grandtotal;
            return View();
        }

        // GET: Payment
        public ActionResult VerifyPayment(int? bookId)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 100);

            bool orderStatus = true;

            if (randomNumber > 90)
            {
                orderStatus = false;
            }
            else
            {
                orderStatus = true;
            }

            return RedirectToAction("Confirmation", "OrderConfirmation", new { status = orderStatus, bookId = bookId });
        }
    }
}
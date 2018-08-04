using BookStore.DA;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class ShippingAddressController : BaseController
    {
        // GET: ShippingAddress
        public ActionResult ShippingAddress(decimal grandtotal, int? bookId)
        {
            if (!User.Role.ToUpper().Equals("CUSTOMER"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            ViewBag.bookId = bookId;
            ViewBag.grandtotal = grandtotal;
            ShippingAddress sa = _db.ShippingAddress.Where(m => m.CustomerId == User.CustomerId).FirstOrDefault();
            if (sa == null)
            {
                sa = new ShippingAddress();
            }
            return View(sa);
        }
        [HttpPost]
        public ActionResult ShippingAddress(ShippingAddress sa, string returnurl)
        {
            string url = this.Request.UrlReferrer.AbsoluteUri;
            // Validate shipping address model
            if (!ModelState.IsValid)
            {
                sa.Error = "Fill all mandatory fields";
                return View(sa);
            }
            // If shipping address model validation is success
            try
            {
                if (sa.Id == 0)
                {
                    sa.CreatedBy = User.Id;
                    sa.CreatedOn = DateTime.Now;
                }
                sa.UpdatedBy = User.Id;
                sa.UpdatedOn = DateTime.Now;
                sa.CustomerId = User.CustomerId;
                int dbState = 0;

                if (sa.Id == 0)
                {
                    // Save address to DATABASE
                    _db.ShippingAddress.Add(sa);
                    _db.SaveChanges();
                    dbState = 1;
                }
                else if (sa.Id > 0)
                {
                    // Update address to DATABASE
                    _db.Entry(sa).State = EntityState.Modified;
                    _db.SaveChanges();
                    dbState = 2;
                }
                // If address is saved successfully, redirect to customer List along with success message
                //return RedirectToAction("ShippingAddress", new { grandtotal = ViewBag.grandtotal, message = dbState });
                return Redirect(url);
            }
            catch (Exception ex)
            {
                // If address is not saved, show the error message
                sa.Error = "Unexpected error occurred. Contact administrator.";
            }
            return View(sa);
        }
    }
}

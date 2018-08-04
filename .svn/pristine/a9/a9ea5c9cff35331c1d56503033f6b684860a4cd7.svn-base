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
    public class PaymentTypeController : BaseController
    {
        // Run all PaymentType to view
        public ActionResult PaymentTypeList(int? message)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            // Get all active or inactive PaymentType  from database to show in the view for ADMIN
            IEnumerable<PaymentType> PaymentType = _db.PaymentType.ToList();
            return View(PaymentType);
        }
        // GET: To Create or Edit a PaymentType
        [Authorize]
        public ActionResult AddOrEdit(int? PaymentTypeId)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            PaymentType paymenttype = new PaymentType();
            if (PaymentTypeId != null)
            {
                paymenttype = _db.PaymentType.Where(m => m.Id == PaymentTypeId).Select(m => m).FirstOrDefault();
            }
            return View(paymenttype);
        }
        // POST: Save or Update PaymentType
        [HttpPost]
        public ActionResult AddOrEdit(PaymentType paymenttype)
        {
            // Validate PaymentType model
            if (!ModelState.IsValid)
            {
                paymenttype.Error = "Fill all  mandatory fields";
                return View(paymenttype);
            }
            // If paymentType validation is succuss
            try
            {
                if (paymenttype.Id == 0)
                {
                    paymenttype.CreatedBy = User.Id;
                    paymenttype.CreatedOn = DateTime.Now;
                }
                paymenttype.UpdatedBy = User.Id;
                paymenttype.UpdatedOn = DateTime.Now;

                int dbstate = 0;

                if (paymenttype.Id == 0)
                {
                    // Save PaymentType to DATABASE
                    _db.PaymentType.Add(paymenttype);
                    _db.SaveChanges();

                    dbstate = 1;
                }
                else if (paymenttype.Id > 0)
                {
                    // Upadete PaymentType to DATABASE
                    _db.Entry(paymenttype).State = EntityState.Modified;
                    _db.SaveChanges();

                    dbstate = 2;
                }
                return RedirectToAction("PaymentTypeList", new { message = dbstate });
            }
            catch (Exception)
            {
                // If PaymentType is not saved show the error message
                paymenttype.Error = "Unexpected error occurred. Contact administrator.";
            }
            return View(paymenttype);
        }
    }
}
using BookStore.DA;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class MyProfileController : BaseController
    {
        // GET: MyProfile
        [Authorize]
        public ActionResult MyProfile()
        {
            Customer customer = _db.Customer.Where(m => m.Email == User.Email).FirstOrDefault();
            if (customer == null)
            {
                customer = new Customer();
            }
            return View(customer);
        }
        [Authorize]
        public ActionResult AddOrEdit()
        {
            Customer customer = new Customer();
            customer = _db.Customer.Where(m => m.Email == User.Email).FirstOrDefault();
            return View(customer);
        }
        [HttpPost]
        public ActionResult AddOrEdit(Customer customer)
        {
            // Validate customer model
            if (!ModelState.IsValid)
            {
                customer.Error = "Fill all mandatory fields";
                return View(customer);
            }

            // If customer model validation is success
            try
            {
                if (customer.Id == 0)
                {
                    customer.CreatedBy = customer.Id;
                    customer.CreatedOn = DateTime.Now;
                }

                customer.UpdatedBy = customer.Id;
                customer.UpdatedOn = DateTime.Now;

                int dbState = 0;

                if (customer.Id == 0)
                {
                    // Save customer to DATABASE
                    _db.Customer.Add(customer);
                    _db.SaveChanges();

                    dbState = 1;
                }
                else if (customer.Id > 0)
                {
                    // Update customer to DATABASE
                    _db.Entry(customer).State = EntityState.Modified;
                    _db.SaveChanges();
                    dbState = 2;
                }
                // If customer is saved successfully, redirect to customer List along with success message
                return RedirectToAction("MyProfile", new { message = dbState });

            }
            catch (Exception ex)
            {
                // If customer is not saved, show the error message
                customer.Error = "Unexpected error occurred. Contact administrator.";
            }
            return View(customer);
        }
    }
}
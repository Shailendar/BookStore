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
    public class OrderStatusController : BaseController
    {
        // Run all OrderStatus to view
        public ActionResult OrderStatusList(int? message)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            // Get all active or inactive OrderStatus from database to show in the view for ADMIN
            IEnumerable<OrderStatus> OrderStatus = _db.OrderStatus.ToList();
            return View(OrderStatus);
        }
        // GET: To Create or Edit a OrderStatus
        [Authorize]
        public ActionResult AddOrEdit(int? orderStatusId)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            OrderStatus orderstatus = new OrderStatus();
            if (orderStatusId != null)
            {
                orderstatus = _db.OrderStatus.Where(m => m.Id == orderStatusId).Select(m => m).FirstOrDefault();
            }
            return View(orderstatus);
        }
        // POST: Save or Update OrderStatus
        [HttpPost]
        public ActionResult AddOrEdit(OrderStatus orderStatus)
        {
            // Validate OrderStatus model
            if (!ModelState.IsValid)
            {
                orderStatus.Error = "Fill all the mandatory fields";
                return View(orderStatus);
            }

            // If OrderStatus model validation is success
            try
            {
                if (orderStatus.Id == 0)
                {
                    orderStatus.CreatedBy = User.Id;
                    orderStatus.CreatedOn = DateTime.Now;
                }
                orderStatus.UpdatedBy = User.Id;
                orderStatus.UpdatedOn = DateTime.Now;

                int dbState = 0;

                if (orderStatus.Id == 0)
                {
                    // Save OrderStatus to DATABASE
                    _db.OrderStatus.Add(orderStatus);
                    _db.SaveChanges();

                    dbState = 1;
                }
                else if (orderStatus.Id > 0)
                {
                    // Upadate OrderStatus to DATBASE 
                    _db.Entry(orderStatus).State = EntityState.Modified;
                    _db.SaveChanges();

                    dbState = 2;
                }
                return RedirectToAction("OrderStatusList", new { message = dbState });
            }
            catch (Exception ex)
            {
                // If OrderStatus is not saved, show the error message
                orderStatus.Error = "Unexpected error occurred. Contact administrator.";
            }
            return View(orderStatus);
        }
    }
}
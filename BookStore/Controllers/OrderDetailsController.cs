using BookStore.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrderDetailsController : BaseController
    {
        // GET: OrderDetails
        public ActionResult OrderDetails(int orderId)
        {
            OrderDetails od = _db.OrderDetails.Find(orderId);
            if (od == null)
                return View("No Records Found");
            else
                return View(od);
        }
    }
}
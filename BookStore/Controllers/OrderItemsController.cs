using BookStore.DA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrderItemsController : BaseController
    {
        // GET: OrderItems
        public ActionResult OrderItems(int orderId)
        {
            IEnumerable<OrderDetails> od = _db.OrderDetails.Where(m => m.OrderId == orderId).ToList();
            return View(od);
        }
    }
}
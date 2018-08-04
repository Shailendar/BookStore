using BookStore.DA;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class OrderConfirmationController : BaseController
    {
        // GET: OrderConfirmation
        public ActionResult Confirmation(bool status, int? bookId)
        {
            if (!User.Role.ToUpper().Equals("CUSTOMER"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }

            if (status == true)
            {
                List<Cart> cart = _db.Cart.Where(m => m.CustomerId == User.CustomerId && (bookId == null || m.BookId == bookId)).ToList();

                decimal totalPrice = 0;
                decimal totalDiscount = 0;
                decimal totalShippingCost = 0;
                decimal grandTotal = 0;

                cart.ForEach(m =>
                {
                    Book book = this._db.Book.Find(m.BookId);
                    totalPrice += book.UnitPrice * (decimal)m.Quantity;
                    totalDiscount += book.Discount * (decimal)m.Quantity;
                    totalShippingCost += book.ShippingCost;
                    grandTotal += (book.UnitPrice * (decimal)m.Quantity) - (book.Discount * (decimal)m.Quantity) + book.ShippingCost;
                });

                if (cart.Count() > 0)
                {
                    int sId = _db.ShippingAddress.Where(m => m.CustomerId == User.CustomerId).Select(m=>m.Id).FirstOrDefault();

                    // Save the order
                    Order customerOrder = new Order();
                    customerOrder.CustomerId = User.CustomerId;
                    customerOrder.OrderDate = DateTime.Now;
                    customerOrder.ShippingAddressId = sId; // HARD CODED
                    customerOrder.PaymentTypeId = 1; // HARD CODED
                    customerOrder.TotalPrice = totalPrice;
                    customerOrder.TotalDiscount = totalDiscount;
                    customerOrder.TotalShippingCost = totalShippingCost;
                    customerOrder.GrandTotal = grandTotal;
                    customerOrder.Status = 1; // 1- ORDERED
                    customerOrder.CreatedBy = User.Id;
                    customerOrder.CreatedOn = DateTime.Now;
                    customerOrder.UpdatedBy = User.Id;
                    customerOrder.UpdatedOn = DateTime.Now;

                    this._db.Order.Add(customerOrder);
                    //_db.SaveChanges();

                    // Save the order details

                    cart.ForEach(m =>
                    {
                        Book book = this._db.Book.Find(m.BookId);

                        OrderDetails details = new OrderDetails();
                        details.OrderId = customerOrder.Id;
                        details.BookId = m.BookId;
                        details.UnitPrice = book.UnitPrice;
                        details.Discount = book.Discount;
                        details.ShippingCost = book.ShippingCost;
                        details.Quantity = m.Quantity;
                        details.Total = ((decimal)m.Quantity * book.UnitPrice) - ((decimal)m.Quantity * book.Discount) + book.ShippingCost;
                        details.CreatedBy = User.Id;
                        details.CreatedOn = DateTime.Now;
                        details.UpdatedBy = User.Id;
                        details.UpdatedOn = DateTime.Now;

                        this._db.OrderDetails.Add(details);
                        //_db.SaveChanges();

                    });

                    // Remove cart items
                    cart.ForEach(item =>
                    {
                        _db.Cart.Remove(item);
                        //_db.SaveChanges();

                    });

                    // Decrease book Qty
                    cart.ForEach(m =>
                    {
                        Book book = this._db.Book.Find(m.BookId);
                        book.Quantity = (book.Quantity - m.Quantity);

                        this._db.Entry(book).State = EntityState.Modified;
                        //_db.SaveChanges();

                    });
                    _db.SaveChanges();
                }
                // Show success message
                TempData["SUCCESS"] = "Your order is successful";
                {
                    System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress("shailendar@cosyn.in", "BookStore"),
                        new System.Net.Mail.MailAddress(User.Email));
                    m.Subject = "Order confirmation";
                    m.Body = string.Format("Dear Customer<BR><BR> Thank you for your order!<BR><BR>We will send you another email once the items in your order have been dispatched. Meanwhile, you can check the status of your order on BookStore<BR><BR>Regards<BR>BookStore Team");
                    m.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com");
                    smtp.Credentials = new System.Net.NetworkCredential("shailendar@cosyn.in", "C@syn%5458");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
            }
            else
            {
                // Show success message
                TempData["ERROR"] = "Order failed";
                {
                    System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress("shailendar@cosyn.in", "BookStore"),
                        new System.Net.Mail.MailAddress(User.Email));
                    m.Subject = "Order confirmation";
                    m.Body = string.Format("Dear Customer<BR><BR> Sorry for the Inconvinance, Your Order is failed!<BR><BR>Regards<BR>BookStore Team");
                    m.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com");
                    smtp.Credentials = new System.Net.NetworkCredential("shailendar@cosyn.in", "C@syn%5458");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
            }
            return View();
        }
    }
}
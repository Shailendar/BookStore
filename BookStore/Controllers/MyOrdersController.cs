﻿using BookStore.DA;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class MyOrdersController : BaseController
    {
        // GET: MyOrders
        public ActionResult MyOrders(string searchString)
        {
            if (User.Role.ToUpper().Equals("ADMIN"))
            {
                IEnumerable<Order> od = _db.Order.OrderByDescending(m => m.OrderDate).ToList();
                // Search
                if (!String.IsNullOrEmpty(searchString))
                {
                    od = od.Where(o => o.Customer.FirstName.ToLower().Contains(searchString.ToLower()) || o.OrderStatus.Code.ToLower().Contains(searchString.ToLower()));
                }
                return View(od);
            }
            else if (User.Role.ToUpper().Equals("CUSTOMER"))
            {
                IEnumerable<Order> od = _db.Order.Where(m => m.Customer.Email == User.Email).OrderByDescending(m => m.OrderDate).ToList();
                return View(od);
            }
            return View();
        }
        //Get
        public ActionResult Edit(int? orderId)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            Order order = new Order();
            if (orderId != null)
            {
                order = _db.Order.Where(m => m.Id == orderId).Select(m => m).FirstOrDefault();
            }
            order.OrderStatusList = this._db.OrderStatus.Where(b => b.IsActive == true).ToList();
            return View(order);
        }
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            order.OrderStatusList = this._db.OrderStatus.Where(b => b.IsActive == true).ToList();
            if (order.Id > 0)
            {
                OrderDetails od = _db.OrderDetails.Where(m => m.OrderId == order.Id).FirstOrDefault();
                Book book = _db.Book.Find(od.BookId);
                book.Quantity = book.Quantity + od.Quantity;
                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();
            }
            if (order.Status == 8)
            {
                Customer customer = _db.Customer.Where(b => b.Id == order.CustomerId).FirstOrDefault();
                System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress("shailendar@cosyn.in", "BookStore"),
                        new System.Net.Mail.MailAddress(customer.Email));
                m.Subject = "Order Status";
                m.Body = string.Format("Dear Customer<BR><BR>Greetings from BookStore!<BR><BR>We are pleased to inform you that the items in your Order have been packed by the seller and are ready to be delivered.<BR><BR>Regards<BR>BookStore Team");
                m.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com");
                smtp.Credentials = new System.Net.NetworkCredential("shailendar@cosyn.in", "C@syn%5458");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
            if (order.Status == 9)
            {
                Customer customer = _db.Customer.Where(b => b.Id == order.CustomerId).FirstOrDefault();
                System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress("shailendar@cosyn.in", "BookStore"),
                        new System.Net.Mail.MailAddress(customer.Email));
                m.Subject = "Order Status";
                m.Body = string.Format("Dear Customer<BR><BR>Greetings from BookStore!<BR><BR>We are pleased to inform that your order have been delivered. This completes your order. Thank you for shopping!<BR><BR>Regards<BR>BookStore Team");
                m.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com");
                smtp.Credentials = new System.Net.NetworkCredential("shailendar@cosyn.in", "C@syn%5458");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
            else if(order.Status==10)
             {
                    Customer customer = _db.Customer.Where(b => b.Id == order.CustomerId).FirstOrDefault();
                    System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                            new System.Net.Mail.MailAddress("shailendar@cosyn.in", "BookStore"),
                            new System.Net.Mail.MailAddress(customer.Email));
                    m.Subject = "Order Status";
                    m.Body = string.Format("Dear Customer<BR><BR>Greetings from BookStore!<BR><BR>We are pleased to inform that you have cancelled your order. Thank you for visiting us!<BR><BR>Regards<BR>BookStore Team");
                    m.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com");
                    smtp.Credentials = new System.Net.NetworkCredential("shailendar@cosyn.in", "C@syn%5458");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
                return RedirectToAction("MyOrders");
            }
        public ActionResult Cancel(int? orderId)
        {
            Order order = _db.Order.Find(orderId);
            if (order.Status == 1 || order.Status == 8)
            {
                order.Status = 2;
                OrderDetails od = _db.OrderDetails.Where(m => m.OrderId == orderId).FirstOrDefault();
                Book book = _db.Book.Find(od.BookId);
                book.Quantity = book.Quantity + od.Quantity;
                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();
                {
                    Customer customer = _db.Customer.Where(b => b.Id == order.CustomerId).FirstOrDefault();
                    System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                            new System.Net.Mail.MailAddress("shailendar@cosyn.in", "BookStore"),
                            new System.Net.Mail.MailAddress(customer.Email));
                    m.Subject = "Order Status";
                    m.Body = string.Format("Dear Customer<BR><BR>Greetings from BookStore!<BR><BR>We are pleased to inform that you have cancelled your order. Thank you for visiting us!<BR><BR>Regards<BR>BookStore Team");
                    m.IsBodyHtml = true;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com");
                    smtp.Credentials = new System.Net.NetworkCredential("shailendar@cosyn.in", "C@syn%5458");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
                return RedirectToAction("MyOrders");
            }
            return RedirectToAction("MyOrders");
        }
    }
}
using BookStore.DA;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class BaseController : Controller
    {
        public BookStoreDbContext _db;

        public BaseController()
        {
            this._db = new BookStoreDbContext();
        }

        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}
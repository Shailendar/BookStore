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
    public class CategoryController : BaseController
    {
        // Returns all categories to view
        public ActionResult CategoryList(int? message)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            // Get all active or inactive catgeories from database to show in the view for ADMIN
            IEnumerable<Category> categories = _db.Category.ToList();
            return View(categories);
        }
        // GET: To Create or Edit a Category
        [Authorize]
        public ActionResult AddOrEdit(int? categoryId)
        {
            if (!User.Role.ToUpper().Equals("ADMIN"))
            {
                return RedirectToAction("Index", "NotAuthorised");
            }
            Category category = new Category();

            if (categoryId != null)
            {
                category = _db.Category.Where(m => m.Id == categoryId).Select(m => m).FirstOrDefault();
            }
            return View(category);
        }
        // POST: Save or Update Category
        [HttpPost]
        public ActionResult AddOrEdit(Category category)
        {
            // Validate category model
            if (!ModelState.IsValid)
            {
                category.Error = "Fill all mandatory fields";
                return View(category);
            }
            // If category model validation is success
            try
            {
                if (category.Id == 0)
                {
                    category.CreatedBy = User.Id;
                    category.CreatedOn = DateTime.Now;
                }

                category.UpdatedBy = User.Id;
                category.UpdatedOn = DateTime.Now;

                int dbState = 0;

                if (category.Id == 0)
                {
                    // Save category to DATABASE
                    _db.Category.Add(category);
                    _db.SaveChanges();
                    dbState = 1;
                }
                else if (category.Id > 0)
                {
                    // Update category to DATABASE
                    _db.Entry(category).State = EntityState.Modified;
                    _db.SaveChanges();
                    dbState = 2;
                }

                // If category is saved successfully, redirect to Category List along with success message
                return RedirectToAction("CategoryList", new { message = dbState });
            }
            catch (Exception ex)
            {
                // If category is not saved, show the error message
                category.Error = "Unexpected error occurred. Contact administrator.";
            }
            return View(category);
        }
    }
}
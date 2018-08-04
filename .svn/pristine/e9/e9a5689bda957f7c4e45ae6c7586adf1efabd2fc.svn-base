using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BookStore.Models;
using System.Web.Script.Serialization;
using System.Web.Security;
using BookStore.DA;

namespace BookStore.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //CreateDefaults();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // RAMESH: 2016-06-25 - to create default admin & users
        // Change the email/password, run and comment if you created the admin and other users
        //public void CreateDefaults()
        //{
        //    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        //    string name = "ramesh.bandi@cosyn.in";
        //    string password = "Admin@123";
        //    string roleName = "Administrator";

        //    //Create Role Admin if it does not exist
        //    var role = roleManager.FindByName(roleName);
        //    if (role == null)
        //    {
        //        // *** INITIALIZE WITH CUSTOM APPLICATION ROLE CLASS:
        //        role = new ApplicationRole(roleName);
        //        var roleresult = roleManager.Create(role);
        //    }

        //    var user = userManager.FindByName(name);
        //    if (user == null)
        //    {
        //        user = new ApplicationUser { UserName = name, Email = name };
        //        var result = userManager.Create(user, password);
        //        result = userManager.SetLockoutEnabled(user.Id, false);
        //    }

        //    // Add user admin to Role Admin if not already added
        //    var rolesForUser = userManager.GetRoles(user.Id);
        //    if (!rolesForUser.Contains(role.Name))
        //    {
        //        var result = userManager.AddToRole(user.Id, role.Name);
        //    }

        //    //Create User1
        //    //name = "shailendar@cosyn.in";
        //    //password = "User@123";
        //    //roleName = "Employee";

        //    //Create Role Admin if it does not exist
        //    role = roleManager.FindByName(roleName);
        //    if (role == null)
        //    {
        //        // *** INITIALIZE WITH CUSTOM APPLICATION ROLE CLASS:
        //        role = new ApplicationRole(roleName);
        //        var roleresult = roleManager.Create(role);
        //    }

        //    user = userManager.FindByName(name);
        //    if (user == null)
        //    {
        //        user = new ApplicationUser { UserName = name, Email = name };
        //        var result = userManager.Create(user, password);
        //        result = userManager.SetLockoutEnabled(user.Id, false);
        //    }

        //    // Add user admin to Role Admin if not already added
        //    rolesForUser = userManager.GetRoles(user.Id);
        //    if (!rolesForUser.Contains(role.Name))
        //    {
        //        var result = userManager.AddToRole(user.Id, role.Name);
        //    }

        //    //Create User2
        //    //name = "meghana@cosyn.in";
        //    //password = "User@123";
        //    //roleName = "Employee";

        //    //Create Role Admin if it does not exist
        //    role = roleManager.FindByName(roleName);
        //    if (role == null)
        //    {
        //        // *** INITIALIZE WITH CUSTOM APPLICATION ROLE CLASS:
        //        role = new ApplicationRole(roleName);
        //        var roleresult = roleManager.Create(role);
        //    }

        //    user = userManager.FindByName(name);
        //    if (user == null)
        //    {
        //        user = new ApplicationUser { UserName = name, Email = name };
        //        var result = userManager.Create(user, password);
        //        result = userManager.SetLockoutEnabled(user.Id, false);
        //    }

        //    // Add user admin to Role Admin if not already added
        //    rolesForUser = userManager.GetRoles(user.Id);
        //    if (!rolesForUser.Contains(role.Name))
        //    {
        //        var result = userManager.AddToRole(user.Id, role.Name);
        //    }

        //    //Create User3
        //    //name = "rajitha@cosyn.in";
        //    //password = "User@123";
        //    //roleName = "Employee";

        //    //Create Role Admin if it does not exist
        //    role = roleManager.FindByName(roleName);
        //    if (role == null)
        //    {
        //        // *** INITIALIZE WITH CUSTOM APPLICATION ROLE CLASS:
        //        role = new ApplicationRole(roleName);
        //        var roleresult = roleManager.Create(role);
        //    }

        //    user = userManager.FindByName(name);
        //    if (user == null)
        //    {
        //        user = new ApplicationUser { UserName = name, Email = name };
        //        var result = userManager.Create(user, password);
        //        result = userManager.SetLockoutEnabled(user.Id, false);
        //    }

        //    // Add user admin to Role Admin if not already added
        //    rolesForUser = userManager.GetRoles(user.Id);
        //    if (!rolesForUser.Contains(role.Name))
        //    {
        //        var result = userManager.AddToRole(user.Id, role.Name);
        //    }
        //}

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = UserManager.FindByName(model.Email);

            if (user == null)
            {
                TempData["ERROR"] = "Incorrect Username and/or Password";
                return View(model);
            }
            if (user.EmailConfirmed == false)
            {
                ModelState.AddModelError("", "Please confirm your email before login");
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        CreateAuthenticationTicket(model.Email);

                        return RedirectToLocal(returnUrl);
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    {
                        TempData["ERROR"] = "Incorrect Username and/or Password";
                        return View(model);
                    }
            }
        }

        // RAMESH: 2016-06-25 - creates authentication ticket for logged in user; can be accessible through "User" object in all controllers/views
        public void CreateAuthenticationTicket(string username)
        {
            var user = UserManager.Users.Where(u => u.UserName == username).First();
            var customer = this._db.Customer.Where(m=>m.Email == username).FirstOrDefault();

            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = user.Id;
            serializeModel.Email = user.Email;
            serializeModel.CustomerId = customer.Id;
            serializeModel.FirstName = customer.FirstName;
            serializeModel.LastName = customer.LastName;

            var userRoles = UserManager.GetRoles(user.Id);

            if (userRoles.Count() > 0)
            {
                serializeModel.Role = userRoles[0];
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1, username, DateTime.Now, DateTime.Now.AddHours(8), false, userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, EmailConfirmed = false };
                user.Email = model.Email;
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Assign user Role - "Customer"
                    var rolesForUser = UserManager.GetRoles(user.Id);
                    if (!rolesForUser.Contains("Customer"))
                    {
                        result = UserManager.AddToRole(user.Id, "Customer");
                    }
                    //Save Customer details in customer table
                    Customer customer = new Customer()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Phone = model.Phone,
                        BirthDate = model.BirthDate
                    };
                    _db.Customer.Add(customer);
                    _db.SaveChanges();

                    //CreateAuthenticationTicket(model.Email);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    if (result.Succeeded)
                    {
                        //System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                        //    new System.Net.Mail.MailAddress("shailendar@cosyn.in", "BookStore"),
                        //    new System.Net.Mail.MailAddress(user.Email));
                        //m.Subject = "Email Confirmation";
                        //m.Body = string.Format("Dear {0}<BR/>Thank you for your registration, please click on the below link to complete your registration: <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", user.UserName, Url.Action("ConfirmEmail", "Account", new { Token = user.Id, Email = user.Email }, Request.Url.Scheme));
                        //m.IsBodyHtml = true;
                        //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mail.yahoo.com");
                        //smtp.Credentials = new System.Net.NetworkCredential("shailendar@cosyn.in", "C@syn%5458");
                        //smtp.EnableSsl = true;
                        //smtp.Send(m);
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }

                // If we got this far, something failed, redisplay form
                else
                {
                    AddErrors(result);
                }
            }
            return View(model);
        }
        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        //{
        //    ApplicationUser user = this.UserManager.FindByEmail(Email);
        //    if (user != null)
        //    {
        //        if (user.Email == Email)
        //        {
        //            user.EmailConfirmed = true;
        //            await UserManager.UpdateAsync(user);
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //            //await SignInAsync(user, isPersistent: false);
        //            CreateAuthenticationTicket(Email);
        //            return RedirectToAction("Index", "Home", new { EmailConfirmed = user.Email });
        //        }
        //        else
        //        {
        //            return RedirectToAction("Confirm", "Account", new { Email = user.Email });
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Confirm", "Account", new { Email = "" });
        //    }
        //}
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        //Action Confirm View
        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            ViewBag.Email = Email; return View();
        }
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            // to kill user identity object
            authManager.SignOut();

            // to kill forms authentication object
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
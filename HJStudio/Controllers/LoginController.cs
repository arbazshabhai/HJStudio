using HJStudio.Models;
using HJStudio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace HJStudio.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string ReturnUrl)
        {
            var identity = HttpContext.User.Identity;
            if (identity.IsAuthenticated)
            {
                if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                   && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            LoginModel _model = new LoginModel();
            _model.ReturnUrl = ReturnUrl;
            return View(_model);
        }

        [HttpPost]
        public ActionResult Index(LoginModel _model)
        {
            string ReturnUrl = _model.ReturnUrl;
            LogedUserDetails user = LoginService.CheckLogin(_model);
            if (user != null)
            {
                var json_model = new JavaScriptSerializer().Serialize(user);
                FormsAuthentication.SetAuthCookie(user.Name.ToUpper(), false);
                var authTicket = new FormsAuthenticationTicket(1, user.Name.ToUpper(), DateTime.Now, DateTime.Now.AddHours(2), false, json_model);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                //var Menu = MenuService.GetUserMenu(user.UserId);
                //Session["UserMenu"] = Menu;

                if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                   && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                {
                    return Redirect(ReturnUrl);

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(_model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
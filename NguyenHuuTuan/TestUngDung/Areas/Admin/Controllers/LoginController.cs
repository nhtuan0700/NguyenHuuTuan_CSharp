using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Areas.Admin.Models;
using TestUngDung.Common;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            var session = (LoginModel)Session[Contrants.USER_SESSION];
            if (session != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = new UserDAO().login(model.UserName, Encryptor.EncryptMD5(model.Password));
                if (result)
                {
                    Session.Add(Contrants.USER_SESSION, model);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Đăng nhập không thành công");
            return View("Index");
        }


        public ActionResult Logout()
        {
            Session[Contrants.USER_SESSION] = null;
            return RedirectToAction("Index","Login");
        }
    }
}
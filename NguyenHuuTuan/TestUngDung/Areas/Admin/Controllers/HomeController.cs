using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        NguyenHuuTuanContext db = new NguyenHuuTuanContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.users = db.UserAccount.Count();
            ViewBag.products = db.Product.Count();
            return View();
        }
    }
}
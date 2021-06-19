using ModelEF.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Controllers
{
    public class BaseController : Controller
    {
        public void setViewBag()
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryList = dao.listAll();
        }
    }
}
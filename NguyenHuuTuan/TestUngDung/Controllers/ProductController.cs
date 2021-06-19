using ModelEF.DAO;
using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Controllers
{
    public class ProductController : BaseController
    {
        protected ProductDAO productDAO;
        
        public ProductController()
        {
            productDAO = new ProductDAO();
        }

        public ActionResult Index(int page = 1, int pageSize = 6)
        {
            var model = productDAO.ListWhereAll(null, null);
            setViewBag();
            return View(model.ToPagedList(page, pageSize));
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}
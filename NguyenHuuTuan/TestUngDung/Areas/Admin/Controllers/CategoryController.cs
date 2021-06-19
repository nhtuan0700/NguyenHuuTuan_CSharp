using ModelEF.DAO;
using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        protected CategoryDAO categroyDAO;

        public CategoryController()
        {
            categroyDAO = new CategoryDAO();
        }
        // GET: Admin/Category
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var model = categroyDAO.listAll();
            return View(model.ToPagedList(page, pageSize));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categroyDAO.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var rs = categroyDAO.Update(model);
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Edit", "Category", rs.ID);
                }
                catch (Exception e)
                {
                    SetAlert("Cập nhật thất bại!", "error");
                }
            }
            return View(model);
        }
    }
}
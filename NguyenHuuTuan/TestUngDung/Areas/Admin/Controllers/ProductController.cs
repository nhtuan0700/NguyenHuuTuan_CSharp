using ModelEF.DAO;
using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        protected ProductDAO productDAO;

        public ProductController()
        {
            productDAO = new ProductDAO();
        }
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            string nameSearch = Request.Params["name"];
            string category = Request.Params["category"];
            int idSelected = !string.IsNullOrEmpty(category) ? int.Parse(category) : 0;
            var model = productDAO.ListWhereAll(nameSearch, idSelected);
            ViewBag.Name = nameSearch;
            ViewBag.page = page;
            setViewBagDropDown(idSelected);
            return View(model.ToPagedList(page, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.ID = productDAO.GetNewID();
            setViewBagDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product model, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    int id = productDAO.GetNewID();
                    model.Image = uploadImage(Image, id);
                }
                else
                {
                    model.Image = "";
                }
                try
                {
                    var product = productDAO.Create(model);
                    SetAlert("Tạo mới thành công!", "success");
                    return RedirectToAction("Edit", "Product", new { ID = product.ID });
                }
                catch (Exception e)
                {
                    SetAlert("Tạo mới thất bại!", "error");
                }
            }
            setViewBagDropDown(model.ID_Category);
            ViewBag.ID = productDAO.GetNewID();
            return View();
        }

        public ActionResult Edit(int? id)
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
            setViewBagDropDown();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product model, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    int id = model.ID;
                    model.Image = uploadImage(Image, id);
                } else
                {
                    model.Image = productDAO.Find(model.ID).Image;
                }
                try
                {
                    Product product = productDAO.Update(model);
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Edit", "Product", product.ID);
                }
                catch (Exception e)
                {
                    SetAlert("Cập nhật thất bại!", "error");
                }
            }
            setViewBagDropDown(model.ID_Category);
            model.Image = productDAO.Find(model.ID).Image;
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            productDAO.Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }

        protected void setViewBagDropDown(long? selectedValue = null)
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryList = new SelectList(dao.listAll(), "ID", "Name", selectedValue);
        }

        protected string uploadImage(HttpPostedFileBase Image, int id)
        {
            string _FileName = "";
            int index = Image.FileName.IndexOf('.');
            _FileName = "sp" + id.ToString() + "." + Image.FileName.Substring(index + 1);
            string _path = Path.Combine(Server.MapPath("~/Upload/Product"), _FileName);
            Image.SaveAs(_path);
            return _FileName;
        }
    }
}
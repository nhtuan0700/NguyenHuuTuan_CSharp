using ModelEF.Model;
using Models.DAO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Common;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        protected UserDAO userDAO;

        public UserController()
        {
            userDAO = new UserDAO();
        }

        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            string keyword = Request.Params["keyword"];
            var model = userDAO.ListWhereAll(keyword);
            ViewBag.keyword = keyword;
            ViewBag.page = page;
            return View(model.ToPagedList(page, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAccount model)
        {
            if (ModelState.IsValid)
            {
                if (userDAO.CheckUserName(model.UserName) != null)
                {
                    SetAlert("Tên đăng nhập đã tồn tại.", "warning");
                    return RedirectToAction("Create", "User");
                }
                model.Password = Encryptor.EncryptMD5(model.Password);
                try
                {
                    userDAO.Create(model);
                    SetAlert("Tạo mới thành công!", "success");
                    return RedirectToAction("Index", "User");
                }
                catch (Exception e)
                {
                    SetAlert("Tạo mới thất bại!", "error");
                }
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount user = userDAO.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserAccount model)
        {
            ModelState.Remove("UserName");
            var user = userDAO.Find(model.ID);
            if (ModelState.IsValid)
            {
                if (!model.Password.Equals(user.Password))
                {
                    model.Password = Encryptor.EncryptMD5(model.Password);
                }
                try
                {
                    var rs = userDAO.Update(model);
                    SetAlert("Cập nhật thành công!", "success");
                    return RedirectToAction("Edit", "User", rs.ID);
                }
                catch (Exception e)
                {
                    SetAlert("Cập nhật thất bại!", "error");
                }
            }
            model.UserName = user.UserName;
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            userDAO.Delete(id);
            SetAlert("Xóa thành công!", "success");
            return RedirectToAction("Index");
        }
    }
}
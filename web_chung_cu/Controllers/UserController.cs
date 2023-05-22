using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_chung_cu.Models;
using web_chung_cu.Models.Services;

namespace web_chung_cu.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService = new UserService();

        // GET: User
        public ActionResult Index(int _page = 1, int _limit = 5, string searchTerm = null)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            PaginationResult<User> result = _userService.GetListByPagination(_page - 1, _limit, searchTerm);
            List<User> users = result.Data;
            int count = result.TotalCount;

            ViewData["page"] = _page;
            ViewData["limit"] = _limit;
            ViewData["searchTerm"] = searchTerm;

            ViewData["totalPage"] = (int)Math.Ceiling((float)count / _limit);

            return View(users);
        }

        public ActionResult Add()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        //POST: Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(User _user)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (_user.avatarImage == null || _user.avatarImage.ContentLength == 0)
            {
                ModelState.AddModelError("avatarImage", "Vui lòng tải ảnh lên!");
            }

            if (ModelState.IsValid)
            {
                User user = _userService.GetOneByEmail(_user.email);
                if (user == null)
                {
                    // Lưu tệp hình ảnh trên máy chủ
                    var random = Guid.NewGuid();
                    var _extension = Path.GetExtension(_user.avatarImage.FileName);
                    string newFileName = random + _extension;
                    string _FileName = Path.GetFileName(newFileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    _user.avatarImage.SaveAs(_path);
                    _user.avatar = _FileName;

                    _userService.AddOne(_user);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(_user.email, "Email đã tồn tại!");
                    return View();
                }
            }
            return View();
        }

        public ActionResult Edit(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // Gọi phương thức GetByID của service để lấy user theo ID
            User user = _userService.GetOneById(int.Parse(id));

            // Nếu user không tồn tại, chuyển hướng người dùng đến trang 404 hoặc trang tương tự
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            // Trả về View và truyền đối tượng Apartment vào View
            return View(user);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, User _user)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                int userId = int.Parse(id);

                User user = _userService.GetOneById(userId);
                if (user != null)
                {
                    user = _userService.GetOneByEmail(_user.email);
                    if (user == null || user.id == userId)
                    {
                        if (_user.avatarImage != null && _user.avatarImage.ContentLength > 0)
                        {
                            // Lưu tệp hình ảnh trên máy chủ
                            var random = Guid.NewGuid();
                            var _extension = Path.GetExtension(_user.avatarImage.FileName);
                            string newFileName = random + _extension;
                            string _FileName = Path.GetFileName(newFileName);
                            string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                            _user.avatarImage.SaveAs(_path);
                            _user.avatar = _FileName;
                        }

                        _userService.UpdateOne(userId, _user);
                    }
                    else
                    {
                        ModelState.AddModelError(_user.email, "Email đã tồn tại!");
                        return View();
                    }
                }

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            _userService.DeleteOne(int.Parse(id));

            return RedirectToAction("Index");
        }
    }
}
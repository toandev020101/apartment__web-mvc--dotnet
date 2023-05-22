using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_chung_cu.Models;
using web_chung_cu.Models.Services;

namespace web_chung_cu.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly ApartmentService _apartmentService = new ApartmentService();

        // GET: Apartment
        public ActionResult Index(int _page = 1, int _limit = 5, string searchTerm = null, int status = -1)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            PaginationResult<Apartment> result = _apartmentService.GetListByPagination(_page - 1, _limit, searchTerm, status);
            List<Apartment> apartments = result.Data;
            int count = result.TotalCount;

            ViewData["page"] = _page;
            ViewData["limit"] = _limit;
            ViewData["searchTerm"] = searchTerm;
            ViewData["status"] = status;

            ViewData["totalPage"] = (int)Math.Ceiling((float)count/_limit);

            return View(apartments);
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
        public ActionResult Add(Apartment _apartment)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (_apartment.totalFloor <= 0)
            {
                ModelState.AddModelError("totalFloor", "Tổng số tầng phải lớn hơn 0!");
            }

            if (_apartment.totalRoom <= 0)
            {
                ModelState.AddModelError("totalRoom", "Tổng số phòng phải lớn hơn 0!");
            }

            if (_apartment.status == -1)
            {
                ModelState.AddModelError("status", "Vui lòng chọn trạng thái!");
            }

            if (ModelState.IsValid)
            {
                Apartment apartment = _apartmentService.GetOneBySlug(_apartment.slug);
                if (apartment == null)
                {
                    User user = (User)Session["user"];  
                    _apartment.userId = user.id;
                    _apartment.status = 0;
                    _apartmentService.AddOne(_apartment);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("slug", "Chung cư đã tồn tại!");
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

            // Gọi phương thức GetByID của service để lấy Apartment theo ID
            Apartment apartment = _apartmentService.GetOneById(int.Parse(id));

            // Nếu Apartment không tồn tại, chuyển hướng người dùng đến trang 404 hoặc trang tương tự
            if (apartment == null)
            {
                return RedirectToAction("Index");
            }

            // Trả về View và truyền đối tượng Apartment vào View
            return View(apartment);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Apartment _apartment)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (_apartment.totalFloor <= 0)
            {
                ModelState.AddModelError("totalFloor", "Tổng số tầng phải lớn hơn 0!");
            }

            if (_apartment.totalRoom <= 0)
            {
                ModelState.AddModelError("totalRoom", "Tổng số phòng phải lớn hơn 0!");
            }

            if (_apartment.status == -1)
            {
                ModelState.AddModelError("status", "Vui lòng chọn trang thái!");
            }

            if (ModelState.IsValid)
            {
                int apartmentId = int.Parse(id);

                Apartment apartment = _apartmentService.GetOneById(apartmentId);
                if (apartment != null)
                {
                    apartment = _apartmentService.GetOneBySlug(_apartment.slug);
                    if (apartment == null || apartment.id == apartmentId)
                    {
                        User user = (User)Session["user"];
                        _apartment.userId = user.id;
                        _apartmentService.UpdateOne(apartmentId, _apartment);
                    }
                    else
                    {
                        ModelState.AddModelError("slug", "Chung cư đã tồn tại!");
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

            _apartmentService.DeleteOne(int.Parse(id));

            return RedirectToAction("Index");
        }
    }
}
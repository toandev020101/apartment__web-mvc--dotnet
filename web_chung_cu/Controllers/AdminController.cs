using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_chung_cu.Models.Services;

namespace web_chung_cu.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApartmentService _apartmentService = new ApartmentService();
        private readonly RoomService _roomService = new RoomService();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            int apartmentCount = _apartmentService.Count();
            int roomCount = _roomService.Count();
            int roomIsRentCount = _roomService.Count(0);
            int roomNotIsRentCount = _roomService.Count(1);

            ViewData["apartmentCount"] = apartmentCount;
            ViewData["roomCount"] = roomCount;
            ViewData["roomIsRentCount"] = roomIsRentCount;
            ViewData["roomNotIsRentCount"] = roomNotIsRentCount;

            return View();
        }
    }
}
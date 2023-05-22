using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_chung_cu.Models;
using web_chung_cu.Models.Services;

namespace web_chung_cu.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoomService _roomService = new RoomService();

        public ActionResult Index()
        {
            List<Room> _rooms = _roomService.GetAll();
            ViewData["roomHots"] = _rooms.Take(10).ToList();
            ViewData["roomNews"] = _rooms.Take(6).ToList();
            ViewData["roomViews"] = _rooms.Take(8).ToList();

            return View();
        }

        public ActionResult Apartment(int _page = 1, int _limit = 6, string _sort="id-desc", int priceFrom=0, int priceTo = 0, int areaFrom = 0, int areaTo = 0)
        {
            PaginationResult<Room> result = _roomService.GetListByPagination(_page - 1, _limit, _sort, priceFrom, priceTo, areaFrom, areaTo);
            List<Room> rooms = result.Data;
            int count = result.TotalCount;

            ViewData["page"] = _page;
            ViewData["limit"] = _limit;
            ViewData["sort"] = _sort;

            ViewData["priceFrom"] = priceFrom;
            ViewData["priceTo"] = priceTo;
            ViewData["areaFrom"] = areaFrom;
            ViewData["areaTo"] = areaTo;

            ViewData["count"] = count;
            ViewData["totalPage"] = (int)Math.Ceiling((float)count / _limit);
            ViewData["rooms"] = rooms;

            return View();
        }

        public ActionResult ApartmentDetail(string slug = null)
        {
            if(slug == null)
            {
                return RedirectToAction("Apartment", "Home");
            }

            Room room = _roomService.GetOneBySlug(slug);
            ViewData["room"] = room;
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
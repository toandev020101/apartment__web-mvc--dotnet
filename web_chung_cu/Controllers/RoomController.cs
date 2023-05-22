using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_chung_cu.Models;
using web_chung_cu.Models.Services;

namespace web_chung_cu.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomService _roomService = new RoomService();
        private readonly ApartmentService _apartmentService = new ApartmentService();
        private readonly RoomImageService _roomImageService = new RoomImageService();

        // GET: Room
        public ActionResult Index(int _page = 1, int _limit = 5, string searchTerm = null, int status = -1)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            PaginationResult<Room> result = _roomService.GetListByPagination(_page - 1, _limit, searchTerm, status);
            List<Room> rooms = result.Data;
            int count = result.TotalCount;

            ViewData["page"] = _page;
            ViewData["limit"] = _limit;
            ViewData["searchTerm"] = searchTerm;
            ViewData["status"] = status;

            ViewData["totalPage"] = (int)Math.Ceiling((float)count / _limit);

            return View(rooms);
        }

        public ActionResult Add()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            List<Apartment> apartments = _apartmentService.GetList();
            ViewData["apartments"] = apartments;

            return View();
        }

        //POST: Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Room _room)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (_room.image == null || _room.image.ContentLength == 0)
            {
                ModelState.AddModelError("image", "Vui lòng tải ảnh lên!");
            }

            if (_room.libraryImage == null || _room.libraryImage.LongCount() == 0)
            {
                ModelState.AddModelError("library", "Vui lòng tải ảnh lên!");
            }

            if (_room.floorNumber <= 0)
            {
                ModelState.AddModelError("floorNumber", "Số tầng phải lớn hơn 0!");
            }

            if (_room.roomNumber <= 0)
            {
                ModelState.AddModelError("roomNumber", "Số phòng phải lớn hơn 0!");
            }

            if (_room.bedroomNumber <= 0)
            {
                ModelState.AddModelError("bedroomNumber", "Số phòng ngủ phải lớn hơn 0!");
            }

            if (_room.toletNumber <= 0)
            {
                ModelState.AddModelError("toletNumber", "Số phòng vệ sinh phải lớn hơn 0!");
            }

            if (_room.status == -1)
            {
                ModelState.AddModelError("status", "Vui lòng chọn trạng thái!");
            }

            if (_room.isInterior == -1)
            {
                ModelState.AddModelError("isInterior", "Vui lòng chọn nội thất!");
            }

            if (_room.apartmentId == -1)
            {
                ModelState.AddModelError("apartmentId", "Vui lòng chọn chung cư!");
            }

            List<Apartment> apartments = _apartmentService.GetList();
            ViewData["apartments"] = apartments;

            if (ModelState.IsValid)
            {
                Room room = _roomService.GetOneBySlug(_room.slug);
                if (room == null)
                {
                    // Lưu tệp hình ảnh trên máy chủ
                    var random = Guid.NewGuid();
                    var _extension = Path.GetExtension(_room.image.FileName);
                    string newFileName = random + _extension;
                    string _FileName = Path.GetFileName(newFileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                    _room.image.SaveAs(_path);
                    _room.imageName = _FileName;

                    int id = _roomService.AddOne(_room);

                    if(_room.status == 1)
                    {
                        _apartmentService.UpdateStatus(_room.apartmentId, 1);
                    }else
                    {
                        _apartmentService.UpdateStatus(_room.apartmentId, _roomService.checkRoomStatus(1) ? 1 : 0);
                    }

                    List<RoomImage> roomImages = new List<RoomImage>();
                    foreach (HttpPostedFileBase fileItem in _room.libraryImage)
                    {
                        // Tạo mới đối tượng RoomImage
                        RoomImage _roomImage = new RoomImage();

                        if (fileItem != null && fileItem.ContentLength > 0)
                        {
                            // Lấy tên và đường dẫn của file
                            random = Guid.NewGuid();
                            _extension = Path.GetExtension(fileItem.FileName);
                            newFileName = random + _extension;
                            _FileName = Path.GetFileName(newFileName);
                            _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                            fileItem.SaveAs(_path);

                            // Gán giá trị cho các thuộc tính của đối tượng RoomImage
                            _roomImage.path = _path;
                            _roomImage.name = _FileName;
                            _roomImage.roomId = id;

                            // Thêm đối tượng RoomImage vào mảng
                            roomImages.Add(_roomImage);
                        }
                    }

                    _roomImageService.AddAny(roomImages);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("slug", "Phòng đã tồn tại!");
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

            // Gọi phương thức GetByID của service để lấy Room theo ID
            Room room = _roomService.GetOneById(int.Parse(id));

            // Nếu room không tồn tại, chuyển hướng người dùng đến trang 404 hoặc trang tương tự
            if (room == null)
            {
                return RedirectToAction("Index");
            }

            List<Apartment> apartments = _apartmentService.GetList();
            ViewData["apartments"] = apartments;

            // Trả về View và truyền đối tượng room vào View
            return View(room);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Room _room)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (_room.floorNumber <= 0)
            {
                ModelState.AddModelError("floorNumber", "Số tầng phải lớn hơn 0!");
            }

            if (_room.roomNumber <= 0)
            {
                ModelState.AddModelError("roomNumber", "Số phòng phải lớn hơn 0!");
            }

            if (_room.bedroomNumber <= 0)
            {
                ModelState.AddModelError("bedroomNumber", "Số phòng ngủ phải lớn hơn 0!");
            }

            if (_room.toletNumber <= 0)
            {
                ModelState.AddModelError("toletNumber", "Số phòng vệ sinh phải lớn hơn 0!");
            }

            if (_room.status == -1)
            {
                ModelState.AddModelError("status", "Vui lòng chọn trạng thái!");
            }

            if (_room.isInterior == -1)
            {
                ModelState.AddModelError("isInterior", "Vui lòng chọn nội thất!");
            }

            if (_room.apartmentId == -1)
            {
                ModelState.AddModelError("apartmentId", "Vui lòng chọn chung cư!");
            }

            if (ModelState.IsValid)
            {
                int roomId = int.Parse(id);

                Room room = _roomService.GetOneById(roomId);
                if (room != null)
                {
                    room = _roomService.GetOneBySlug(_room.slug);
                    if (room == null || room.id == roomId)
                    {
                        if (_room.image != null && _room.image.ContentLength > 0)
                        {
                            // Lưu tệp hình ảnh trên máy chủ
                            var random = Guid.NewGuid();
                            var _extension = Path.GetExtension(_room.image.FileName);
                            string newFileName = random + _extension;
                            string _FileName = Path.GetFileName(newFileName);
                            string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                            _room.image.SaveAs(_path);
                            _room.imageName = _FileName;
                        }

                        _roomService.UpdateOne(roomId, _room);

                        if (_room.status == 1)
                        {
                            _apartmentService.UpdateStatus(_room.apartmentId, 1);
                        }
                        else
                        {
                            _apartmentService.UpdateStatus(_room.apartmentId, _roomService.checkRoomStatus(1) ? 1: 0);
                        }

                        if (_room.libraryImage != null && _room.libraryImage.LongCount() > 0)
                        {
                            List<RoomImage> roomImages = new List<RoomImage>();
                            foreach (HttpPostedFileBase fileItem in _room.libraryImage)
                            {
                                // Tạo mới đối tượng RoomImage
                                RoomImage _roomImage = new RoomImage();

                                if (fileItem != null && fileItem.ContentLength > 0)
                                {
                                    // Lấy tên và đường dẫn của file
                                    var random = Guid.NewGuid();
                                    var _extension = Path.GetExtension(fileItem.FileName);
                                    string newFileName = random + _extension;
                                    string _FileName = Path.GetFileName(newFileName);
                                    string _path = Path.Combine(Server.MapPath("~/Content/uploads"), _FileName);
                                    fileItem.SaveAs(_path);

                                    // Gán giá trị cho các thuộc tính của đối tượng RoomImage
                                    _roomImage.path = _path;
                                    _roomImage.name = _FileName;
                                    _roomImage.roomId = _room.id;

                                    // Thêm đối tượng RoomImage vào mảng
                                    roomImages.Add(_roomImage);
                                }
                            }

                            if(roomImages.LongCount() > 0)
                            {
                                _roomImageService.DeleteByRoomId(_room.id);
                            }
                            _roomImageService.AddAny(roomImages);
                        }

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("slug", "Phòng đã tồn tại!");
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

            int roomId = int.Parse(id);

            _roomImageService.DeleteByRoomId(roomId);
            _roomService.DeleteOne(roomId);

            return RedirectToAction("Index");
        }
    }
}
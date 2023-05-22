using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace web_chung_cu.Models.Services
{
    public class RoomService : IRoomService
    {
        private readonly DB_Entities _db = new DB_Entities();

        public PaginationResult<Room> GetListByPagination(int page, int limit, string _sort, int priceFrom, int priceTo, int areaFrom, int areaTo)
        {
            PaginationResult<Room> result = new PaginationResult<Room>();
            string[] sortBy = _sort.Split('-');
            string sortByField = sortBy[0]; // Lấy trường cần sắp xếp từ mảng sortBy
            string sortOrder = sortBy[1]; // Lấy thứ tự sắp xếp từ mảng sortBy
            bool isAscending = sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase);

            if (sortByField.Equals("id", StringComparison.OrdinalIgnoreCase))
            {
                if (isAscending)
                {
                    result.Data = _db.Rooms.OrderBy(r => r.id).Skip(page * limit).Take(limit).ToList();
                }
                else
                {
                    result.Data = _db.Rooms.OrderByDescending(r => r.id).Skip(page * limit).Take(limit).ToList();
                }
                result.TotalCount = _db.Rooms.Count();
            }else if(sortByField.Equals("price", StringComparison.OrdinalIgnoreCase)){
                if (isAscending)
                {
                    result.Data = _db.Rooms.OrderBy(r => r.price).Skip(page * limit).Take(limit).ToList();
                }else
                {
                    result.Data = _db.Rooms.OrderByDescending(r => r.price).Skip(page * limit).Take(limit).ToList();
                }
                result.TotalCount = _db.Rooms.Count();
            }else
            {
                if (isAscending)
                {
                    result.Data = _db.Rooms.OrderBy(r => r.area).Skip(page * limit).Take(limit).ToList();
                }
                else
                {
                    result.Data = _db.Rooms.OrderByDescending(r => r.area).Skip(page * limit).Take(limit).ToList();
                }
                result.TotalCount = _db.Rooms.Count();
            }

            if(priceFrom != 0 || priceTo != 0)
            {
                if(priceTo == 0)
                {
                    result.Data = _db.Rooms.Where(r => r.price > priceFrom).OrderByDescending(r => r.id).Skip(page * limit).Take(limit).ToList();
                    result.TotalCount = _db.Rooms.Where(r => r.price > priceFrom).Count();
                }
                else
                {
                    result.Data = _db.Rooms.Where(r => (r.price > priceFrom && r.price <= priceTo)).OrderByDescending(r => r.id).Skip(page * limit).Take(limit).ToList();
                    result.TotalCount = _db.Rooms.Where(r => (r.price > priceFrom && r.price <= priceTo)).Count();
                }
            }

            if (areaFrom != 0 || areaTo != 0)
            {
                if (areaTo == 0)
                {
                    result.Data = _db.Rooms.Where(r => r.area > areaFrom).OrderByDescending(r => r.id).Skip(page * limit).Take(limit).ToList();
                    result.TotalCount = _db.Rooms.Where(r => r.area > areaFrom).Count();
                }
                else
                {
                    result.Data = _db.Rooms.Where(r => (r.area > areaFrom && r.area <= areaTo)).OrderByDescending(r => r.id).Skip(page * limit).Take(limit).ToList();
                    result.TotalCount = _db.Rooms.Where(r => (r.area > areaFrom && r.area <= areaTo)).Count();
                }
            }

            return result;
        }

        public PaginationResult<Room> GetListByPagination(int page, int limit, string searchTerm, int status)
        {
            PaginationResult<Room> result = new PaginationResult<Room>();

            if (searchTerm == null && status == -1)
            {
                result.Data = _db.Rooms.OrderByDescending(r => r.id).Skip(page * limit).Take(limit).ToList();
                result.TotalCount = _db.Rooms.Count();
            }
            else
            {
                if(searchTerm != null)
                {
                    result.Data = _db.Rooms.Where(r => r.roomNumber.ToString().Contains(searchTerm)).OrderByDescending(a => a.id).Skip(page * limit).Take(limit).ToList();
                    result.TotalCount = _db.Rooms.Where(r => r.roomNumber.ToString().Contains(searchTerm)).Count();
                }

                if (status != -1)
                {
                    result.Data = _db.Rooms.Where(r => r.status == status).OrderByDescending(a => a.id).Skip(page * limit).Take(limit).ToList();
                    result.TotalCount = _db.Rooms.Where(r => r.status == status).Count();
                }
            }

            return result;
        }

        public bool checkRoomStatus(int status)
        {
            return _db.Rooms.Where(r => r.status == status).ToList().LongCount() > 0;
        }

        public List<Room> GetAll()
        {
            return _db.Rooms.ToList();
        }

        public Room GetOneBySlug(string slug)
        {
            return _db.Rooms.FirstOrDefault(r => r.slug == slug);
        }

        public Room GetOneById(int id)
        {
            return _db.Rooms.FirstOrDefault(r => r.id == id);
        }

        public int AddOne(Room room)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;

            _db.Rooms.Add(room);
            _db.SaveChanges();
            return room.id;
        }

        public void UpdateOne(int id, Room room)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;

            Room _room = _db.Rooms.FirstOrDefault(r => r.id == id);
            if (_room != null)
            {
                if (room.imageName != null && _room.imageName != room.imageName)
                {
                    string appPath = AppDomain.CurrentDomain.BaseDirectory;
                    string uploadsPath = Path.Combine(appPath, "Content/uploads");
                    string filePath = Path.Combine(uploadsPath, _room.imageName);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    _room.imageName = room.imageName;
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                _room.roomNumber = room.roomNumber;
                _room.floorNumber = room.floorNumber;
                _room.title = room.title;
                _room.slug = room.slug;
                _room.area = room.area;
                _room.price = room.price;
                _room.pricePile = room.pricePile;
                _room.description = room.description;
                _room.apartmentId = room.apartmentId;
                _room.bedroomNumber = room.bedroomNumber;
                _room.toletNumber = room.toletNumber;
                _room.isInterior = room.isInterior;
                _room.status = room.status;

                _db.SaveChanges();
            }
        }

        public void DeleteOne(int id)
        {
            Room _room = _db.Rooms.FirstOrDefault(r => r.id == id);
            if (_room != null)
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                string uploadsPath = Path.Combine(appPath, "Content/uploads");
                string filePath = Path.Combine(uploadsPath, _room.imageName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                _db.Rooms.Remove(_room);
                _db.SaveChanges();
            }
        }

        public int Count()
        {
            return _db.Rooms.Count();
        }

        public int Count(int status)
        {
            return _db.Rooms.Where(r => r.status == status).Count();
        }
    }
}
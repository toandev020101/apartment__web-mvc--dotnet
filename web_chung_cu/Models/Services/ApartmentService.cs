using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace web_chung_cu.Models.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly DB_Entities _db = new DB_Entities();

        public PaginationResult<Apartment> GetListByPagination(int page, int limit, string searchTerm, int status)
        {
            PaginationResult<Apartment> result = new PaginationResult<Apartment>();

            if (searchTerm == null && status == -1)
            {
                result.Data = _db.Apartments.OrderByDescending(a => a.id).Skip(page * limit).Take(limit).ToList();
                result.TotalCount = _db.Apartments.Count();
            }
            else
            {
                if(searchTerm != null)
                {
                    result.Data = _db.Apartments.Where(a => a.name.Contains(searchTerm)).OrderByDescending(a => a.id).Skip(page * limit).Take(limit).ToList();
                    result.TotalCount = _db.Apartments.Where(a => a.name.Contains(searchTerm)).Count();
                }

                if (status != -1)
                {
                    result.Data = _db.Apartments.Where(a => a.status == status).OrderByDescending(a => a.id).Skip(page * limit).Take(limit).ToList();
                    result.TotalCount = _db.Apartments.Where(a => a.status == status).Count();
                }
            }

            return result;
        }

        public List<Apartment> GetList()
        {
            List<Apartment> _apartments = _db.Apartments.ToList();
            List<Apartment> newApartments = new List<Apartment>();

            _apartments.ForEach(apartment =>
            {
                List<Room> _rooms = _db.Rooms.Where(r => r.apartmentId == apartment.id).ToList();

                if (apartment.totalRoom > _rooms.LongCount())
                {
                    newApartments.Add(apartment);
                }
            });

            return newApartments;
        }

        public Apartment GetOneBySlug(string slug)
        {
            return _db.Apartments.FirstOrDefault(a => a.slug == slug);
        }

        public Apartment GetOneById(int id)
        {
            return _db.Apartments.FirstOrDefault(a => a.id == id);
        }

        public void AddOne(Apartment apartment)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;

            _db.Apartments.Add(apartment);
            _db.SaveChanges();
        }

        public void UpdateOne(int id, Apartment apartment)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;

            Apartment _apartment = _db.Apartments.FirstOrDefault(a => a.id == id);
            if (_apartment != null)
            {
                // Lưu thay đổi vào cơ sở dữ liệu
                _apartment.name = apartment.name;
                _apartment.slug = apartment.slug;
                _apartment.address = apartment.address;
                _apartment.totalFloor = apartment.totalFloor;
                _apartment.totalRoom = apartment.totalRoom;
                _apartment.status = apartment.status;
                _apartment.userId = apartment.userId;

                _db.SaveChanges();
            }
        }

        public void UpdateStatus(int id, int status)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;

            Apartment _apartment = _db.Apartments.FirstOrDefault(a => a.id == id);
            if (_apartment != null)
            {
                // Lưu thay đổi vào cơ sở dữ liệu
                _apartment.status = status;

                _db.SaveChanges();
            }
        }

        public void DeleteOne(int id)
        {
            Apartment _apartment = _db.Apartments.FirstOrDefault(a => a.id == id);
            if (_apartment != null)
            {
                _db.Apartments.Remove(_apartment);
                _db.SaveChanges();
            }
        }

        public int Count()
        {
            return _db.Apartments.Count();
        }
    }
}
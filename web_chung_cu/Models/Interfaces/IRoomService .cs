using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_chung_cu.Models
{
    interface IRoomService
    {
        PaginationResult<Room> GetListByPagination(int page, int limit, string _sort, int priceFrom, int priceTo, int areaFrom, int areaTo);
        PaginationResult<Room> GetListByPagination(int page, int limit, string searchTerm, int status);

        bool checkRoomStatus(int status);

        List<Room> GetAll ();

        Room GetOneBySlug (string slug);

        Room GetOneById(int id);

        int AddOne(Room room);

        void UpdateOne(int id, Room room);

        void DeleteOne(int id);

        int Count();

        int Count(int status);
    }
}

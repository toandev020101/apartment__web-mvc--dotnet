using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_chung_cu.Models
{
    interface IApartmentService
    {
        PaginationResult<Apartment> GetListByPagination(int page, int limit, string searchTerm, int status);

        List<Apartment> GetList();

        Apartment GetOneBySlug (string slug);

        Apartment GetOneById(int id);

        void AddOne(Apartment apartment);

        void UpdateOne(int id, Apartment apartment);

        void UpdateStatus(int id, int status);

        void DeleteOne(int id);

        int Count();
    }
}

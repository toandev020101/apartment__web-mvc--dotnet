using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_chung_cu.Models
{
    interface IUserService
    {
        PaginationResult<User> GetListByPagination(int page, int limit, string searchTerm);

        User GetOneByEmail (string email);
        User GetOneByEmailAndPassword (string email, string password);
        User GetOneById(int id);

        void AddOne(User user);

        void UpdateOne(int id, User user);

        void DeleteOne(int id);

        string GetMD5(string str);
    }
}

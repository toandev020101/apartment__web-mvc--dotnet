using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace web_chung_cu.Models.Services
{
    public class UserService : IUserService
    {
        private readonly DB_Entities _db = new DB_Entities();

        public PaginationResult<User> GetListByPagination(int page, int limit, string searchTerm)
        {
            PaginationResult<User> result = new PaginationResult<User>();

            if (searchTerm == null)
            {
                result.Data = _db.Users.OrderByDescending(u => u.id).Skip(page * limit).Take(limit).ToList();
                result.TotalCount = _db.Users.Count();
            }
            else
            {
                result.Data = _db.Users.Where(u => u.FullName().ToString().Contains(searchTerm) || u.email.ToString().Contains(searchTerm)).OrderByDescending(a => a.id).Skip(page * limit).Take(limit).ToList();
                result.TotalCount = _db.Users.Where(u => u.FullName().ToString().Contains(searchTerm) || u.email.ToString().Contains(searchTerm)).Count();
            }

            return result;
        }

        public User GetOneByEmail(string email)
        {
            return _db.Users.FirstOrDefault(u => u.email == email);
        }

        public User GetOneByEmailAndPassword(string email, string password)
        {
            string f_password = GetMD5(password);
            return _db.Users.FirstOrDefault(u => u.email == email && u.password.Equals(f_password)); ;
        }

        public User GetOneById(int id)
        {
            return _db.Users.FirstOrDefault(u => u.id == id);
        }

        public void AddOne(User user)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;

            user.password = GetMD5(user.password);
            _db.Configuration.ValidateOnSaveEnabled = false;
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void UpdateOne(int id, User user)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;

            User _user = _db.Users.FirstOrDefault(u => u.id == id);
            if (_user != null)
            {
                if (user.avatar != null && _user.avatar != user.avatar)
                {
                    if(_user.avatar != null)
                    {
                        string appPath = AppDomain.CurrentDomain.BaseDirectory;
                        string uploadsPath = Path.Combine(appPath, "Content/uploads");
                        string filePath = Path.Combine(uploadsPath, _user.avatar);

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }

                    _user.avatar = user.avatar;
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                _user.firstName = user.firstName;
                _user.lastName = user.lastName;
                _user.phoneNumber = user.phoneNumber;
                _user.email = user.email;

                _db.SaveChanges();
            }
        }

        public void DeleteOne(int id)
        {
            User _user = _db.Users.FirstOrDefault(u => u.id == id);
            if (_user != null)
            {
                if(_user.avatar != null)
                {
                    string appPath = AppDomain.CurrentDomain.BaseDirectory;
                    string uploadsPath = Path.Combine(appPath, "Content/uploads");
                    string filePath = Path.Combine(uploadsPath, _user.avatar);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                _db.Users.Remove(_user);
                _db.SaveChanges();
            }
        }

        //create a string MD5
        public string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}
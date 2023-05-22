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
    public class RoomImageService : IRoomImageService
    {
        private readonly DB_Entities _db = new DB_Entities();

        public void AddAny(List<RoomImage> roomImages)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;

            _db.RoomImages.AddRange(roomImages);
            _db.SaveChanges();
        }

        public void DeleteByRoomId(int roomId)
        {
            List<RoomImage> _roomImages = _db.RoomImages.Where(ri => ri.roomId == roomId).ToList();
            if (_roomImages != null && _roomImages.LongCount() > 0)
            {
                _roomImages.ForEach(roomImage =>
                {
                    if (File.Exists(roomImage.path))
                    {
                        File.Delete(roomImage.path);
                    }
                });

                _db.RoomImages.RemoveRange(_roomImages);
                _db.SaveChanges();
            }
        }
    }
}
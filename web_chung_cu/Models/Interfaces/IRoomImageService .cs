using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_chung_cu.Models
{
    interface IRoomImageService
    {
        void AddAny(List<RoomImage> roomImages);

        void DeleteByRoomId(int roomId);
    }
}

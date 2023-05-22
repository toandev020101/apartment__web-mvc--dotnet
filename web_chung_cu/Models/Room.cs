using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web_chung_cu.Models
{
    public class Room
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string imageName { get; set; }

        [Required(ErrorMessage = "Tiêu đề không thể để trống!")]
        public string title { get; set; }

        [Required(ErrorMessage = "Đường dẫn không thể để trống!")]
        public string slug { get; set; }

        [Required(ErrorMessage = "Giá không thể để trống!")]
        public int price { get; set; }

        [Required(ErrorMessage = "Giá cọc không thể để trống!")]
        public int pricePile { get; set; }

        [Required(ErrorMessage = "Số tầng không thể để trống!")]
        public int floorNumber { get; set; }

        [Required(ErrorMessage = "Số phòng không thể để trống!")]
        public int roomNumber { get; set; }

        [Required(ErrorMessage = "Số phòng ngủ không thể để trống!")]
        public int bedroomNumber { get; set; }

        [Required(ErrorMessage = "Số phòng vệ sinh không thể để trống!")]
        public int toletNumber { get; set; }

        [Required(ErrorMessage = "Diện tích không thể để trống!")]
        public int area { get; set; }

        [Required(ErrorMessage = "Nội thất không thể để trống!")]
        public int isInterior { get; set; }

        [AllowHtml]
        public string description { get; set; }

        [Required(ErrorMessage = "Trạng thái không thể để trống!")]
        public int status { get; set; }

        [NotMapped]
        public HttpPostedFileBase image { get; set; }

        [NotMapped]
        public List<HttpPostedFileBase> libraryImage { get; set; }

        public int apartmentId { get; set; }
        public virtual Apartment apartment { get; set; }

        public virtual ICollection<RoomImage> roomImages { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace web_chung_cu.Models
{
    public class Apartment
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Tên không thể để trống!")]
        [MinLength(3, ErrorMessage = "Tên phải tối thiểu 3 ký tự!")]
        [MaxLength(45, ErrorMessage = "Tên chỉ tối đa 45 ký tự!")]
        public string name { get; set; }

        [Required(ErrorMessage = "Đường dẫn không thể để trống!")]
        [MinLength(3, ErrorMessage = "Đường dẫn phải tối thiểu 3 ký tự!")]
        [MaxLength(45, ErrorMessage = "Đường dẫn chỉ tối đa 45 ký tự!")]
        public string slug { get; set; }

        [Required(ErrorMessage = "Địa chỉ không thể để trống!")]
        [MinLength(3, ErrorMessage = "Địa chỉ phải tối thiểu 3 ký tự!")]
        public string address { get; set; }

        [Required(ErrorMessage = "Tổng số tầng không thể để trống!")]
        public int totalFloor { get; set; }

        [Required(ErrorMessage = "Tổng số phòng không thể để trống!")]
        public int totalRoom { get; set; }

        public int status { get; set; }

        public int userId { get; set; }
        public virtual User user { get; set; }

        public virtual ICollection<Room> rooms { get; set; }
    }
}
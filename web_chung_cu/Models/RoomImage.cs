using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace web_chung_cu.Models
{
    public class RoomImage
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required(ErrorMessage = "Tên không thể để trống!")]
        public string name { get; set; }

        [Required(ErrorMessage = "Đường dẫn không thể để trống!")]
        public string path { get; set; }

        public int roomId { get; set; }
        public virtual Room room { get; set; }
    }
}
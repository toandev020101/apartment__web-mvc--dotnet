using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace web_chung_cu.Models
{
    public class User
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string avatar { get; set; }

        [Required(ErrorMessage = "Họ không thể để trống!")]
        [MinLength(3, ErrorMessage = "Họ phải tối thiểu 3 ký tự!")]
        [MaxLength(45, ErrorMessage = "Họ chỉ tối đa 45 ký tự!")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Tên không thể để trống!")]
        [MinLength(3, ErrorMessage = "Tên phải tối thiểu 3 ký tự!")]
        [MaxLength(45, ErrorMessage = "Tên chỉ tối đa 45 ký tự!")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Số điện thoại không thể để trống!")]
        [MinLength(10, ErrorMessage = "Số điện thoại phải tối thiểu 10 ký tự!")]
        [MaxLength(15, ErrorMessage = "Số điện thoại chỉ tối đa 15 ký tự!")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Email không thể để trống!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không thể để trống!")]
        public string password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Nhập lại mật khẩu không thể để trống!")]
        [System.ComponentModel.DataAnnotations.Compare("password", ErrorMessage = "Mật khẩu không khớp với nhau!")]
        public string confirmPassword { get; set; }

        [NotMapped]
        public HttpPostedFileBase avatarImage { get; set; }

        public string FullName()
        {
            return this.firstName + " " + this.lastName;
        }

        public virtual ICollection<Apartment> apartments { get; set; }
    }
}
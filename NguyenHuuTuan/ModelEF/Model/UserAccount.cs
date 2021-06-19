namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("Tên Người dùng")]
        [Required(ErrorMessage = "Tên Người dùng không để trống!")]
        public string Name { get; set; }

        [StringLength(20)]
        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không để trống")]
        public string UserName { get; set; }

        [StringLength(100)]
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không để trống!")]
        public string Password { get; set; }

        public bool? Status { get; set; }
    }
}

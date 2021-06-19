namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ID { get; set; }

        [StringLength(100)]
        [DisplayName("Tên Sản phẩm")]
        [Required(ErrorMessage = "Tên Sản phẩm không để trống!")]
        public string Name { get; set; }

        [DisplayName("Đơn giá")]
        [Required(ErrorMessage = "Đơn giá không để trống!")]
        [Column(TypeName = "money")]
        [Range(1000, int.MaxValue, ErrorMessage = "Giá chưa hợp lệ")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? UnitCost { get; set; }

        [DisplayName("Số lượng")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tối thiểu là 0")]
        [Required(ErrorMessage = "Số lượng không để trống")]
        public int? Quantity { get; set; }

        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        [DisplayName("Mô tả sản phẩm")]
        public string Description { get; set; }

        public bool? Status { get; set; }

        public int? ID_Category { get; set; }

        [DisplayName("Loại Sản phẩm")]
        public virtual Category Category { get; set; }
    }
    
}

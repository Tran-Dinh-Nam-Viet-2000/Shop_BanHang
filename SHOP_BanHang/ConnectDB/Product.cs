namespace SHOP_BanHang.ConnectDB
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        //[Required(ErrorMessage = "Vui lòng nhập một số")]
        public int ID { get; set; }

        [StringLength(150)]
        [DisplayName("Tên sản phẩm")]
        //[Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string Name { get; set; }

        [DisplayName("Hình ảnh")]
        [StringLength(150)]
        //[Required(ErrorMessage = "Vui lòng tải hình ảnh lên")]
        public string Images { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập mã sản phẩm")]
        [DisplayName("Loại sản phẩm")]
        public int? CategoryId { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập giá bán")]
        [DisplayName("Giá bán")]
        public double? Price { get; set; }

        [DisplayName("Giá giảm giá")]
        public double? PriceDiscout { get; set; }

        [StringLength(100)]
        [DisplayName("Mã loại")]
        //[Range(0,4, ErrorMessage = "Mã loại được giới hạn từ 1 đến 4")]
        public string TypeId { get; set; }

        [StringLength(100)]
        [DisplayName("Mã thương hiệu")]
        public string BrandId { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập tên thương hiệu")]
        [StringLength(100)]
        [DisplayName("Thương hiệu")]
        public string NameBrand { get; set; }

        [DisplayName("Mô tả sản phẩm")]
        [StringLength(1000)]
        //[Required(ErrorMessage = "Vui lòng tải hình ảnh lên")]
        public string Description { get; set; }

        public bool? ShowOnHomePage { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(255)]
        [DisplayName("Nơi sản xuất")]
        //[Required(ErrorMessage = "Vui lòng nhập nơi sản xuất")]
        public string Origin { get; set; }

        [DisplayName("Ngày tạo")]
        [DataType(DataType.Date)]
        //[Required(ErrorMessage = "Vui lòng nhập ngày tạo")]
        public DateTime? CreateDate { get; set; }

        [DisplayName("Ngày chỉnh sửa")]
        [DataType(DataType.Date)]
        //[Required(ErrorMessage = "Vui lòng nhập ngày sửa")]
        public DateTime? UpdateDate { get; set; }


    }
}

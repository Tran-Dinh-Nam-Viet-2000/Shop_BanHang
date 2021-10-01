namespace SHOP_BanHang.ConnectDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brand")]
    public partial class Brand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Thương hiệu")]
        [StringLength(150)]
        public string Name { get; set; }

        [DisplayName("Logo")]
        [StringLength(150)]
        public string Images { get; set; }



        [DisplayName("Mã TH")]
        [StringLength(100)]
        public string BrandId { get; set; }


        [DisplayName("Ngày tạo")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Ngày sửa")]
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }

    }
}

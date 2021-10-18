namespace SHOP_BanHang.ConnectDB
{
    using SHOP_BanHang.ConnectDB.Enum_Status;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

 
    public partial class Brand
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        
        public string Images { get; set; }

        [StringLength(100)]
        public string BrandCode { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Status Status { get; set; }
    }
}

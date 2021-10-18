namespace SHOP_BanHang.ConnectDB
{
    using SHOP_BanHang.ConnectDB.Enum_Status;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public partial class Product
    {
        public int ID { get; set; }
        public string ProductCode { get; set; }

        [StringLength(150)]
        public string Name { get; set; }
        
        
        public string Images { get; set; }

        public double? Price { get; set; }
        public double? PriceDiscout { get; set; }


        [StringLength(1000)]
        public string Description { get; set; }

        public bool? ShowOnHomePage { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(255)]
        public string Origin { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreateDate { get; set; }

        [DataType(DataType.Date)]
        
        public DateTime? UpdateDate { get; set; }

        public virtual int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public Status Status { get; set; }
    }
}

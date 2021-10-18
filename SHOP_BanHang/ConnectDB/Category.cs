namespace SHOP_BanHang.ConnectDB
{
    using SHOP_BanHang.ConnectDB.Enum_Status;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public class Category
    {
        
        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryCode { get; set; }
         
        public DateTime? CreateDate { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

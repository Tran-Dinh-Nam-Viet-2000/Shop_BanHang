namespace SHOP_BanHang.ConnectDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Images { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        public bool? ShowOnHomePage { get; set; }

        public int? DisplayOrder { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? Deleted { get; set; }
    }
}

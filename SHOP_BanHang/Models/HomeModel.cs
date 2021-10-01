using SHOP_BanHang.ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHOP_BanHang.Models
{
    public class HomeModel
    {
        

        public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<Brand>  ListBrand { get; set; }
        
        public List<Product> ListDiscountProduct { get; set; }
        public List<Product> ListAppleProduct { get; set; }
        public List<Product> ListSamsungProduct { get; set; }
        public List<Product> ListXiaomiProduct { get; set; }
        public List<Product> ListLGProduct { get; set; }
        public List<Product> ListDellProduct { get; set; }
    }
}
using SHOP_BanHang.ConnectDB;
using SHOP_BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOP_BanHang.Controllers
{
    public class HomeController : Controller
    {
        DatabaseDB objDatabaseDB = new DatabaseDB();
        
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objDatabaseDB.Category.ToList(); 

            objHomeModel.ListProduct = objDatabaseDB.Product.ToList();

            objHomeModel.ListBrand = objDatabaseDB.Brand.Where(n => n.Status == ConnectDB.Enum_Status.Status.Active).ToList();

            objHomeModel.ListDiscountProduct = objDatabaseDB.Product.Take(6).ToList();

            objHomeModel.ListAppleProduct = objDatabaseDB.Product.Where(n => n.Brand.Name == "Apple" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();

            objHomeModel.ListSamsungProduct = objDatabaseDB.Product.Where(n => n.Brand.Name == "Samsung" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();

            objHomeModel.ListXiaomiProduct = objDatabaseDB.Product.Where(n => n.Brand.Name == "Xiaomi" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();

            objHomeModel.ListLGProduct = objDatabaseDB.Product.Where(n => n.Brand.Name == "LG" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();

            objHomeModel.ListDellProduct = objDatabaseDB.Product.Where(n => n.Brand.Name == "Dell" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();


            return View(objHomeModel);
        }
    }
}
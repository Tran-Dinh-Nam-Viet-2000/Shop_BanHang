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

            objHomeModel.ListBrand = objDatabaseDB.Brand.ToList();

            objHomeModel.ListDiscountProduct = objDatabaseDB.Product.Take(6).ToList();

            objHomeModel.ListAppleProduct = objDatabaseDB.Product.Where(n => n.BrandId == "AP01").ToList();

            objHomeModel.ListSamsungProduct = objDatabaseDB.Product.Where(n => n.BrandId == "SS01").ToList();

            objHomeModel.ListXiaomiProduct = objDatabaseDB.Product.Where(n => n.BrandId == "XM01").ToList();

            objHomeModel.ListLGProduct = objDatabaseDB.Product.Where(n => n.BrandId == "LG01").ToList();

            objHomeModel.ListDellProduct = objDatabaseDB.Product.Where(n => n.BrandId == "DE01").ToList();


            return View(objHomeModel);
        }
    }
}
using SHOP_BanHang.ConnectDB;
using SHOP_BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOP_BanHang.Controllers
{
    public class BrandController : Controller
    {
        DatabaseDB objDB = new DatabaseDB();
        // GET: Brand
        public ActionResult Apple()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListAppleProduct = objDB.Product.Where(n => n.Brand.Name == "Apple" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();
            return View(objHomeModel);
        }
        public ActionResult Samsung()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListSamsungProduct = objDB.Product.Where(n => n.Brand.Name == "Samsung" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();
            return View(objHomeModel);
        }
        public ActionResult Xiaomi()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListXiaomiProduct = objDB.Product.Where(n => n.Brand.Name == "Xiaomi" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();
            return View(objHomeModel);
        }
        public ActionResult LG()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListLGProduct = objDB.Product.Where(n => n.Brand.Name == "LG" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();
            return View(objHomeModel);
        }
        public ActionResult Dell()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListDellProduct = objDB.Product.Where(n => n.Brand.Name == "Dell" && n.Status == ConnectDB.Enum_Status.Status.Active).ToList();
            return View(objHomeModel);
        }
    }
}
using SHOP_BanHang.ConnectDB;
using SHOP_BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOP_BanHang.Controllers
{
    public class CategoryController : Controller
    {
        DatabaseDB objDB = new DatabaseDB();
        // GET: Category
        public ActionResult SmartPhone()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListProduct = objDB.Product.Where(n => n.Category.CategoryCode == "ĐT2").ToList();
            return View(objHomeModel);
        }
        public ActionResult Screen()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListProduct = objDB.Product.Where(n => n.Category.CategoryCode == "MH3").ToList();
            return View(objHomeModel);
        }
        public ActionResult Laptop()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListProduct = objDB.Product.Where(n => n.Category.CategoryCode == "LT4").ToList();
            return View(objHomeModel);
        }
        public ActionResult All_Items()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListProduct = objDB.Product.ToList();
            return View(objHomeModel);
        }
    }
}
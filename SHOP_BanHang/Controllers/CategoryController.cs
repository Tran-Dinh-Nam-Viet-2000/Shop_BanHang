using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOP_BanHang.ConnectDB;
using SHOP_BanHang.Models;
namespace SHOP_BanHang.Controllers
{
    public class CategoryController : Controller
    {
        DatabaseDB objDatabaseDB = new DatabaseDB();
        // GET: Category
        public ActionResult SmartPhone()
        {
            // Lấy dữ liệu từ DB lên theo điều kiện
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListProduct = objDatabaseDB.Product.Where(n => n.CategoryId == 1).ToList();

            return View(objHomeModel);
        }

        public ActionResult Screen()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListProduct = objDatabaseDB.Product.Where(n => n.CategoryId == 2).ToList();

            return View(objHomeModel);
        }

        public ActionResult Laptop()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListProduct = objDatabaseDB.Product.Where(n => n.CategoryId == 3).ToList();

            return View(objHomeModel);
        }

        public ActionResult All_Item()
        {
            HomeModel objHomeModel = new HomeModel();
      
            objHomeModel.ListProduct = objDatabaseDB.Product.ToList();

            return View(objHomeModel);
        }
    }
}
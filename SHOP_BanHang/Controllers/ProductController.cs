using SHOP_BanHang.ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOP_BanHang.Controllers
{
    public class ProductController : Controller
    {
        DatabaseDB objDB = new DatabaseDB();
        // GET: Product
        public ActionResult Details(int ID)
        {
            var productDetails = objDB.Product.Where(n => n.ID == ID).FirstOrDefault();
            return View(productDetails);
        }
    }
}
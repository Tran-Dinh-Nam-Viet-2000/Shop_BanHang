using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOP_BanHang.ConnectDB;

namespace SHOP_BanHang.Controllers
{
    public class ProductController : Controller
    {
        
        // GET: Product
        public ActionResult Details()
        {
            
            return View();
        }
    }
}
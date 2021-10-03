using SHOP_BanHang.ConnectDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOP_BanHang.Areas.Admin.Controllers
{
    public class Product_AdminController : Controller
    {
        // GET: Admin/Product_Admin

        private DatabaseDB objDB = new DatabaseDB();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListProduct()
        {
            try
            {
                var listProduct = (from list in objDB.Product.Where(n => n.ID != 1)
                                   select new
                                   {
                                       ID = list.ID,
                                       Name = list.Name,
                                       Image = list.Images,
                                       Price = list.Price,
                                       PriceDiscout = list.PriceDiscout,
                                       TypeId = list.TypeId,
                                       BrandId =list.BrandId,
                                       NameBrand = list.NameBrand,
                                       Description = list.Description,
                                       Origin = list.origin,
                                       CreateDate = list.CreateDate,
                                       UpdateDate = list.UpdateDate,
                                   });
                return Json(new { code = true, listProduct = listProduct, msg = "Lấy danh sách thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = false, msg = "Lấy danh sách thành công!" +ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
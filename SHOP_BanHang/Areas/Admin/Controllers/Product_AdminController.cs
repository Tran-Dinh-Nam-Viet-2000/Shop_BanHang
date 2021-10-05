using SHOP_BanHang.ConnectDB;
using System;
using System.Collections.Generic;
using System.IO;
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
                                       Images = list.Images,
                                       Price = list.Price,
                                       TypeId = list.TypeId,
                                       BrandId = list.BrandId,
                                       NameBrand = list.NameBrand,
                                       Description = list.Description,
                                       Origin = list.Origin,
                                       CreateDate = list.CreateDate,
                                   });
                return Json(new { code = 100, listProduct = listProduct, msg = "Lấy danh sách thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 300, msg = "Lấy danh sách thất bại!" +ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }

            [HttpPost]
            public JsonResult Create(string name, string price, string typeId, string brandId, string nameBrand, string description, string origin, string createDate, HttpPostedFileBase file)
            {
                //kiểm tra đối tượng ảnh có load lên được hay không?
                try
                {
                    var objProduct = new Product();
                    objProduct.Name = name;
                    objProduct.TypeId = typeId;
                    objProduct.BrandId = brandId;
                    objProduct.NameBrand = nameBrand;
                    objProduct.Name = description;
                    objProduct.Origin = origin;
                    objProduct.CreateDate = DateTime.Now;


                //Kiểm tra file ảnh có null hay ko, nếu không null thì tiếp tục lưu
                if (file != null)
                    {
                        //Lấy tên File
                        var fileName = Path.GetFileName(file.FileName);
                        //Cộng đường dẫn với File ảnh
                        var urlFile = "/theme/images/brand/" + fileName;
                        //Gán tên File vào đối tượng objProduct
                        objProduct.Images = urlFile;
                        //Lưu hình ảnh vào urlFile để trùng với đường dẫn trong DB
                        file.SaveAs(Server.MapPath(urlFile));
                        //Lưu và thêm giá trị vào Database
                        objDB.Product.Add(objProduct);
                        objDB.SaveChanges();
                        //Lưu thành công thì trả về "Index" trang danh sách sản phẩm
                        return Json(new { code = 200, msg = "Thêm sản phẩm thành công!" }, JsonRequestBehavior.AllowGet);
                }
               
                    
                return Json(new { code = 200, msg = "Thêm sản phẩm thành công!" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { code = 500, msg = "Thêm sản phẩm thành công!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }

            }

        [HttpGet]
        public JsonResult Details(int ID)
        {
            try
            {
                var product = objDB.Product.FirstOrDefault(n => n.ID == ID);

                return Json(new { code = 200, P = product, msg = "Thành công" }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { code = 300, msg = "Thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Details()
        {
            return View();
        }
    }
}
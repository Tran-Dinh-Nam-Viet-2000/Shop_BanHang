using Newtonsoft.Json;
using SHOP_BanHang.ConnectDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SHOP_BanHang.Areas.Admin.Controllers
{
    public class Product_AdminController : Controller
    {
        DatabaseDB objDB = new DatabaseDB();
        // GET: Admin/Product_Admin
        public ActionResult Index()
        {
            //chọn kiểu dropdownlist
            ViewBag.BrandList = objDB.Brand.Where(n => n.Status == ConnectDB.Enum_Status.Status.Active).ToList();
            ViewBag.CategoryList = objDB.Category.Where(n => n.Status == ConnectDB.Enum_Status.Status.Active).ToList();
            return View();
        }
        //Lấy dữ liệu  từ controller xong đổ ra ajax
        [HttpGet]
        public JsonResult ListProduct(string key, int pageSize, int page)
        {
            key = key.Trim();
            try
            {
               
                var product = (from p in objDB.Product.Where(n => n.Status != ConnectDB.Enum_Status.Status.Deleted && (n.Name.ToLower().Contains(key)
                                                                                                    || n.CreateDate.ToString().ToLower().Contains(key)
                                                                                                    || n.Description.ToLower().Contains(key)
                                                                                                    || n.Status.ToString().ToLower().Contains(key)
                                                                                                    || n.Price.ToString().ToLower().Contains(key)
                                                                                                    || n.PriceDiscout.ToString().ToLower().Contains(key)
                                                                                                    || n.Origin.ToString().ToLower().Contains(key)
                                                                                                    || n.Category.Name.ToString().ToLower().Contains(key)
                                                                                                    || n.Brand.Name.ToString().ToLower().Contains(key)))
                               select new
                               {
                                   ID = p.ID,
                                   Name = p.Name,
                                   Images = p.Images,
                                   CategoryId = p.Category.Name, //Lấy name trong bảng category
                                   Price = p.Price,
                                   PriceDiscout = p.PriceDiscout,
                                   NameBrand = p.Brand.Name,  //Lấy name trong bảng brand
                                   Description = p.Description,
                                   Origin = p.Origin,
                                   CreateDate = p.CreateDate,
                                   Status = p.Status
                               }).ToList();
                product = product.OrderByDescending(n => n.ID).ToList();
                var productListPage = product.Skip((page - 1) * pageSize).Take(pageSize).ToList(); //số dòng
                //tổng số trang bằng tổng mã chia cho số lượng mã 1 trang (10 / 5 = 2)
                var numberPage = product.Count % pageSize == 0 ? product.Count / pageSize : product.Count / pageSize + 1;
                var pageIndex = page; //trang
                
                return Json(new { code = 200, product = productListPage,
                    pageIndex = pageIndex,
                    pageSize = pageSize,
                    numberPage = numberPage,
                    totalRecords = product.Count,
                    msg = "Lấy danh sách sản phẩm thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách sản phẩm thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(string name, int price, int priceDiscout, string description, int categoryId, string origin, string image, int brandId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //gán giá trị cho objProduct
                    var objProduct = new Product();
                    objProduct.Name = name;
                    objProduct.Price = price;
                    objProduct.PriceDiscout = priceDiscout;
                    objProduct.Images = image;
                    objProduct.CategoryId = categoryId;
                    objProduct.BrandId = brandId;
                    objProduct.Description = description;
                    objProduct.Origin = origin;
                    objProduct.CreateDate = DateTime.Now;
                    objProduct.Status = ConnectDB.Enum_Status.Status.Active;
                    objDB.Product.Add(objProduct);
                    objDB.SaveChanges();

                    return Json(new { code = 200, msg = "Thêm mới sản phẩm thành công!" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    return Json(new { code = 300, msg = "Thêm mới sản phẩm thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { code = 500, msg = "Thất bại" }, JsonRequestBehavior.AllowGet);
        }

        //Xem thông tin sản phẩm
        [HttpGet]
        public JsonResult Details(int ID)
        {
            try
            {
                var detailsProduct = objDB.Product.FirstOrDefault(n => n.ID == ID);
                //tránh lỗi vòng lặp
                var result = JsonConvert.SerializeObject(detailsProduct, Formatting.Indented,

                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                return Json(new { code = 200, details = result, msg = "Lấy thông tin thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy thông tin thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //Tránh lỗi MaxLength khi xem details
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        //Sửa sản phẩm
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(int ID, string name, int price, int priceDiscout, string description, int categoryId, string origin, string image, int brandId)
        {
            if (ModelState.IsValid)
            {
                var product = objDB.Product.FirstOrDefault(n => n.ID == ID);
                try
                {
                    product.Name = name;
                    product.Price = price;
                    product.PriceDiscout = priceDiscout;
                    product.Description = description;
                    product.CategoryId = categoryId;
                    product.Origin = origin;
                    product.Images = image;
                    product.BrandId = brandId;
                    product.CreateDate = DateTime.Now;
                    product.Status = ConnectDB.Enum_Status.Status.Active;
                    objDB.SaveChanges();
                    return Json(new { code = 200, msg = "Chỉnh sửa thương hiệu thành công!" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { code = 300, msg = "Chỉnh sửa thương hiệu thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { code = 500, msg = "Thất bại" }, JsonRequestBehavior.AllowGet);
        }

        //Xóa logic sản phẩm, chuyển trạng thái
        [HttpPost]
        public JsonResult Delete(int? ID)
        {
            //Kiểm tra ID
            if (ID == null)
            {
                return Json(new { code = 300, msg = "ID không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }
            var delete = objDB.Product.FirstOrDefault(n => n.ID == ID);
            if(delete == null || delete.Status == ConnectDB.Enum_Status.Status.Deleted)
            {
                return Json(new { code = 400, msg = "Sản phẩm này đã bị xóa trước đó, chọn \"OK\" để tải lại trang!" }, JsonRequestBehavior.AllowGet);
            }
            delete.Status = ConnectDB.Enum_Status.Status.Deleted;
            objDB.Entry(delete).State = EntityState.Modified;
            objDB.SaveChanges();
            return Json(new { code = 200, msg = "Bạn đã xóa sản phẩm thành công!" }, JsonRequestBehavior.AllowGet);
        }



        //Trang những sản phẩm đã bị xóa
        public ActionResult Product_Deleted()
        {
            return View();
        }
        
        //List sản phẩm đã xóa
        public JsonResult ListProduct_Deleted(string key, int pageSize, int page)
        {
            key = key.Trim();
            try
            {
                var productDeleted = (from p in objDB.Product.Where(n => n.Status != ConnectDB.Enum_Status.Status.Active && (n.Name.ToLower().Contains(key)
                                                                                                    || n.CreateDate.ToString().Contains(key)
                                                                                                    || n.Description.ToLower().Contains(key)
                                                                                                    || n.Status.ToString().Contains(key)
                                                                                                    || n.Price.ToString().Contains(key)
                                                                                                    || n.PriceDiscout.ToString().Contains(key)
                                                                                                    || n.Origin.ToString().Contains(key)
                                                                                                    || n.Category.Name.ToString().Contains(key)
                                                                                                    || n.Brand.Name.ToString().Contains(key)))
                                       select new
                                       {
                                           ID = p.ID,
                                           Name = p.Name,
                                           Images = p.Images,
                                           CategoryId = p.Category.Name, //Lấy name trong bảng category
                                           Price = p.Price,
                                           PriceDiscout = p.PriceDiscout,
                                           NameBrand = p.Brand.Name,  //Lấy name trong bảng brand
                                           Description = p.Description,
                                           Origin = p.Origin,
                                           CreateDate = p.CreateDate,
                                           Status = p.Status
                                       }).ToList();
                var productListPage = productDeleted.Skip((page - 1) * pageSize).Take(pageSize).ToList(); //số dòng
                //tổng số trang bằng tổng mã chia cho số lượng mã 1 trang (10 / 5 = 2)
                var numberPage = productDeleted.Count % pageSize == 0 ? productDeleted.Count / pageSize : productDeleted.Count / pageSize + 1;
                var pageIndex = page; //trang
                return Json(new
                {
                    code = 200,
                    productDeleted = productListPage,
                    pageIndex = pageIndex,
                    pageSize = pageSize,
                    numberPage = numberPage,
                    totalRecords = productDeleted.Count,
                    msg = "Lấy danh sách sản phẩm đã xóa thành công!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách sản phẩm đã xóa thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Xóa vĩnh viễn sản phẩm
        [HttpPost]
        public JsonResult DeleteData(int? ID)
        {
            //Kiểm tra ID
           if (ID == null)
           {
                return Json(new { code = 300, msg = "ID không hợp lệ!" }, JsonRequestBehavior.AllowGet);
           }
            var deletedData = objDB.Product.FirstOrDefault(n => n.ID == ID);
            if (deletedData == null || deletedData.Status != ConnectDB.Enum_Status.Status.Deleted)
            {
                return Json(new { code = 500, msg = "Sản phẩm đã được xóa vĩnh viễn trước đó, chọn \"OK\" để tải lại trang!" }, JsonRequestBehavior.AllowGet);
            }
            //Nếu không có lỗi gì thì gán lại giá trị và xóa vv
            deletedData.Status = ConnectDB.Enum_Status.Status.Deleted;
            objDB.Product.Remove(deletedData);
            objDB.SaveChanges();
            return Json(new { code = 200, msg = "Sản phẩm đã được xóa vĩnh viễn thành công!" }, JsonRequestBehavior.AllowGet);
        }

        //Khôi phục sản phẩm
        [HttpPost]
        public JsonResult RestoreData(int? ID)
        {
            if(ID == null)
            {
                return Json(new { code = 300, msg = "ID không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }
            var restore = objDB.Product.FirstOrDefault(n => n.ID == ID);
            if(restore == null || restore.Status != ConnectDB.Enum_Status.Status.Deleted)
            {
                return Json(new { code = 500, msg = "Sản phẩm đã được khôi phục trước đó, chọn \"OK\" để tải lại trang!" }, JsonRequestBehavior.AllowGet);
            }
            restore.Status = ConnectDB.Enum_Status.Status.Active;
            objDB.Entry(restore).State = EntityState.Modified;
            objDB.SaveChanges();

            return Json(new { code = 200, msg = "Sản phẩm đã được khôi phục thành công!" }, JsonRequestBehavior.AllowGet);
        }
    }
}
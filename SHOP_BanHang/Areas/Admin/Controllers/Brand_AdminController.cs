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
    public class Brand_AdminController : Controller
    {
        DatabaseDB objDB = new DatabaseDB();
        // GET: Admin/Brand_Admin
        public ActionResult Index()
        {
            return View();
        }

        //Lấy dữ liệu từ controller xong đổ ra ajax
        [HttpGet]
        public JsonResult ListBrand(string key, int pageSize, int page)
        {
            key = key.Trim();
            try
            {

                var list = (from c in objDB.Brand.Where(n => n.Status != ConnectDB.Enum_Status.Status.Deleted && (n.Name.ToLower().Contains(key)
                                                                                                    || n.CreatedDate.ToString().Contains(key)
                                                                                                    || n.BrandCode.ToLower().Contains(key)
                                                                                                    || n.Status.ToString().Contains(key)))
                            select new
                            {
                                ID = c.ID,
                                Name = c.Name,
                                Images = c.Images,
                                BrandCode = c.BrandCode,
                                CreatedDate = c.CreatedDate,
                                Status = c.Status
                            }).ToList();
                var brandListPage = list.Skip((page - 1) * pageSize).Take(pageSize).ToList(); //số dòng
                //tổng số trang bằng tổng mã chia cho số lượng mã 1 trang (10 / 5 = 2)
                var numberPage = list.Count % pageSize == 0 ? list.Count / pageSize : list.Count / pageSize + 1;
                var pageIndex = page; //trang
                return Json(new { code = 200, listBrand = brandListPage,
                    pageIndex = pageIndex,
                    pageSize = pageSize,
                    totalRecords = list.Count,
                    numberPage = numberPage, msg = "Lấy danh sách thương hiệu thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy danh sách thương hiệu thất bại!" + ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        //Thêm thương hiệu
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(string name, string brandCode, string image)
        {
            //Lấy id cuối để cộng với dữ liệu nhập để thành mã thương hiệu
            var lastId = 0;
            if (objDB.Brand.ToList().Count > 0) //nếu id lớn hơn 0 là có dữ liệu
            {
                for (int i = 0; i < objDB.Brand.ToList().Count; i++)
                {
                    if (i == objDB.Brand.ToList().Count - 1)
                    {
                        lastId = objDB.Brand.ToList()[i].ID + 1; //id cuối cùng + thêm 1
                    }
                }
            }
            else //không có dữ liệu
            {
                lastId = 1;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var objBrand = new Brand();
                    objBrand.Name = name;
                    objBrand.BrandCode = brandCode + lastId; //cộng tên và Id tương ứng
                    objBrand.Images = image;
                    objBrand.CreatedDate = DateTime.Now;
                    objBrand.Status = ConnectDB.Enum_Status.Status.Active;
                    objDB.Brand.Add(objBrand);
                    objDB.SaveChanges();

                    return Json(new { code = 200, msg = "Thêm thương hiệu thành công" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { code = 500, msg = "Thêm thương hiệu thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { code = 400, msg = "Thất bại" }, JsonRequestBehavior.AllowGet);
        }

        //Xem thương hiệu
        [HttpGet]
        public JsonResult Details(int ID)
        {
            try
            {
                var detailsBrand = objDB.Brand.FirstOrDefault(n => n.ID == ID);
                var result = JsonConvert.SerializeObject(detailsBrand, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                return Json(new { code = 200, details = result, msg = "Lấy chi tiết thương hiệu thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lấy chi tiết thương hiệu thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //Tránh lỗi MaxLength cho Details
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

        //Chỉnh sửa thương hiệu
        [HttpPost]
        public JsonResult Edit(int ID, string name, string brandCode, string image)
        {
            if(ModelState.IsValid)
            {
                var brand = objDB.Brand.FirstOrDefault(n => n.ID == ID);
                try
                {
                    brand.Name = name;
                    brand.BrandCode = brandCode;
                    brand.Images = image;
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

        //Xóa thương hiệu
        [HttpPost]
        public JsonResult Delete(int? ID)
        {
            //kiểm tra id có null hay không
            if (ID == null)
            {
                return Json(new { code = 300, msg = "ID không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }
            //nếu ko null thì tìm Id
            var brand = objDB.Brand.FirstOrDefault(n => n.ID == ID);
            //nếu không brand và brand status là deleted thì thông báo đã xóa
            if (brand == null || brand.Status == ConnectDB.Enum_Status.Status.Deleted)
            {
                return Json(new { code = 400, msg = "Thương hiệu này đã bị xóa trước đó, chọn \"OK\" để tải lại trang!" }, JsonRequestBehavior.AllowGet);
            }
            //Nếu không có lỗi gì thì sẽ gán lại Status
            brand.Status = ConnectDB.Enum_Status.Status.Deleted;
            //Đổi Active thành Deleted
            objDB.Entry(brand).State = EntityState.Modified;
            //lưu
            objDB.SaveChanges();
            return Json(new { code = 200, msg = "Xóa thương hiệu thành công!" }, JsonRequestBehavior.AllowGet);
        }



        //Trang những thương hiệu đã xóa
        public ActionResult Brand_Deleted()
        {
            return View();
        }
        //List những thương hiệu đã xóa
        [HttpGet]
        public JsonResult ListBrand_Deleted(string key)
        {
            //cắt khoảng trống đầu và cuối
            key = key.Trim();
            try
            {
                var listDeleted = (from d in objDB.Brand.Where(n => n.Status == ConnectDB.Enum_Status.Status.Deleted && (n.Name.ToLower().ToString().Contains(key) 
                                                                                                                         ||n.BrandCode.ToLower().ToString().Contains(key)
                                                                                                                         ||n.Status.ToString().Contains(key)
                                                                                                                         ||n.CreatedDate.ToString().Contains(key)))
                                   select new
                                   {
                                       ID = d.ID,
                                       Name = d.Name,
                                       BrandCode = d.BrandCode,
                                       Images = d.Images,
                                       CreatedDate = d.CreatedDate,
                                       Status = d.Status
                                   }).ToList();
                return Json(new { code = 200, listBrandDeleted = listDeleted, msg = "Lấy thương hiệu đã xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy danh sách thương hiệu đã xóa thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }

        //Xóa ở thương hiệu vĩnh viễn
        [HttpPost]
        public JsonResult DeletedData(int? ID)
        {
            if (ID == null)
            {
                return Json(new { code = 300, msg = "ID không hợp lệ" }, JsonRequestBehavior.AllowGet);
            }
            var deletedData = objDB.Brand.SingleOrDefault(n => n.ID == ID);
            if(deletedData == null || deletedData.Status != ConnectDB.Enum_Status.Status.Deleted)
            {
                return Json(new { code = 500, msg = "Thương hiệu này đã được xóa vĩnh viễn trước đó, chọn \"OK\" để tải lại trang!" });
            }
            deletedData.Status = ConnectDB.Enum_Status.Status.Deleted;
            objDB.Brand.Remove(deletedData);
            objDB.SaveChanges();
            return Json(new { code = 200, msg = "Bạn đã xóa thương hiệu vĩnh viễn thành công!" }, JsonRequestBehavior.AllowGet);
        }


        //Khôi phục thương hiệu
        [HttpPost]
        public JsonResult RestoreDeleted(int? ID)
        {
            //Lấy ID
            var brand = objDB.Brand.FirstOrDefault(n => n.ID == ID);
            //Nếu id = null và status khác deleted thì thông báo cho admin
            if(brand == null || brand.Status != ConnectDB.Enum_Status.Status.Deleted)
            {
                return Json(new { code = 300, msg = "Thương hiệu đã khôi phục trước đó, chọn \"Ok\" để tải lại trang!" });
            }
            //Nếu không có lỗi thì sẽ gán lại Status
            brand.Status = ConnectDB.Enum_Status.Status.Active;
            try
            {
                //Chuyển đổi status (Deleted -> Active)
                objDB.Entry(brand).State = EntityState.Modified;
                objDB.SaveChanges();
                return Json(new { code = 200, msg = "Thương hiệu đã được khôi phục thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Bạn đã khôi phục thương hiệu thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
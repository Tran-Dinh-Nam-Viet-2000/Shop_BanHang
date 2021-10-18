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
    public class Category_AdminController : Controller
    {
        private DatabaseDB objDB = new DatabaseDB();
        // GET: Admin/Category_Admin
        public ActionResult Index()
        {
            return View();
        }

        //Lấy mã sản phẩm từ DB lên
        [HttpGet]
        public JsonResult ListCategory(string key, int pageSize, int page)
        {
            key = key.Trim();
            try
            {
                var listCategory = (from list in objDB.Category
                                    .Where(n => n.Status != ConnectDB.Enum_Status.Status.Deleted && (n.CategoryCode.ToLower().Contains(key)
                                                                                                    || n.CreateDate.ToString().Contains(key)
                                                                                                    || n.Name.ToLower().Contains(key)
                                                                                                    || n.Status.ToString().Contains(key)))
                                    select new
                                    {
                                        ID = list.ID,
                                        Name = list.Name,
                                        CategoryCode = list.CategoryCode,
                                        CreateDate = list.CreateDate,
                                        Status = list.Status
                                    }).ToList();
                var categoryListPage = listCategory.Skip((page - 1) * pageSize).Take(pageSize).ToList(); //số dòng
                //tổng số trang bằng tổng mã chia cho số lượng mã 1 trang (10 / 5 = 2)
                var numberPage = listCategory.Count % pageSize == 0 ? listCategory.Count / pageSize : listCategory.Count/pageSize + 1; 
                var pageIndex = page; //trang
                return Json(new { code = 100, listCategory = categoryListPage, pageIndex = pageIndex,pageSize = pageSize,
                    totalRecords = listCategory.Count, numberPage = numberPage, msg = "Lấy danh sách thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 300, msg = "Lấy danh sách thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Thêm mới mã sản phẩm
        [HttpPost]
        public JsonResult Create(string name, string categoryCode)
        {
            //Lấy id
            var lastId = 0;
            if (objDB.Category.ToList().Count > 0) //nếu id lớn hơn 0 là có dữ liệu
            {
                for (int i = 0; i < objDB.Category.ToList().Count; i++)
                {
                    if (i == objDB.Category.ToList().Count - 1)
                    {
                        lastId = objDB.Category.ToList()[i].ID + 1; //id cuối cùng + thêm 1
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
                    var objCategory = new Category();
                    objCategory.Name = name;
                    objCategory.CategoryCode = categoryCode + lastId; //cộng tên và Id tương ứng
                    objCategory.CreateDate = DateTime.Now;
                    objCategory.Status = ConnectDB.Enum_Status.Status.Active;
                    objDB.Category.Add(objCategory);
                    objDB.SaveChanges();

                    return Json(new { code = 100, msg = "Thêm mã sản phẩm thành công" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { code = 500, msg = "Thêm mã sản phẩm thất bại" + ex.Message}, JsonRequestBehavior.AllowGet);
                }
                
            }
            return Json(new { code = 400, msg = "Thất bại" }, JsonRequestBehavior.AllowGet);
        }
        //Xem chi tiết mã sản phẩm
        [HttpGet]
        public JsonResult Details(int ID)
        {
            try
            {
                var category = objDB.Category.FirstOrDefault(n => n.ID == ID);
                var result = JsonConvert.SerializeObject(category, Formatting.Indented,

                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                return Json(new { code = 100, C = result, msg = "Lấy thông tin mã sản phẩm thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 300, msg = "Lấy thông tin mã sản phẩm thất bại!" + ex.Message}, JsonRequestBehavior.AllowGet);
              
            }
        }
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
    
    //Chỉnh sửa mã sản phẩm
    [HttpPost]
        public JsonResult Edit(int ID, string name, string categoryCode)
        {
            
            if (ModelState.IsValid)
            {
                var category = objDB.Category.SingleOrDefault(n => n.ID == ID);
                try
                {
                    category.Name = name;
                    category.CategoryCode = categoryCode;
                    objDB.SaveChanges();
                    return Json(new { code = 200, msg = "Chỉnh sửa mã sản phẩm thành công!"}, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    return Json(new { code = 300, msg = "Chỉnh sửa tin mã sản phẩm thất bại!" + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { code = 500, msg = "Thất bại" }, JsonRequestBehavior.AllowGet);
        }
        //Xóa mã sản phẩm
        [HttpPost]
        public JsonResult Delete(int? ID)
        {
            if (ID == null) //kiểm tra ID có null không, nếu null thì trả về message tránh trường hợp lỗi trang
            {
                return Json(new { code = 300, msg = "ID không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }
            var category = objDB.Category.FirstOrDefault(n => n.ID == ID);  //tìm giá trị ID  đầu tiên
            if (category == null || category.Status == ConnectDB.Enum_Status.Status.Deleted) //nếu bằng null hoặc status = deleted thì trả về lỗi
            {
                return Json(new { code = 400, msg = "Loại sản phẩm đã được xóa trước đó, nhấn \"Ok\" để tải lại trang!" }, JsonRequestBehavior.AllowGet);
            }
            //Nếu không có lỗi thì sẽ gán lại Status
            category.Status = ConnectDB.Enum_Status.Status.Deleted;
            //Đổi Active thành Deleted
            objDB.Entry(category).State = EntityState.Modified;
            //Lưu vào DB
            objDB.SaveChanges();
            return Json(new { code = 200, msg = "Bạn đã xóa thành công!" }, JsonRequestBehavior.AllowGet);
        }

        

        ////////Trang những mã đã bị xóa
        public ActionResult Category_Deleted()
        {
            return View();
        }
        //List những sản phẩm đã bị xóa và tìm kiếm
        [HttpGet]
        public JsonResult ListCategoryDeleted(string find, int pageSize, int page)
        {
            try
            {
                find = find.Trim();
                var listDeleted = (from d in objDB.Category.Where(n => n.Status == ConnectDB.Enum_Status.Status.Deleted && (n.CategoryCode.ToLower().Contains(find)
                                                                                                    || n.CreateDate.ToString().Contains(find)
                                                                                                    || n.Name.ToLower().Contains(find)
                                                                                                    || n.Status.ToString().Contains(find)))
                                   select new
                                   {
                                       ID = d.ID,
                                       Name = d.Name,
                                       CategoryCode = d.CategoryCode,
                                       CreateDate = d.CreateDate,
                                       Status = d.Status
                                   }).ToList();
                var listCategoryDeleted = listDeleted.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var numberPage = listDeleted.Count % pageSize == 0 ? listDeleted.Count/pageSize : listDeleted.Count/pageSize + 1;
                var pageIndex = page;
                return Json(new { code = 100, deleted = listCategoryDeleted, numberPage = numberPage, pageIndex = pageIndex, pageSize = pageSize,
                    totalRecord = listDeleted.Count, msg = "Lấy danh sách đã xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Lấy danh sách đã xóa thất bại" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }

        //Xóa khỏi Database
        [HttpPost]
        public JsonResult DeleteData(int? ID)
        {
            if (ID == null) //kiểm tra ID có null không, nếu null thì trả về message tránh trường hợp lỗi trang
            {
                return Json(new { code = 300, msg = "ID không hợp lệ!" }, JsonRequestBehavior.AllowGet);
            }
            var category = objDB.Category.FirstOrDefault(n => n.ID == ID);  //tìm giá trị ID  đầu tiên
            if (category == null || category.Status != ConnectDB.Enum_Status.Status.Deleted) //nếu bằng null hoặc status = deleted thì trả về lỗi
            {
                return Json(new { code = 400, msg = "Loại sản phẩm đã được khôi phục trước đó, nhấn \"Ok\" để tải lại trang!" }, JsonRequestBehavior.AllowGet);
            }
            //Nếu không có lỗi thì sẽ gán lại Status
            category.Status = ConnectDB.Enum_Status.Status.Deleted;
            //Xóa ở DB
            objDB.Category.Remove(category);
            //Lưu vào DB
            objDB.SaveChanges();
            return Json(new { code = 200, msg = "Bạn đã xóa loại sản phẩm vĩnh viễn thành công!" }, JsonRequestBehavior.AllowGet);
        }

        //khôi phục loại sản phẩm đã bị xóa
        [HttpPost]
        public JsonResult RestoreDeleted(int? ID)
        {
            var category = objDB.Category.FirstOrDefault(n => n.ID == ID);  //tìm giá trị ID  đầu tiên
            //nếu id bằng null hoặc status khác deleted thì trả về message
            //Trường hợp là để dùng 2 tab khôi phục 1 mã sp
            if (category == null || category.Status != ConnectDB.Enum_Status.Status.Deleted)
            {
                return Json(new { code = 400, msg = "Loại sản phẩm đã được khôi phục trước đó, nhấn \"Ok\" để tải lại trang!" }, JsonRequestBehavior.AllowGet);
            }
            //Nếu không có lỗi thì sẽ gán lại Status
            category.Status = ConnectDB.Enum_Status.Status.Active;
            try
            {
                //Chuyển Deleted sang Active
                objDB.Entry(category).State = EntityState.Modified;
                //Lưu vào DB
                objDB.SaveChanges();
                return Json(new { code = 200, msg = "Bạn đã khôi phục thành công!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 100, msg = "Bạn đã khôi phục thất bại!" + ex.Message}, JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}
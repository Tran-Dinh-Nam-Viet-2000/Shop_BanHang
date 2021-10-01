using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SHOP_BanHang
{
    public class HelperCommon
    {
        [NonAction]
        //Path đường dẫn của table
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }
            return new SelectList(list, "Value", "Text");
        }

        //Chuyển đổi danh sách sang bảng dữ liệu
        //Thay vì nhập cứng vào phần Create thì hàm này sẽ giúp xổ ra bảng dữ liệu từ DB để dễ chọn hơn
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Lấy tất cả thuộc tính
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo props in Props)
                {
                    //đặt tên cột làm tên thuộc tính
                    dataTable.Columns.Add(props.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //chèn các giá trị thuộc tính vào các hàng có dữ liệu
                        values[i] = Props[i].GetValue(item, null);
                    }
                    //Thêm giá trị vào bảng
                    dataTable.Rows.Add(values);
                }
                return dataTable;
            }

        }


    }
}
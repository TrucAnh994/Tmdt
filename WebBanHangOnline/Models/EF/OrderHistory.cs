using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public List<string> ProductNames { get; set; } // Danh sách tên sản phẩm
    }

}
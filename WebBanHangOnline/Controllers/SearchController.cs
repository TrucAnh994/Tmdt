using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Results(string searchString)
        {
            // Logic to search for products based on searchString
            var products = db.Products
                             .Where(p => p.Title.Contains(searchString) || p.Description.Contains(searchString))
                             .ToList();

            return View(products); // Trả về view Results.cshtml với danh sách sản phẩm tìm được
        }

    }
}
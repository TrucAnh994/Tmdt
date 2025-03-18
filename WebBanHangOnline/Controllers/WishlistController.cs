using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        // GET: Wishlist
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Wishlist> items = db.Wishlists.Where(x => x.UserName == User.Identity.Name).OrderByDescending(x => x.CreatedDate);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostWishlist(int ProductId)
        {
            if (!Request.IsAuthenticated)
            {
                return Json(new { Success = false, Message = "Bạn chưa đăng nhập." });
            }

            var userName = User.Identity.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return Json(new { Success = false, Message = "Không xác định được tên người dùng." });
            }

            var checkItem = db.Wishlists.FirstOrDefault(x => x.ProductId == ProductId && x.UserName == userName);
            if (checkItem != null)
            {
                return Json(new { Success = false, Message = "Sản phẩm đã được yêu thích rồi." });
            }

            var item = new Wishlist
            {
                ProductId = ProductId,
                UserName = userName,
                CreatedDate = DateTime.Now
            };

            try
            {
                db.Wishlists.Add(item);
                db.SaveChanges();
                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                // Logging exception (you can use any logging framework)
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return Json(new { Success = false, Message = "Có lỗi xảy ra khi lưu dữ liệu." });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostDeleteWishlist(int ProductId)
        {
            var checkItem = db.Wishlists.FirstOrDefault(x => x.ProductId == ProductId && x.UserName == User.Identity.Name);
            if (checkItem != null)
            {
                var item = db.Wishlists.Find(checkItem.Id);
                db.Set<Wishlist>().Remove(item);
                var i = db.SaveChanges();
                return Json(new { Success = true, Message = "Xóa thành công." });
            }
            return Json(new { Success = false, Message = "Xóa thất bại." });
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
using Do_An_Wed.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Areas.Admin.Controllers
{
    public class KhachhangController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        ///quản lý khách hàng
        #region QUAN LY TAI KHOAN KHACH HANG
        public ActionResult QLKhachhang(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            return View(data.KHACHHANGs.Where(n => n.MaRole == 0).ToList().ToPagedList(pageNumber, pageSize));
        }
        //Xóa khách hàng
        [HttpGet]
        public ActionResult Xoatkkh(int id)
        {
            KHACHHANG tk = data.KHACHHANGs.Where(n => n.MaRole == 0).SingleOrDefault(n => n.MaKH == id);
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaKH = tk.MaKH;
            return View(tk);
        }
        [HttpPost, ActionName("Xoatkkh")]
        public ActionResult AcXoatkkh(int id)
        {
            KHACHHANG tk = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaKH = tk.MaKH;
            data.KHACHHANGs.DeleteOnSubmit(tk);
            data.SubmitChanges();
            return RedirectToAction("QLKhachhang");
        }
        //chi tiết khách hàng

        public ActionResult Chitietkh(int id)
        {
            KHACHHANG kh = data.KHACHHANGs.Where(n => n.MaRole == 0).SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MAKH = kh.MaKH;
            return View(kh);
        }
        /// sửa khach hàng
        [HttpGet]
        public ActionResult Suakh(int id)
        {
            ViewBag.maQuyen = new SelectList(data.Roles.ToList().OrderBy(n => n.MaRole), "MaRole", "TenRole");
            KHACHHANG tk = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tk);
        }
        [HttpPost, ActionName("Suakh")]
        [ValidateInput(false)]
        public ActionResult savekh(int id)
        {
            KHACHHANG tk = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            UpdateModel(tk);
            data.SubmitChanges();
            return RedirectToAction("QLKhachhang");
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Do_An_Wed.Models;
using PagedList;

namespace Do_An_Wed.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            List<SANPHAM> list = data.SANPHAMs.ToList();
            int v = list.Count();
            List<DANHMUC> dm = data.DANHMUCs.ToList();
            int d = dm.Count();
            List<THUONGHIEU> listth = data.THUONGHIEUs.ToList();
            int t = listth.Count();
            List<KHACHHANG> TK = data.KHACHHANGs.Where(n => n.MaRole == 0).ToList();
            List<KHACHHANG> TKad = data.KHACHHANGs.Where(n => n.MaRole == 1).ToList();
            ViewBag.sanpham = v;
            ViewBag.dm = d;
            ViewBag.thuonghieu = t;
            ViewBag.khachhang = TK.Count();
            ViewBag.Admin = TKad.Count();
            
            return View();
        }
     
        ///quản lý admin

        #region QUẢN LÝ THÔNG TIN ADMIN
        public ActionResult QLAdmin(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            return View(data.KHACHHANGs.Where(n => n.MaRole == 1).ToList().ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult themad()
        {

            ViewBag.MaQuyen = new SelectList(data.Roles.ToList().OrderBy(n => n.MaRole), "MaRole", "TenRole");
            return View();
        }
        [HttpPost]
        
        public ActionResult Themad(KHACHHANG taikhoa)
        {
            if (string.IsNullOrEmpty(taikhoa.tentk))
            {
                ViewBag.ThongBao = "Bạn cần nhập tên thương hiệu";
                return View(taikhoa.tentk);
            }
            KHACHHANG brand = data.KHACHHANGs.FirstOrDefault(b => b.tentk == taikhoa.tentk);
            if (brand != null)
            {
                ViewBag.ThongBao = "Tên thương hiệu đã tồn tại";
                return View(taikhoa);
            }
            ViewBag.MaQuyen = new SelectList(data.Roles.ToList().OrderBy(n => n.MaRole), "MaRole", "TenRole");
            data.KHACHHANGs.InsertOnSubmit(taikhoa);
            data.SubmitChanges();
            return RedirectToAction("QLAdmin");
        }
        [HttpGet]
        public ActionResult Suaad(int id)
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
        [HttpPost, ActionName("Suaad")]
        [ValidateInput(false)]
        public ActionResult savead(int id)
        {
            KHACHHANG tk = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            UpdateModel(tk);
            data.SubmitChanges();
            return RedirectToAction("QLAdmin");
        }
        [HttpGet]
        public ActionResult Xoaad(int id)
        {
            KHACHHANG tk = data.KHACHHANGs.Where(n => n.MaRole == 1).SingleOrDefault(n => n.MaKH == id);
            if (tk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaKH = tk.MaKH;
            return View(tk);
        }
        [HttpPost, ActionName("Xoaad")]
        public ActionResult upxoaad(int id)
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
            return RedirectToAction("QLAdmin");
        }
        //chi tiết khách hàng

        public ActionResult Chitietad(int id)
        {
            KHACHHANG kh = data.KHACHHANGs.Where(n => n.MaRole == 1).SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MAKH = kh.MaKH;
            return View(kh);
        }

     
        #endregion



    }
}
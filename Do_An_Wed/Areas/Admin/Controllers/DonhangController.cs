using Do_An_Wed.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Areas.Admin.Controllers
{
    public class DonhangController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: Admin/Donhang
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.DONHANGs.ToList().OrderBy(n => n.MaDH).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult SuaDH(int id)
        {

            DONHANG sanpham = data.DONHANGs.SingleOrDefault(n => n.MaDH == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaDH(DONHANG sanpham)
        {
            DONHANG sp = data.DONHANGs.FirstOrDefault(x => x.MaDH == sanpham.MaDH);
            sp.NgayDH = sanpham.NgayDH;
            sp.Ngaygiao = sanpham.Ngaygiao;
            sp.Trangthai = sanpham.Trangthai;          
            UpdateModel(sp);
            data.SubmitChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult XoaDH(int id)
        {
            var sanpham = data.DONHANGs.First(n => n.MaDH == id);
            ViewBag.MaSP = sanpham.MaDH;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost, ActionName("XoaDH")]
        public ActionResult XacnhanxoaDH(int id, FormCollection collection)
        {
            var sanpham = data.DONHANGs.SingleOrDefault(n => n.MaDH == id);
            ViewBag.MaSP = sanpham.MaDH;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.DONHANGs.DeleteOnSubmit(sanpham);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChitietDH(int id)
        {
            DONHANG thuonghieu = data.DONHANGs.SingleOrDefault(n => n.MaDH == id);
            ViewBag.MaTH = thuonghieu.MaDH;
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(thuonghieu);
        }

    }
}
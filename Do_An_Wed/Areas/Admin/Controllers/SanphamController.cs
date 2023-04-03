using Do_An_Wed.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Areas.Admin.Controllers
{
    public class SanphamController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        /// quản lý sản phẩm

        #region QUẢN LÝ SẢN PHẨM
        public ActionResult QLSanpham(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        }
        // the moi san pham
        [HttpGet]
        public ActionResult Themmoisp()
        {
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisp(SANPHAM sanpham, HttpPostedFileBase uploadhinh)
        {

            data.SANPHAMs.InsertOnSubmit(sanpham);
            data.SubmitChanges();
            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                int id = int.Parse(data.SANPHAMs.ToList().Last().MaSP.ToString());

                string _FileName = "";
                int index = uploadhinh.FileName.IndexOf('.');
                _FileName = "themsp" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/Content/img"), _FileName);
                uploadhinh.SaveAs(_path);

                SANPHAM unv = data.SANPHAMs.FirstOrDefault(x => x.MaSP == id);
                unv.HinhanhSP = _FileName;
                data.SubmitChanges();
            }
            return RedirectToAction("QLSanpham");

        }

        //hien thi san pham
        public ActionResult Chitietsp(int id)
        {
            SANPHAM sanpham = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        //xoa san pham
        [HttpGet]
        public ActionResult Xoasp(int id)
        {
            SANPHAM sanpham = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost, ActionName("Xoasp")]
        public ActionResult Xacnhanxoa(int id)
        {
            SANPHAM sanpham = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SANPHAMs.DeleteOnSubmit(sanpham);
            data.SubmitChanges();
            return RedirectToAction("QLSanpham");
        }
        //chỉnh sửa sản phẩm
        // chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult Suasp(int id)
        {

            SANPHAM sanpham = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.MaTH), "MaTH", "TenTH");
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.MaDM), "MaDM", "TenDM");
            return View(sanpham);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasp(SANPHAM sanpham, HttpPostedFileBase uploadhinh)
        {
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.MaTH), "MaTH", "TenTH");
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.MaDM), "MaDM", "TenDM");
            SANPHAM sp = data.SANPHAMs.FirstOrDefault(x => x.MaSP == sanpham.MaSP);
            sp.TenSP = sanpham.TenSP;
            sp.MotaSP = sanpham.MotaSP;

            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                int id = sanpham.MaSP;

                string _FileName = "";
                int index = uploadhinh.FileName.IndexOf('.');
                _FileName = "suasp" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/Content/img"), _FileName);
                uploadhinh.SaveAs(_path);
                sp.HinhanhSP = _FileName;
            }

            sp.NgaycapnhatSP = sanpham.NgaycapnhatSP;
            sp.SoluongSP = sanpham.SoluongSP;
            sp.GiaSP = sanpham.GiaSP;
            sp.MaTH = 1;
            sp.MaDM = 1;
            UpdateModel(sp);
            data.SubmitChanges();    
            return RedirectToAction("QLSanpham");
            
        }
        #endregion
    }
}
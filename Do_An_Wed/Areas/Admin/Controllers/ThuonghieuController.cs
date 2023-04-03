using Do_An_Wed.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Areas.Admin.Controllers
{
    public class ThuonghieuController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        ///quan ly thuong hiêu
        #region quản ly thương hiệu
        public ActionResult QLThuonghieu(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            return View(data.THUONGHIEUs.ToList().OrderBy(n => n.MaTH).ToPagedList(pageNumber, pageSize));
        }
        //Them thuong hieu
        [HttpGet]
        public ActionResult Themmoith()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themmoith(THUONGHIEU thuonghieu)
        {
            if (string.IsNullOrEmpty(thuonghieu.TenTH))
            {
                ViewBag.ThongBao = "Bạn cần nhập tên thương hiệu";
                return View(thuonghieu.TenTH);
            }
            var brand = data.THUONGHIEUs.FirstOrDefault(b => b.TenTH == thuonghieu.TenTH);
            if (brand != null)
            {
                ViewBag.ThongBao = "Tên thương hiệu đã tồn tại";
                return View(thuonghieu);
            }
            if (string.IsNullOrEmpty(thuonghieu.QuocgiaSX))
            {
                ViewBag.ThongBao1 = "Bạn cần nhập tên quốc gia sản xuất ";
                return View(thuonghieu.QuocgiaSX);
            }
            if (string.IsNullOrEmpty(thuonghieu.Mota))
            {
                ViewBag.ThongBao2 = "Bạn cần nhập mô tả thương hiệu";
                return View(thuonghieu.Mota);
            }

            // Lưu thương hiệu vào CSDL ở đây


            data.THUONGHIEUs.InsertOnSubmit(thuonghieu);
            data.SubmitChanges();
            return RedirectToAction("QLThuonghieu");
        }
        //hien thi thuong hiêu
        public ActionResult Chitietth(int id)
        {
            THUONGHIEU thuonghieu = data.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            ViewBag.MaTH = thuonghieu.MaTH;
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(thuonghieu);
        }
        //xoa thuong hieu
        [HttpGet]
        public ActionResult Xoath(int id)
        {


            THUONGHIEU dm = data.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idDM = dm.MaTH;
            return View(dm);

        }
        [HttpPost, ActionName("Xoath")]
        public ActionResult AcceptXoath(int id)
        {
            THUONGHIEU dm = data.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idDM = dm.MaTH;
            data.THUONGHIEUs.DeleteOnSubmit(dm);
            data.SubmitChanges();
            return RedirectToAction("QLThuonghieu");
        }
        //sua thuong hieu
        [HttpGet]
        public ActionResult Suath(int id)
        {
            THUONGHIEU thuonghieu = data.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(thuonghieu);
        }
        [HttpPost, ActionName("Suath")]
        [ValidateInput(false)]
        public ActionResult DropDown(int id)
        {

            THUONGHIEU thuonghieu = data.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            ViewBag.MaTS = thuonghieu.MaTH;
            UpdateModel(thuonghieu);
            data.SubmitChanges();
            return RedirectToAction("QLThuonghieu");
        }

        #endregion
    }
}
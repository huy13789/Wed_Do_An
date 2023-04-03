using Do_An_Wed.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Areas.Admin.Controllers
{
    public class DanhmucController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: Admin/Danhmuc
        //quản lý danh mục
        #region quản ly Danh mục
        public ActionResult QLDanhmuc(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            return View(data.DANHMUCs.ToList().OrderBy(n => n.MaDM).ToPagedList(pageNumber, pageSize));
        }
        //Them danh mục
        [HttpGet]
        public ActionResult Themmoidm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Themmoidm(DANHMUC danhmuc)
        {
            if (string.IsNullOrEmpty(danhmuc.TenDM))
            {
                ViewBag.ThongBao = "Bạn cần nhập tên danh mục";
                return View(danhmuc.TenDM);
            }
            var brand = data.DANHMUCs.FirstOrDefault(b => b.TenDM == danhmuc.TenDM);
            if (brand != null)
            {
                ViewBag.ThongBao = "Tên danh mục đã tồn tại";
                return View(danhmuc);
            }


            // Lưu thương hiệu vào CSDL ở đây


            data.DANHMUCs.InsertOnSubmit(danhmuc);
            data.SubmitChanges();
            return RedirectToAction("QLDanhmuc");
        }

        //xoa danh muc
        [HttpGet]
        public ActionResult Xoadm(int id)
        {
            DANHMUC dm = data.DANHMUCs.SingleOrDefault(n => n.MaDM == id);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idDM = dm.MaDM;
            return View(dm);

        }
        [HttpPost, ActionName("Xoadm")]
        public ActionResult AcceptXoaDM(int id)
        {
            DANHMUC dm = data.DANHMUCs.SingleOrDefault(n => n.MaDM == id);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idDM = dm.MaDM;
            data.DANHMUCs.DeleteOnSubmit(dm);
            data.SubmitChanges();
            return RedirectToAction("QLDanhmuc");
        }
        //sua danh muc
        [HttpGet]
        public ActionResult Suadm(int id)
        {
            DANHMUC danhmuc = data.DANHMUCs.SingleOrDefault(n => n.MaDM == id);
            if (danhmuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(danhmuc);
        }
        [HttpPost, ActionName("Suadm")]
        [ValidateInput(false)]
        public ActionResult save(int id)
        {

            DANHMUC danhmuc = data.DANHMUCs.SingleOrDefault(n => n.MaDM == id);
            if (danhmuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            ViewBag.MaTS = danhmuc.MaDM;
            UpdateModel(danhmuc);
            data.SubmitChanges();
            return RedirectToAction("QLDanhmuc");
        }
        #endregion
    }
}
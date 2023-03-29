using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Do_An_Wed.Models;
using PagedList;

namespace Do_An_Wed.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        MyDataDataContext data= new MyDataDataContext();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
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
            if (string.IsNullOrEmpty(thuonghieu.TenTH ))
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
            if (string.IsNullOrEmpty( thuonghieu.QuocgiaSX ))
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
        //quản lý danh mục
        #region quản ly Danh mục
        public ActionResult QLDanhmuc(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            return View(data.DANHMUCs.ToList().OrderBy(n => n.MaDM).ToPagedList(pageNumber, pageSize));
        }
        //Them thuong hieu
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

        #endregion
        /// quản lý sản phẩm

        //lỗi tụt lz
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
        public ActionResult Themmoisp(SANPHAM sanpham, HttpPostedFileBase fileupload)
        {
           
                //var fileName = Path.GetFileName(fileupload.FileName);
                //var path = Path.Combine(Server.MapPath("~/Content/img/Kem/"), fileName);
                string path = Path.Combine(Server.MapPath("/Content/img/"), Path.GetFileName(fileupload.FileName));
                fileupload.SaveAs(path);
                sanpham.HinhanhSP = "/Content/img/" + fileupload.FileName;
                ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
                ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
                //sanpham.HinhanhSP = fileName;
                //data.SANPHAMs.Add(sanpham);
                data.SANPHAMs.InsertOnSubmit(sanpham);
                data.SubmitChanges();
                return RedirectToAction("QLSanpham");
          
        }

        //hien thi san pham
        /*public ActionResult Chitietsp(int id)
        {
            Trasua trasua = db.Trasuas.SingleOrDefault(n => n.MaTS == id);
            ViewBag.MaTS = trasua.MaTS;
            if (trasua == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(trasua);
        }
        //xoa san pham
        [HttpGet]
        public ActionResult Xoasp(int id)
        {
            Trasua trasua = db.Trasuas.SingleOrDefault(n => n.MaTS == id);
            ViewBag.MaTS = trasua.MaTS;
            if (trasua == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(trasua);
        }
        [HttpPost, ActionName("Xoasp")]
        public ActionResult Xacnhanxoa(int id)
        {
            Trasua trasua = db.Trasuas.SingleOrDefault(n => n.MaTS == id);
            ViewBag.MaTS = trasua.MaTS;
            if (trasua == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Trasuas.DeleteOnSubmit(trasua);
            db.SubmitChanges();
            return RedirectToAction("ThemSanPham");
        }
        //chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult Suasp(int id)
        {
            ViewBag.MaTH = new SelectList(db.Thuonghieus.ToList().OrderBy(n => n.MaTH), "MaTH", "TenTH");
            Trasua trasua = db.Trasuas.SingleOrDefault(n => n.MaTS == id);
            if (trasua == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(trasua);
        }
        [HttpPost, ActionName("Suasp")]
        [ValidateInput(false)]
        public ActionResult DropDownList(int id)
        {

            Trasua product = db.Trasuas.SingleOrDefault(n => n.MaTS == id);
            if (product == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            ViewBag.MaTS = product.MaTS;
            UpdateModel(product);
            db.SubmitChanges();
            return RedirectToAction("ThemSanPham");
        }*/
        #endregion

        //Tài khoản 
        public ActionResult Edit(int id)
        {
            var kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            return View(kh);
        }
        public ActionResult Edit(int id, FormCollection collection)
        {
            
            return View();
        }

    }
}
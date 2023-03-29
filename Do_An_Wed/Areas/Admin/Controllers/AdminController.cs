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
        // GET: Admin/Admin
        public ActionResult Index()
        {
            List<SANPHAM> list = data.SANPHAMs.ToList();
            int v = list.Count();
            List<DANHMUC> dm = data.DANHMUCs.ToList();
            int d = dm.Count();
            List<THUONGHIEU> listth = data.THUONGHIEUs.ToList();
            int t = listth.Count();
            List<KHACHHANG> TK = data.KHACHHANGs.Where(n => n.MaQuyen == 0).ToList();
            List<KHACHHANG> TKad = data.KHACHHANGs.Where(n => n.MaQuyen == 1).ToList();
            ViewBag.sanpham = v;
            ViewBag.dm = d;
            ViewBag.thuonghieu = t;
            ViewBag.khachhang = TK.Count();
            ViewBag.Admin = TKad.Count();
            
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
        public ActionResult Themmoisp(SANPHAM sanpham, HttpPostedFileBase fileupload)
        {

            var fileName = Path.GetFileName(fileupload.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/img/Kem/"), fileName);
            if (System.IO.File.Exists(path))
            {
                ViewBag.Thongbao = "Hình ảnh đã tồn tại ";
            }
            else
            {
                //luu hinh anh vao duong dan
                fileupload.SaveAs(path);
            }
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            sanpham.HinhanhSP = fileName;
            data.SANPHAMs.InsertOnSubmit(sanpham);
            data.SubmitChanges();
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
        public ActionResult Suasp(SANPHAM sanpham, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.MaTH), "MaTH", "TenTH");
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.MaDM), "MaDM", "TenDM");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sanpham.HinhanhSP = fileName;
                    UpdateModel(sanpham);
                    data.SubmitChanges();
                }
                return RedirectToAction("SANPHAM");
            }

        }
        #endregion

        ///quản lý khách hàng
        #region QUAN LY TAI KHOAN KHACH HANG
        public ActionResult QLKhachhang(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            return View(data.KHACHHANGs.Where(n => n.MaQuyen == 0).ToList().ToPagedList(pageNumber, pageSize));
        }
        //Xóa khách hàng
        [HttpGet]
         public ActionResult Xoatkkh(int id)
          {
              KHACHHANG tk = data.KHACHHANGs.Where(n => n.MaQuyen == 0).SingleOrDefault(n => n.MaKH == id);
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
              KHACHHANG kh = data.KHACHHANGs.Where(n => n.MaQuyen == 0).SingleOrDefault(n => n.MaKH == id);
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
            ViewBag.maQuyen = new SelectList(data.PHANQUYENs.ToList().OrderBy(n => n.MaQuyen), "MaQuyen", "TenQuyen");
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
        ///quản lý admin

        #region QUẢN LÝ THÔNG TIN ADMIN
        public ActionResult QLAdmin(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            return View(data.KHACHHANGs.Where(n => n.MaQuyen == 1).ToList().ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult themad()
        {

            ViewBag.MaQuyen = new SelectList(data.PHANQUYENs.ToList().OrderBy(n => n.MaQuyen), "MaQuyen", "TenQuyen");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themad(KHACHHANG taikhoa)
        {
            if (string.IsNullOrEmpty(taikhoa.tentk))
            {
                ViewBag.ThongBao = "Bạn cần nhập tên tài khoản";
                return View(taikhoa.tentk);
            }
            var brand = data.KHACHHANGs.FirstOrDefault(b => b.tentk == taikhoa.tentk);
            if (brand != null)
            {
                ViewBag.ThongBao = "Tên tài khoản  đã tồn tại";
                return View(taikhoa);
            }
            ViewBag.MaQuyen = new SelectList(data.PHANQUYENs.ToList().OrderBy(n => n.MaQuyen), "MaQuyen", "TenQuyen");
            data.KHACHHANGs.InsertOnSubmit(taikhoa);
            data.SubmitChanges();
            return RedirectToAction("QLAdmin");
        }
        [HttpGet]
        public ActionResult Suaad(int id)
        {
            ViewBag.maQuyen = new SelectList(data.PHANQUYENs.ToList().OrderBy(n => n.MaQuyen), "MaQuyen", "TenQuyen");
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
            KHACHHANG tk = data.KHACHHANGs.Where(n => n.MaQuyen == 1).SingleOrDefault(n => n.MaKH == id);
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
            KHACHHANG kh = data.KHACHHANGs.Where(n => n.MaQuyen == 1).SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MAKH = kh.MaKH;
            return View(kh);
        }

        public ActionResult Chitietadadmin(string id)
        {
            KHACHHANG kh = data.KHACHHANGs.Where(n => n.MaQuyen == 1).SingleOrDefault(n => n.tentk == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MAKH = kh.MaKH;
            return View(kh);
        }
        #endregion



        ///quản lý đơn hàng
        #region quan ly Don hang
        #endregion


    }
}
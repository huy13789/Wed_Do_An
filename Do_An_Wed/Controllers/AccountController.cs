using Do_An_Wed.Models;
using Do_An_Wed.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Controllers
{
    public class AccountController : Controller
    {
        MyDataDataContext db = new MyDataDataContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
            //mã hóa ở đây lúc đăng kí, nên đăng nhặp cg phải mã hóa ngược, ok chưa
           
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection, loginVM lg)
        {
            var tendn = collection["tentk"];
            var matkhau = collection["mk"];
            System.Diagnostics.Debug.WriteLine(tendn);
            System.Diagnostics.Debug.WriteLine(matkhau);
            if (String.IsNullOrEmpty(tendn) && String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi"] = "Vui lòng nhập tài khoản và mật khẩu!";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Vui lòng nhập tài khoản!";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu!";
            }
            else
            {
                KHACHHANG tk = db.KHACHHANGs.SingleOrDefault(n => n.tentk == tendn && n.mk == MD5Hash(matkhau));
                KHACHHANG tkCheck = db.KHACHHANGs.SingleOrDefault(n => n.tentk == tendn && n.MaRole == 1);
                if (tk == null)
                {
                    ViewBag.checkTK = "Tài khoản chưa tồn tại!";
                }
                else if (tkCheck != null)
                {
                    Session["Taikhoan"] = tendn;
                    Session["Tendangnhap"] = tkCheck.HoTen.ToString();
                    Session["Makh"] = tkCheck.MaKH;
                    Session.Timeout = 500000;
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }
                else if (tk != null)
                {
                    Session["Taikhoan"] = tk.HoTen;
                    Session["TaikhoanCart"] = tk;
                    Session.Timeout = 500000;
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ViewData["ThongBao"] = "Mật khẩu không chính xác!";
                }
            }
            return View();
        }

        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HoTen"];
            var tendn = collection["tentk"];
            var matkhau = collection["MatKhau"];
            var matkhau2 = collection["MatKhau2"];
            var diachi = collection["DiachiKH"];
            var email = collection["EmailKH"];
            var sdt = collection["DienthoaiKh"];

            KHACHHANG check = db.KHACHHANGs.SingleOrDefault(n => n.tentk == tendn);

            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống!";
            }
            else if (String.IsNullOrEmpty(tendn) || tendn.Count() < 6)
            {
                ViewData["LoiCountTK"] = "Vui lòng nhập tài khoản dài hơn 6 ký tự!";
            }
            else if (check != null)
            {
                ViewData["LoiTrungTK"] = "Tài khoản đã được sử dụng!";
            }
            else if (String.IsNullOrEmpty(matkhau) || matkhau.Count() < 6)
            {
                ViewData["LoiCountMK"] = "Vui lòng nhập mật khẩu dài hơn 6 ký tự!";
            }
            else if (String.IsNullOrEmpty(matkhau2))
            {
                ViewData["Loi4"] = "Vui lòng nhập lại mật khẩu!";
            }
            else if (matkhau != matkhau2)
            {
                ViewData["LoiTrungMK"] = "Mật khẩu không khớp!";
            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi5"] = "Địa chỉ không được để trống!";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi6"] = "Vui lòng nhập email!";
            }
            else if (String.IsNullOrEmpty(sdt))
            {
                ViewData["Loi7"] = "Vui lòng nhập số điện thoại!";
            }
            else
            {
                kh.HoTen = hoten;
                kh.DiachiKH = diachi;
                kh.EmailKH = email;
                kh.DiachiKH = sdt;
                kh.tentk = tendn;
                kh.mk = MD5Hash(matkhau);
                kh.MaRole = 0;

                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Login");
            }

            return DangKy();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
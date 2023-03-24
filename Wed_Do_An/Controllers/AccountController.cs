using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Wed_Do_An.Models;
using Wed_Do_An.ViewModel;

namespace Wed_Do_An.Controllers
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
                KHACHHANG tk = db.KHACHHANGs.SingleOrDefault(n => n.tentk == tendn && n.mk == matkhau);
                KHACHHANG tkCheck = db.KHACHHANGs.SingleOrDefault(n => n.tentk == tendn && n.MaRole == 1);
                if (tkCheck == null)
                {
                    ViewBag.checkTK = "Tài khoản chưa tồn tại!";
                }
                else if (tk != null)
                {
                    Session["Taikhoan"] = tk;
                    Session["TKUser"] = tendn;
                    Session.Timeout = 500000;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ThongBao"] = "Mật khẩu không chính xác!";
                }
            }
            return View();
        }
    }
}
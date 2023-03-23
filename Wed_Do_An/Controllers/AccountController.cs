using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Wed_Do_An.Models;

namespace Wed_Do_An.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        MyDataDataContext db = new MyDataDataContext();
        public ActionResult Index()
        {
            return RedirectToAction("Login");
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
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {

            var tendn = collection["tk"];
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
                KHACHHANG tk = db.KHACHHANGs.SingleOrDefault(n => n.tentk == tendn && n.mk == MD5Hash(matkhau) && n.MaRole == 1);
                KHACHHANG tkCheck = db.KHACHHANGs.SingleOrDefault(n => n.tentk == tendn && n.tentk == "User");
                if (tkCheck == null)
                {
                    ViewBag.checkTK = "Tài khoản chưa tồn tại!";
                }
                else if (tk != null)
                {
                    Session["Taikhoan"] = tk;
                    Session["TKUser"] = tendn;
                    Session.Timeout = 500000;
                    return RedirectToAction("Index", "Products");
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
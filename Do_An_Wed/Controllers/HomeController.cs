using Do_An_Wed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        private List<SANPHAM> laysachmoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NgaycapnhatSP).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var spmoi = laysachmoi(6);
            return View(spmoi);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
       public ActionResult Gioithieu()
        {
            return View();
        }
     
    }
}
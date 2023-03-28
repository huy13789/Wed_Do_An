using Do_An_Wed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Do_An_Wed.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        MyDataDataContext db = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_sp = from tt in db.SANPHAMs select tt;
            return View(all_sp);
        }
    }
}
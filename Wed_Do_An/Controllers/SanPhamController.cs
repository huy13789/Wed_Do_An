using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wed_Do_An.Models;

namespace Wed_Do_An.Controllers
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
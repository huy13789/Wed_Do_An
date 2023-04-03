using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Do_An_Wed.Models;
using PagedList;

namespace Do_An_Wed.Controllers
{
    public class TimkiemController : Controller
    {

        MyDataDataContext db = new MyDataDataContext();
        [HttpGet]
        public ActionResult KQTimkiem(string sTukhoa, int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            //tim kiem theo ten san pham
            var listSP = db.SANPHAMs.Where(n => n.TenSP.Contains(sTukhoa));
            ViewBag.Tukhoa = sTukhoa;
            return View(listSP.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(string sTukhoa)
        {
            //goij ve ham get tim kiem

            return RedirectToAction("KQTimkiem", new { @sTukhoa = sTukhoa });
        }
    }
}
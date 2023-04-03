using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Do_An_Wed.Models;
using PagedList;

namespace Do_An_Wed.Controllers
{
    public class SanphamsController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult sanpham(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            return View(data.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Chitiet(int id)
        {
            var chitiet = from s in data.SANPHAMs
                         where s.MaSP == id
                         select s;
            return View(chitiet.Single());
        }
        // 
        /// 
        /// SẢN PHẨM SẮP XẾP THEO DANH MỤC
      
        public ActionResult Danhmuc()
        {
            var danhmuc = from dm in data.DANHMUCs select dm;
            return PartialView(danhmuc);
        }
        public ActionResult SPTheodanhmuc(int id, int? page)
        {
            // kiem tra thuong hieu co ton tai khong
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            DANHMUC danhmuc = data.DANHMUCs.SingleOrDefault(n => n.MaDM == id);
            if (danhmuc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuat danh sach theo thuong hieu
            List<SANPHAM> listsanpham = data.SANPHAMs.Where(n => n.MaDM == id).OrderBy(n => n.GiaSP).ToList();
            if (listsanpham.Count == 0)
            {
                ViewBag.sanpham = "Thương hiệu này chưa mở bán!!";
            }
            else
            {
                int i;
                for (i = 0; i < listsanpham.Count; i++)
                {
                    i = i++;
                }
                ViewBag.soluong = i;
            }
            return View(listsanpham.ToPagedList(pageNumber, pageSize));
        }
        ///
        ///SẢN PHẨM THEO THƯƠNG HIỆU
        public ActionResult Thuonghieu()
        {
            var thuonghieu = from th in data.THUONGHIEUs select th;
            return PartialView(thuonghieu);
        }
        public ActionResult SPTheothuonghieu(int id, int? page)
        {
            // kiem tra thuong hieu co ton tai khong
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            THUONGHIEU thuonghieu = data.THUONGHIEUs.SingleOrDefault(n => n.MaTH == id);
            if (thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuat danh sach theo thuong hieu
            List<SANPHAM> listsanpham = data.SANPHAMs.Where(n => n.MaTH == id).OrderBy(n => n.GiaSP).ToList();
            if (listsanpham.Count == 0)
            {
                ViewBag.sanpham = "Thương hiệu này chưa mở bán!!";
            }
            else
            {
                int i;
                for (i = 0; i < listsanpham.Count; i++)
                {
                    i = i++;
                }
                ViewBag.soluong = i;
            }
            return View(listsanpham.ToPagedList(pageNumber, pageSize));
        }
    }
}
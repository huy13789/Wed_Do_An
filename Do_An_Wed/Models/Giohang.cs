using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_An_Wed.Models
{
    public class Giohang
    {
        MyDataDataContext data = new MyDataDataContext();

        public int Masp { get; set; }

        public string tensanpham { get; set; }

        public string hinh { get; set; }

        public int giaban { get; set; }

        public int soluong { get; set; }

        public double dThanhTIen
        {
            get { return soluong * giaban; }
        }

        public Giohang(int id)
        {
            Masp = id;
            SANPHAM sp = data.SANPHAMs.Single(n => n.MaSP == Masp);
            tensanpham = sp.TenSP;
            hinh = sp.HinhanhSP;
            giaban = (int)double.Parse(sp.GiaSP.ToString());
            soluong = 1;
        }
    }
}
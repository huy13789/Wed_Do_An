﻿using Do_An_Wed.Models;
using Do_An_Wed.Momo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Do_An_Wed.Controllers
{
    public class GioHangController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: Giohang
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang == null)
            {
                lstgiohang = new List<Giohang>();
                Session["Giohang"] = lstgiohang;
            }

            return lstgiohang;
        }
        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Giohang> lstgiohang = Laygiohang();
            Giohang sanpham = lstgiohang.Find(n => n.Masp == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstgiohang.Add(sanpham);
                Session["TongSoSp"] = TongSoluongSanPham() + "";
                return Redirect(strURL);
            }
            else
            {
                Session["TongSoSp"] = TongSoluongSanPham() + "";
                sanpham.soluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoluong()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.soluong);
            }
            return tsl;
        }

        private int TongSoluongSanPham()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            Session["TongSoSp"] = tsl + "";
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhTIen);
            }
            return tt;
        }
        public ActionResult Giohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoluong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            Session["TongSoSp"] = TongSoluongSanPham() + "";
            return View(lstGiohang);
        }

        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoluong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            Session["TongSoSp"] = TongSoluongSanPham() + "";
            return PartialView();
        }

        public ActionResult XoaGioHang(int id)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.Masp == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.Masp == id);
                return RedirectToAction("Giohang");
            }
            return RedirectToAction("Giohang");
        }

        public ActionResult Cannhapgiohang(int id, FormCollection collection)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.Masp == id);
            if (sanpham != null)
            {
                sanpham.soluong = int.Parse(collection["txtSolg"].ToString());
            }
            return RedirectToAction("Giohang");
        }

        public ActionResult Xoatatcagiohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Giohang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("sanpham", "sanphams");
            }
            List<Giohang> lstGioHang = Laygiohang();
            ViewBag.Tongsoluong = TongSoluong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoluongSanPham();
            return View(lstGioHang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            DONHANG dh = new DONHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaikhoanCart"];

            List<Giohang> gh = Laygiohang();

            var ngaygiao = String.Format("{0:dd/MM/yyyy}", collection["NgayGiao"]);
            dh.MaKH = kh.MaKH;
            dh.NgayDH = DateTime.Now;
            dh.Ngaygiao = DateTime.Parse(ngaygiao);
            dh.Trangthai = false;

            data.DONHANGs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                CHITIETDONHANG ctdh = new CHITIETDONHANG();
                ctdh.MaDH = dh.MaDH;
                ctdh.MaSP = item.Masp;
                ctdh.Soluong = item.soluong;
                ctdh.Dongia = (decimal)item.giaban;
                //s = data.SANPHAMs.Single(n => n.MaSP == item.Masp);
                //s.soluongton -= ctdh.soluong;               
                data.CHITIETDONHANGs.InsertOnSubmit(ctdh);
                data.SubmitChanges();
            }
            
            data.SubmitChanges();
            Session["GioHang"] = null;

            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }

        //public ActionResult Payment()
        //{
        //    //request params need to request to MoMo system
        //    List<Giohang> giohangs = Session["GioHang"] as List<Giohang>;
        //    string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
        //    string partnerCode = ConfigurationManager.AppSettings["partnerCode"].ToString();
        //    string accessKey = ConfigurationManager.AppSettings["accessKey"].ToString();
        //    string serectKey = ConfigurationManager.AppSettings["serectKey"].ToString();
        //    string orderInfo = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");
        //    string returnUrl = ConfigurationManager.AppSettings["returnUrl"].ToString();
        //    string notifyurl = ConfigurationManager.AppSettings["notifyurl"].ToString();

        //    string amount = giohangs.Sum(n => n.dThanhTIen).ToString();
        //    string orderid = Guid.NewGuid().ToString();
        //    string requestId = Guid.NewGuid().ToString();
        //    string extraData = "";

        //    //Before sign HMAC SHA256 signature
        //    string rawHash = "partnerCode=" +
        //        partnerCode + "&accessKey=" +
        //        accessKey + "&requestId=" +
        //        requestId + "&amount=" +
        //        amount + "&orderId=" +
        //        orderid + "&orderInfo=" +
        //        orderInfo + "&returnUrl=" +
        //        returnUrl + "&notifyUrl=" +
        //        notifyurl + "&extraData=" +
        //        extraData;

        //    MoMoSecurity crypto = new MoMoSecurity();
        //    //sign signature SHA256
        //    string signature = crypto.signSHA256(rawHash, serectKey);

        //    //build body json request
        //    JObject message = new JObject
        //    {
        //        { "partnerCode", partnerCode },
        //        { "accessKey", accessKey },
        //        { "requestId", requestId },
        //        { "amount", amount },
        //        { "orderId", orderid },
        //        { "orderInfo", orderInfo },
        //        { "returnUrl", returnUrl },
        //        { "notifyUrl", notifyurl },
        //        { "extraData", extraData },
        //        { "requestType", "captureMoMoWallet" },
        //        { "signature", signature }

        //    };

        //    string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

        //    JObject jmessage = JObject.Parse(responseFromMomo);

        //    return Redirect(jmessage.GetValue("payUrl").ToString());
        //}

        ////Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        ////errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        ////Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        //public ActionResult ConfirmPaymentClient(Result result)
        //{
        //    //lấy kết quả Momo trả về và hiển thị thông báo cho người dùng (có thể lấy dữ liệu ở đây cập nhật xuống db)
        //    string rMessage = result.message;
        //    string rOrderId = result.orderId;
        //    string rErrorCode = result.errorCode; // = 0: thanh toán thành công
        //    return View();
        //}

        //[HttpPost]
        //public void SavePayment()
        //{
        //    //cập nhật dữ liệu vào db
        //    String a = "";
        //}
    }
}

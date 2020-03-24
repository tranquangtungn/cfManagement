using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controller
{
    class QLGoiMon
    {
        public void ThemHoaDon(int maNV, DateTime ngayLap, int giamGia)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            HoaDon hd = new HoaDon();

            hd.MaNV = maNV;
            hd.NgayLap = ngayLap;
            hd.GiamGia = giamGia;

            qlcf.HoaDons.Add(hd);
            qlcf.SaveChanges();
        }
        public int  LayMaHoaDon()
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            
            var lmQuery = (from lm in qlcf.HoaDons                            
                           select lm).Max(c=>c.MaHD);
            return lmQuery;
            
        }
        public int LayMaMon(string tenMon)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            var lmQuery = (from lm in qlcf.MonAns
                           where lm.TenMonAn == tenMon
                           select lm).SingleOrDefault();
            return lmQuery.MaMonAn;
        }
        public void ThemChiTietHD( int maMon, int maHD, int SoLuong, int ThanhTien)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            ChiTietHoaDon hd = new ChiTietHoaDon();

            hd.MaMonAn = maMon;
            hd.MaHD = maHD;
            hd.SoLuong = SoLuong;
            hd.ThanhTien = ThanhTien;

            qlcf.ChiTietHoaDons.Add(hd);
            qlcf.SaveChanges();
        }
        public int? LayGiamGia(int maHoaDon)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            var lmQuery = (from lm in qlcf.HoaDons
                           where lm.MaHD == maHoaDon
                           select lm).SingleOrDefault();
            return lmQuery.GiamGia;
        }
        public DateTime LayNgayLap(int maHoaDon)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            var lmQuery = (from lm in qlcf.HoaDons
                           where lm.MaHD == maHoaDon
                           select lm).SingleOrDefault();
            return lmQuery.NgayLap;
        }
        public string LayTenMonAn(int maMon)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            var lmQuery = (from lm in qlcf.MonAns
                           where lm.MaMonAn == maMon
                           select lm).SingleOrDefault();
            return lmQuery.TenMonAn;
        }
        public DataTable XemChiTietHD(int ma)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            DataTable dt = new DataTable();
            
            dt.Columns.Add("Tên món ăn");
            dt.Columns.Add("Số lượng");
            dt.Columns.Add("Thành tiền");
            
            var cQuery = from c in qlcf.ChiTietHoaDons where c.MaHD == ma select c;
            foreach (var temp in cQuery)
            {
                dt.Rows.Add(LayTenMonAn(temp.MaMonAn), temp.SoLuong, temp.ThanhTien);
            }
            return dt;
        }
    }
}

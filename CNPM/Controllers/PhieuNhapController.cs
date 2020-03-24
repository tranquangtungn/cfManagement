using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controllers
{
    class PhieuNhapController
    {

        public string GetTotal(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            string total = qlcp.PhieuNhaps.Where(x => x.MaPhieuNhap == id).Select(x => x.TongTien).FirstOrDefault().ToString();
            return total;
        }
        public string GetNote(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            string note = qlcp.PhieuNhaps.Where(x => x.MaPhieuNhap == id).Select(x => x.GhiChu).FirstOrDefault();
            return note;
        }
        public int GetDistributorId(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            int nccid = qlcp.PhieuNhaps.Where(x => x.MaPhieuNhap == id).Select(x => x.MaNhaCC).FirstOrDefault();
            return nccid;
        }
        public int GetLastId()
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            int id = qlcp.PhieuNhaps.Select(x => x.MaPhieuNhap).Max();
            return id;
        }
        public DataTable GetList()
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = (from p in qlcp.PhieuNhaps
                       join n in qlcp.NhanViens on p.MaNV equals n.MaNV
                       select new { p.MaPhieuNhap, p.NgayNhap, n.TenNV, p.TongTien });

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã phiếu");
            dt.Columns.Add("Ngày nhập");
            dt.Columns.Add("Nhân viên");
            dt.Columns.Add("Tổng tiền");

            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaPhieuNhap, item.NgayNhap.ToShortDateString(), item.TenNV, item.TongTien);
            }

            return dt;
        }
        public DataTable SearchList(string search)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = (from p in qlcp.PhieuNhaps
                        join n in qlcp.NhanViens on p.MaNV equals n.MaNV
                        where (n.TenNV.ToLower().Contains(search.ToLower()) || p.NgayNhap.ToString().Contains(search)) 
                        select new { p.MaPhieuNhap, p.NgayNhap, n.TenNV, p.TongTien });

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã phiếu");
            dt.Columns.Add("Ngày nhập");
            dt.Columns.Add("Nhân viên");
            dt.Columns.Add("Tổng tiền");

            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaPhieuNhap, item.NgayNhap.ToShortDateString(), item.TenNV, item.TongTien);
            }

            return dt;
        }
        public DataTable GetDetails(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();

            var temp = (from ct in qlcp.ChiTietPhieuNhaps
                        join nl in qlcp.NguyenLieux on ct.MaNL equals nl.MaNL
                        where ct.MaPhieuNhap == id
                        select new { nl.TenNL, ct.SoLuongNhap, ct.HanSuDung, ct.Gia, ct.GhiChu});

            DataTable dt = new DataTable();
            dt.Columns.Add("Tên nguyên liệu");
            dt.Columns.Add("Số lượng");
            dt.Columns.Add("Hạn sử dụng");
            dt.Columns.Add("Giá");
            dt.Columns.Add("Ghi chú");
            dt.Columns.Add("Tổng tiền");

            foreach (var item in temp)
            {
                dt.Rows.Add(item.TenNL, item.SoLuongNhap, item.HanSuDung.ToShortDateString(), item.Gia, item.GhiChu, item.SoLuongNhap * item.Gia);
            }

            return dt;
        }
        public void Create(int manv, int mancc, DateTime ngayNhap, string ghiChu, int tongTien)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            PhieuNhap phieuNhap = new PhieuNhap();

            phieuNhap.MaNV = manv;
            phieuNhap.MaNhaCC = mancc;
            phieuNhap.NgayNhap = ngayNhap;
            phieuNhap.GhiChu = ghiChu;
            phieuNhap.TongTien = tongTien;

            qlcp.PhieuNhaps.Add(phieuNhap);
            qlcp.SaveChanges();
        }
        public void CreateDetails(int maPN, int maNL, DateTime hsd, int soLuong, int gia, string ghiChu)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            ChiTietPhieuNhap chiTiet = new ChiTietPhieuNhap();

            chiTiet.MaPhieuNhap = maPN;
            chiTiet.MaNL = maNL;
            chiTiet.HanSuDung = hsd;
            chiTiet.SoLuongNhap = soLuong;
            chiTiet.Gia = gia;
            chiTiet.GhiChu = ghiChu;

            qlcp.ChiTietPhieuNhaps.Add(chiTiet);

            var temp = qlcp.NguyenLieux.Where(x => x.MaNL == maNL).Select(x => x).FirstOrDefault();
            temp.SoLuongTon += soLuong;

            qlcp.SaveChanges();
        }
    }
}

using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controllers
{
    class PhieuXuatController
    {
        public string GetNote(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            string note = qlcp.PhieuXuats.Where(x => x.MaPhieuXuat == id).Select(x => x.GhiChu).FirstOrDefault();
            return note;
        }
        public int GetLastId()
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            int id = qlcp.PhieuXuats.Select(x => x.MaPhieuXuat).Max();
            return id;
        }
        public DataTable GetList()
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = (from p in qlcp.PhieuXuats
                        join n in qlcp.NhanViens on p.MaNV equals n.MaNV
                        select new { p.MaPhieuXuat, p.NgayXuat, n.TenNV});

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã phiếu");
            dt.Columns.Add("Ngày xuất");
            dt.Columns.Add("Nhân viên");

            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaPhieuXuat, item.NgayXuat.ToShortDateString(), item.TenNV);
            }

            return dt;
        }
        public DataTable SearchList(string search)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = (from p in qlcp.PhieuXuats
                        join n in qlcp.NhanViens on p.MaNV equals n.MaNV
                        where (n.TenNV.ToLower().Contains(search.ToLower()) || p.NgayXuat.ToString().Contains(search))
                        select new { p.MaPhieuXuat, p.NgayXuat, n.TenNV});

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã phiếu");
            dt.Columns.Add("Ngày xuất");
            dt.Columns.Add("Nhân viên");

            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaPhieuXuat, item.NgayXuat.ToShortDateString(), item.TenNV);
            }

            return dt;
        }
        public DataTable GetDetails(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();

            var temp = (from ct in qlcp.ChiTietPhieuXuats
                        join nl in qlcp.NguyenLieux on ct.MaNL equals nl.MaNL
                        where ct.MaPhieuXuat == id
                        select new { nl.TenNL, ct.SoLuongXuat, ct.GhiChu });

            DataTable dt = new DataTable();
            dt.Columns.Add("Tên nguyên liệu");
            dt.Columns.Add("Số lượng");
            dt.Columns.Add("Ghi chú");

            foreach (var item in temp)
            {
                dt.Rows.Add(item.TenNL, item.SoLuongXuat, item.GhiChu);
            }

            return dt;
        }
        public void Create(int manv, DateTime ngayXuat, string ghiChu)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            PhieuXuat phieuXuat = new PhieuXuat();

            phieuXuat.MaNV = manv;
            phieuXuat.NgayXuat = ngayXuat;
            phieuXuat.GhiChu = ghiChu;

            qlcp.PhieuXuats.Add(phieuXuat);
            qlcp.SaveChanges();
        }
        public void CreateDetails(int maPX, int maNL, int soLuong, string ghiChu)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            ChiTietPhieuXuat chiTiet = new ChiTietPhieuXuat();

            chiTiet.MaPhieuXuat = maPX;
            chiTiet.MaNL = maNL;
            chiTiet.SoLuongXuat = soLuong;
            chiTiet.GhiChu = ghiChu;

            qlcp.ChiTietPhieuXuats.Add(chiTiet);

            var temp = qlcp.NguyenLieux.Where(x => x.MaNL == maNL).Select(x => x).FirstOrDefault();
            temp.SoLuongTon -= soLuong;

            qlcp.SaveChanges();
        }
    }
}

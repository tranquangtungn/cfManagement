using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controller
{
    class QLTinhLuong
    {

        public DataTable XemChamCongToanBoNV(DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {

            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("Nhân viên");
            dt.Columns.Add("Ngày làm");
            dt.Columns.Add("Số ca làm");
            dt.Columns.Add("Ghi chú");

            var cQuery = from c in qlcf.ChamCongs where ngayBatDau <= c.NgayLam && ngayKetThuc >= c.NgayLam select c;
            foreach (var temp in cQuery)
            {
                dt.Rows.Add(LayTenNV(temp.MaNV), temp.NgayLam, temp.SoCaLam, temp.GhiChu);
            }
            return dt;
        }
        public DataTable XemChamCong(int maNV, DateTime? ngayBatDau, DateTime? ngayKetThuc)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("Nhân viên");
            dt.Columns.Add("Ngày làm");
            dt.Columns.Add("Số ca làm");
            dt.Columns.Add("Ghi chú");

            var cQuery = from c in qlcf.ChamCongs where c.MaNV == maNV && ngayBatDau <= c.NgayLam && ngayKetThuc >= c.NgayLam select c;
            foreach (var temp in cQuery)
            {
                dt.Rows.Add(LayTenNV(temp.MaNV), temp.NgayLam, temp.SoCaLam, temp.GhiChu);
            }
            return dt;
        }
        public string LayTenNV(int maNV)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            var lmQuery = (from lm in qlcf.NhanViens
                           where lm.MaNV == maNV
                           select lm).SingleOrDefault();
            return lmQuery.TenNV;
        }
        public DataTable TimKiemNVTheoMa(string ma)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã NV");
            dt.Columns.Add("Họ tên");
            dt.Columns.Add("Ngày sinh");
           
            if (ma != "")
            {
                int? maNV = Convert.ToInt32(ma);
                var list = (from lm in qlcf.NhanViens
                            where lm.MaNV == maNV
                            select lm).ToList();
                foreach (var temp in list)

                {
                    dt.Rows.Add(temp.MaNV, temp.TenNV, temp.NgaySinh);
                }
            }

            return dt;
        }
        public DataTable TimKiemNVTheoTen(string ma)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã NV");
            dt.Columns.Add("Họ tên");
            dt.Columns.Add("Ngày sinh");

            if (ma != "")
            {
                
                var list = (from lm in qlcf.NhanViens
                            where lm.TenNV == ma
                            select lm).ToList();
                foreach (var temp in list)

                {
                    dt.Rows.Add(temp.MaNV, temp.TenNV, temp.NgaySinh);
                }
            }

            return dt;
        }
        public DataTable TimKiemNVTheoCMND(string ma)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã NV");
            dt.Columns.Add("Họ tên");
            dt.Columns.Add("Ngày sinh");

            if (ma != "")
            {
                
                var list = (from lm in qlcf.NhanViens
                            where lm.SoCMND == ma
                            select lm).ToList();
                foreach (var temp in list)

                {
                    dt.Rows.Add(temp.MaNV, temp.TenNV, temp.NgaySinh);
                }
            }

            return dt;
        }
    }
}

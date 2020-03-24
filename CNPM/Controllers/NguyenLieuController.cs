using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNPM.Models;

namespace CNPM.Controllers
{
    class NguyenLieuController
    {
        public int NameToId(string name)
        {

            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var id = qlcp.NguyenLieux.Where(x => x.TenNL == name).Select(x => x.MaNL).FirstOrDefault();

            return id;
        }
        public List<string> GetName()
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var dt = qlcp.NguyenLieux.Where(x => x.Xoa == false).Select(x => x.TenNL).ToList();

            return dt;
        }
        public DataTable Search(string searchText)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            

            var temp = (from nguyenLieu in qlcp.NguyenLieux
                         join loaiNguyenLieu in qlcp.LoaiNguyenLieux on nguyenLieu.MaLoaiNL equals loaiNguyenLieu.MaLoaiNL
                         where nguyenLieu.Xoa == false && (nguyenLieu.TenNL.ToLower().Contains(searchText.ToLower()) || loaiNguyenLieu.TenLoaiNL.ToLower().Contains(searchText.ToLower()))
                         select new { nguyenLieu.MaNL, nguyenLieu.TenNL, loaiNguyenLieu.TenLoaiNL, nguyenLieu.SoLuongTon });

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nguyên liệu");
            dt.Columns.Add("Tên nguyên liệu");
            dt.Columns.Add("Loại nguyên liệu");
            dt.Columns.Add("Số lượng tồn");
            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaNL, item.TenNL, item.TenLoaiNL, item.SoLuongTon);
            }

            return dt;
        }
        public DataTable GetData()
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            LoaiNguyenLieuController loaiNguyenLieu = new LoaiNguyenLieuController();
            var temp = (from nguyenLieu in qlcp.NguyenLieux
                        where nguyenLieu.Xoa == false
                        select nguyenLieu);
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nguyên liệu");
            dt.Columns.Add("Tên nguyên liệu");
            dt.Columns.Add("Loại nguyên liệu");
            dt.Columns.Add("Số lượng tồn");
            foreach (var item in temp)
            {
                string tenLoai = loaiNguyenLieu.IdToName(item.MaLoaiNL);
                dt.Rows.Add(item.MaNL, item.TenNL, tenLoai, item.SoLuongTon);
            }

            return dt;
        }
        public void Create(string tenNguyenLieu, int maLoaiNL, int soLuong)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            NguyenLieu nguyenLieu = new NguyenLieu();
            nguyenLieu.TenNL = tenNguyenLieu;
            nguyenLieu.MaLoaiNL = maLoaiNL;
            nguyenLieu.SoLuongTon = soLuong;
            nguyenLieu.Xoa = false;
            qlcp.NguyenLieux.Add(nguyenLieu);
            qlcp.SaveChanges();
        }
        public void Edit(int id, string tenNL, int maLoai, int soLuong)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = qlcp.NguyenLieux.Where(x => x.MaNL == id).FirstOrDefault();
            temp.TenNL = tenNL;
            temp.MaLoaiNL = maLoai;
            temp.SoLuongTon = soLuong;
            qlcp.SaveChanges();
        }
        public void Delete(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = qlcp.NguyenLieux.Where(x => x.MaNL == id).FirstOrDefault();
            temp.Xoa = true;
            qlcp.SaveChanges();
        }
    }
}

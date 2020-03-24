using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controllers
{
    class NhaCungCapController
    {
        public List<string> GetName()
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = qlcp.NhaCungCaps.Where(x => x.Xoa == false).Select(x => x.TenNhaCC).ToList();
            return temp;
        }
        public int NameToId(string name)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            int id = qlcp.NhaCungCaps.Where(x => x.TenNhaCC == name).Select(x => x.MaNhaCC).FirstOrDefault();
            return id;
        }
        public string IdToName(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            string name = qlcp.NhaCungCaps.Where(x => x.MaNhaCC == id).Select(x => x.TenNhaCC).FirstOrDefault();
            return name;
        }

        public DataTable Search(string searchText)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();

            var temp = (from ncc in qlcp.NhaCungCaps
                        where (ncc.Xoa == false && (ncc.TenNhaCC.ToLower().Contains(searchText.ToLower()) || ncc.DiaChi.ToLower().Contains(searchText.ToLower()) || ncc.SoDienThoai.ToLower().Contains(searchText.ToLower())))
                        select ncc);

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nhà cung cấp");
            dt.Columns.Add("Tên nhà cung cấp");
            dt.Columns.Add("Địa chỉ");
            dt.Columns.Add("Số điện thoại");
            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaNhaCC, item.TenNhaCC, item.DiaChi, item.SoDienThoai);
            }

            return dt;
        }

        public DataTable GetData()
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();

            var temp = (from ncc in qlcp.NhaCungCaps
                        where ncc.Xoa == false
                        select ncc);

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nhà cung cấp");
            dt.Columns.Add("Tên nhà cung cấp");
            dt.Columns.Add("Địa chỉ");
            dt.Columns.Add("Số điện thoại");
            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaNhaCC, item.TenNhaCC, item.DiaChi, item.SoDienThoai);
            }

            return dt;
        }
        public void Create(string tenNCC, string diaChi, string sdt)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            NhaCungCap ncc = new NhaCungCap();
            ncc.TenNhaCC = tenNCC;
            ncc.DiaChi = diaChi;
            ncc.SoDienThoai = sdt;
            ncc.Xoa = false;
            qlcp.NhaCungCaps.Add(ncc);
            qlcp.SaveChanges();
        }

        public void Edit(int id, string tenNCC, string diaChi, string sdt)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = qlcp.NhaCungCaps.Where(x => x.MaNhaCC == id).FirstOrDefault();
            temp.TenNhaCC = tenNCC;
            temp.DiaChi = diaChi;
            temp.SoDienThoai = sdt;
            qlcp.SaveChanges();
        }

        public void Delete(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = qlcp.NhaCungCaps.Where(x => x.MaNhaCC == id).FirstOrDefault();
            temp.Xoa = true;
            qlcp.SaveChanges();
        }
    }
}

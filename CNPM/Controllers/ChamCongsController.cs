using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CNPM.Controllers
{
    class ChamCongsController
    {
        public DataTable Details()
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = quanLyQuanCaPheEntities.ChamCongs.Select(x => x);
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã chấm công");
            dt.Columns.Add("Tên nhân viên");
            dt.Columns.Add("Ngày làm");
            dt.Columns.Add("Số ca làm trong ngày");
            dt.Columns.Add("Ghi chú");
            foreach (var item in _temp)
            {
                dt.Rows.Add(item.MaChamCong, item.NhanVien.TenNV, item.NgayLam.Value.ToShortDateString(), item.SoCaLam, item.GhiChu);
            }
            return dt;
        }
        public void Create(string name, DateTime ngaylam, int soca, string ghichu)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = quanLyQuanCaPheEntities.ChamCongs.Where(x => x.NhanVien.TenNV == name).Where(x => x.NgayLam == ngaylam).Select(x => x).FirstOrDefault();
            if (_temp == null)
            {
                ChamCong chamCong = new ChamCong();
                chamCong.MaNV = FindID(name);
                chamCong.NgayLam = ngaylam;
                chamCong.SoCaLam = soca;
                chamCong.GhiChu = ghichu;
                quanLyQuanCaPheEntities.ChamCongs.Add(chamCong);
                quanLyQuanCaPheEntities.SaveChanges();
                MessageBox.Show("Thêm mẫu chấm công thành công!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Mẫu chấm công đã tồn tại. Vui lòng xóa đi trước khi tạo mới!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Delete(string Name, DateTime date)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = new ChamCong { MaChamCong = Find(Name, date) };
            quanLyQuanCaPheEntities.Entry(_temp).State = System.Data.Entity.EntityState.Deleted;
            quanLyQuanCaPheEntities.SaveChanges();
        }

        private int FindID(string name)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            return quanLyQuanCaPheEntities.NhanViens.Where(x => x.TenNV == name).Select(x => x.MaNV).FirstOrDefault();
        }
        private int Find(string Name, DateTime date)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            return quanLyQuanCaPheEntities.ChamCongs.Where(x => x.NhanVien.TenNV == Name).Where(x => x.NgayLam == date).Select(x => x.MaChamCong).FirstOrDefault();
        }

        public DataTable Detail(string searchText)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();

            var temp = (from cc in quanLyQuanCaPheEntities.ChamCongs
                        where (cc.NhanVien.TenNV.ToLower().Contains(searchText.ToLower()) ||
                               cc.NgayLam.ToString().ToLower().Contains(searchText.ToLower()) ||
                               cc.GhiChu.ToLower().Contains(searchText.ToLower()))
                        select cc);

            //var _temp = quanLyQuanCaPheEntities.ChamCongs.Where(x=>x.NhanVien.TenNV.Contains(temp)).Select(x => x);
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã chấm công");
            dt.Columns.Add("Tên nhân viên");
            dt.Columns.Add("Ngày làm");
            dt.Columns.Add("Số ca làm trong ngày");
            dt.Columns.Add("Ghi chú");
            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaChamCong, item.NhanVien.TenNV, item.NgayLam.Value.ToShortDateString(), item.SoCaLam, item.GhiChu);
            }
            return dt;
        }
    }
}

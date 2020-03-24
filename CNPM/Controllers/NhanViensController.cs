using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using CNPM.Models;

namespace CNPM.Controllers
{
    class NhanViensController
    {
        public void Create(string ten, string cmnd, string dt, string dc, DateTime ngaylm, DateTime ngaysinh, string cv, string gt, string url)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            NhanVien nhanVien = new NhanVien();
            nhanVien.TenNV = ten;
            nhanVien.SoCMND = cmnd;
            nhanVien.SoDienThoai = dt;
            nhanVien.DiaChi = dc;
            nhanVien.NgayVaoLam = ngaylm;
            nhanVien.NgaySinh = ngaysinh;
            nhanVien.ChucVu = cv;
            nhanVien.GioiTinh = gt;
            nhanVien.Xoa = false;
            if (url != @"\Images\NhanVien\noimg.png")
                nhanVien.HinhAnh = CopyImage(url);
            else
                nhanVien.HinhAnh = url;

            quanLyQuanCaPheEntities.NhanViens.Add(nhanVien);
            quanLyQuanCaPheEntities.SaveChanges();
        }

        public void Edit(int id, string ten, string cmnd, string dt, string dc, DateTime ngaylm, DateTime ngaysinh, string cv, string gt, string url)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            TaiKhoansController tk = new TaiKhoansController();
            var temp = quanLyQuanCaPheEntities.NhanViens.Find(id);
            int right;
            if (temp != null)
            {
                temp.TenNV = ten;
                temp.SoCMND = cmnd;
                temp.SoDienThoai = dt;
                temp.DiaChi = dc;
                temp.NgayVaoLam = ngaylm;
                temp.NgaySinh = ngaysinh;
                temp.ChucVu = cv;

                if (cv == "Quản Lý")
                    right = 0;
                else if (cv == "Thủ Kho")
                    right = 1;
                else
                    right = 2;
                tk.UpdateRight(id, right);

                temp.GioiTinh = gt;
                if (url != null)
                {
                    temp.HinhAnh = CopyImage(url);
                }
                quanLyQuanCaPheEntities.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var _temp = quanLyQuanCaPheEntities.NhanViens.Find(id);
            //var _temp = new NhanVien { MaNV = id };
            //quanLyQuanCaPheEntities.Entry(_temp).State = System.Data.Entity.EntityState.Deleted;
            //quanLyQuanCaPheEntities.SaveChanges();
            if (_temp != null)
            {
                _temp.Xoa = true;
                quanLyQuanCaPheEntities.SaveChanges();
            }
        }

        private string CopyImage(string temp)
        {
            string fileName = temp.Substring(temp.LastIndexOf("/") + 1);
            string sourcePath = temp.Replace("/" + fileName, "").Replace("file:///", "").Replace("/", @"\");
            string targetPath = Environment.CurrentDirectory + @"\Images\NhanVien";
            string targetPath2 = @"\Images\NhanVien";
            string sourceFile = Path.Combine(@sourcePath, fileName);
            string destFile = Path.Combine(targetPath, fileName);
            string destFile2 = Path.Combine(targetPath2, fileName);
            Directory.CreateDirectory(targetPath);
            File.Copy(sourceFile, destFile, true);
            return destFile2;
        }

        public DataTable Detail()
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var temp = quanLyQuanCaPheEntities.NhanViens.Where(x => x.Xoa == false);
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nhân viên");
            dt.Columns.Add("Tên nhân viên");
            dt.Columns.Add("Ngày sinh");
            dt.Columns.Add("Số chứng minh");
            dt.Columns.Add("Điện thoại");
            dt.Columns.Add("Địa chỉ");
            dt.Columns.Add("Ngày vào làm");
            dt.Columns.Add("Chức vụ");
            dt.Columns.Add("Giới tính");
            dt.Columns.Add("url");
            foreach(var item in temp)
            {
                dt.Rows.Add(item.MaNV, item.TenNV, item.NgaySinh.Value.ToShortDateString(), item.SoCMND, item.SoDienThoai, item.DiaChi, item.NgayVaoLam.Value.ToShortDateString(), item.ChucVu, item.GioiTinh, item.HinhAnh);
            }
            return dt;
        }

        public DataTable Detail(string searchText)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();

            var temp = (from nv in quanLyQuanCaPheEntities.NhanViens
                         where (nv.Xoa == false && (nv.TenNV.ToLower().Contains(searchText.ToLower()) ||
                                nv.SoCMND.ToLower().Contains(searchText.ToLower()) ||
                                nv.SoDienThoai.ToLower().Contains(searchText.ToLower())) ||
                                nv.ChucVu.ToLower().Contains(searchText.ToLower()))
                         select nv);

            //var temp = quanLyQuanCaPheEntities.NhanViens.Where(x=>x.TenNV.Contains(serachText)).Where(x => x.Xoa == false);
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nhân viên");
            dt.Columns.Add("Tên nhân viên");
            dt.Columns.Add("Ngày sinh");
            dt.Columns.Add("Số chứng minh");
            dt.Columns.Add("Điện thoại");
            dt.Columns.Add("Địa chỉ");
            dt.Columns.Add("Ngày vào làm");
            dt.Columns.Add("Chức vụ");
            dt.Columns.Add("Giới tính");
            dt.Columns.Add("url");
            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaNV, item.TenNV, item.NgaySinh.Value.ToShortDateString(), item.SoCMND, item.SoDienThoai, item.DiaChi, item.NgayVaoLam.Value.ToShortDateString(), item.ChucVu, item.GioiTinh, item.HinhAnh);
            }
            return dt;
        }

    }
}

using CNPM.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CNPM.Controller
{
    class QLMonAnLoaiMon
    {

        #region LoaiMon
        public DataTable XemLoaiMon()
        {

            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã loại món");
            dt.Columns.Add("Tên loại món");

            var cQuery = from c in qlcf.LoaiMons where c.Xoa == false select c;
            foreach (var temp in cQuery)
            {
                dt.Rows.Add(temp.MaLoaiMon, temp.TenLoaiMon);
            }
            return dt;
        }
        public void XoaLoaiMon(int ma)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            var lmQuery = (from lm in qlcf.LoaiMons
                           where lm.MaLoaiMon == ma
                           select lm).SingleOrDefault();
            if (lmQuery != null)
                lmQuery.Xoa = true;
           
            qlcf.SaveChanges();
        }
        public void CapNhatLoaiMon(int ma, string ten)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            var lmQuery = (from lm in qlcf.LoaiMons
                           where lm.MaLoaiMon == ma
                           select lm).SingleOrDefault();           
            if (lmQuery != null)
            {
                lmQuery.MaLoaiMon = ma;
                lmQuery.TenLoaiMon = ten;
                qlcf.SaveChanges();
            }
        }
        public void ThemLoaiMon( string ten)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            
            LoaiMon lm = new LoaiMon();
            
            lm.TenLoaiMon = ten;
            lm.Xoa = false;
            qlcf.LoaiMons.Add(lm);
            qlcf.SaveChanges();
        }
        public DataTable TimKiemLoaiMon(string searchText)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã loại món");
            dt.Columns.Add("Tên loại món");

            var cQuery = from c in qlcf.LoaiMons
                         where (c.Xoa == false && c.TenLoaiMon.ToLower().Contains(searchText.ToLower()))
                         select c;

            foreach (var temp in cQuery)
            {
                dt.Rows.Add(temp.MaLoaiMon, temp.TenLoaiMon);
            }
            return dt;
        }




        #endregion
        #region Mon An
        public DataTable XemMonAn()
        {

            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("Mã món ăn");
            dt.Columns.Add("Tên món ăn");
            dt.Columns.Add("Tên loại món");
            dt.Columns.Add("Giá tiền");
            dt.Columns.Add("Hình ảnh");
            var cQuery = from c in qlcf.MonAns where c.xoa == false select c;
            foreach (var temp in cQuery)
            {
                dt.Rows.Add(temp.MaMonAn,temp.TenMonAn,LayTenLoaiMon(temp.MaLoaiMon), temp.Gia,temp.HinhAnh);
            }
            return dt;
        }
        public void XoaMonAn(int ma)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            var lmQuery = (from lm in qlcf.MonAns
                           where lm.MaMonAn == ma
                           select lm).SingleOrDefault();
            if (lmQuery != null)
            {
                lmQuery.xoa = true;
                qlcf.SaveChanges();
            }
            
        }
        public string LayTenLoaiMon(int maLoaiMon)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            var lmQuery = (from lm in qlcf.LoaiMons
                           where lm.MaLoaiMon == maLoaiMon
                           select lm).SingleOrDefault();
            return lmQuery.TenLoaiMon;
        }

        public int LayMaLoaiMon(string tenLoaiMon)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            var lmQuery = (from lm in qlcf.LoaiMons
                           where lm.TenLoaiMon == tenLoaiMon
                           select lm).SingleOrDefault();
            return lmQuery.MaLoaiMon;
        }
        public void ThemMonAn(string tenMonAn,string tenLoaiMon, int giaTien, string hinhAnh )
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();

            MonAn ma = new MonAn();

            ma.TenMonAn = tenMonAn;
            ma.MaLoaiMon = LayMaLoaiMon(tenLoaiMon);
            ma.Gia = giaTien;
            ma.HinhAnh = hinhAnh;
            ma.xoa = false;
            qlcf.MonAns.Add(ma);
            qlcf.SaveChanges();
        }
        public void CapNhatMonAn(int ma, string tenMonAn, string tenLoaiMon, int giaTien,string  hinhAnh)
        {
            string path = "";
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            var lmQuery = (from lm in qlcf.MonAns
                           where lm.MaMonAn == ma 
                           select lm).SingleOrDefault();
            
            if (lmQuery != null)
            {
                
                path = lmQuery.HinhAnh;
                // being used by another proj
                lmQuery.MaMonAn = ma;
                lmQuery.TenMonAn = tenMonAn;
                lmQuery.MaLoaiMon =LayMaLoaiMon( tenLoaiMon);
                lmQuery.Gia = giaTien;
                if( lmQuery.HinhAnh != hinhAnh)
                    lmQuery.HinhAnh = hinhAnh;
                qlcf.SaveChanges();
                

            }
            hinhAnh = path;
           
        }
        public DataTable TimKiemMonAn(string searchText)
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã loại món");
            dt.Columns.Add("Tên món ăn");
            dt.Columns.Add("Tên loại món");
            dt.Columns.Add("Giá tiền");
            dt.Columns.Add("Hình ảnh");
            var list = (from m in qlcf.MonAns
                        where (m.xoa == false && (m.TenMonAn.ToLower().Contains(searchText.ToLower()) ||
                                                  m.LoaiMon.TenLoaiMon.ToLower().Contains(searchText.ToLower())))
                        select m).ToList();
            foreach (var temp in list)
            {
                dt.Rows.Add(temp.MaMonAn, temp.TenMonAn, LayTenLoaiMon(temp.MaLoaiMon), temp.Gia, temp.HinhAnh);
            }
            
            return dt;
        }

        public DataTable DSLoaiMon()
        {
            QuanLyQuanCaPheEntities qlcf = new QuanLyQuanCaPheEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("Tên loại món");
            var list = (from lm in qlcf.LoaiMons where lm.Xoa == false select lm).ToList();
            foreach(var temp in list)
            {
                dt.Rows.Add(temp.TenLoaiMon);
            }
            
            return dt;
        }


        #endregion 
    }
}

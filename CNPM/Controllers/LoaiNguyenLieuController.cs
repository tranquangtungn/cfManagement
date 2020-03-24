using CNPM.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CNPM.Controllers
{
    class LoaiNguyenLieuController
    {
        public List<string> GetName()
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var dt = quanLyQuanCaPheEntities.LoaiNguyenLieux.Where(x => x.Xoa == false).Select(x => x.TenLoaiNL).ToList();
            

            return dt;
        }
        public int NameToId(string name)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            int id = qlcp.LoaiNguyenLieux.Where(x => x.TenLoaiNL == name).Select(x => x.MaLoaiNL).FirstOrDefault();
            return id;
        }
        public string IdToName(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            string name = qlcp.LoaiNguyenLieux.Where(x => x.MaLoaiNL == id).Select(x => x.TenLoaiNL).FirstOrDefault();
            return name;
        }
        public DataTable GetData()
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = (from lnl in qlcp.LoaiNguyenLieux
                        where lnl.Xoa == false
                        select lnl);
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã loại");
            dt.Columns.Add("Loại nguyên liệu");
            dt.Columns.Add("Mô tả");
            foreach (var item in temp)
            {
                dt.Rows.Add(item.MaLoaiNL, item.TenLoaiNL, item.MoTa);
            }

            return dt;
        }
        public void Edit(int id, string name, string des)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var temp = quanLyQuanCaPheEntities.LoaiNguyenLieux.Where(x => x.MaLoaiNL == id).FirstOrDefault();
            temp.TenLoaiNL = name;
            temp.MoTa = des;
            quanLyQuanCaPheEntities.SaveChanges();
        }
        public void Create(string name, string des)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            LoaiNguyenLieu lnl = new LoaiNguyenLieu();
            lnl.TenLoaiNL = name;
            lnl.MoTa = des;
            lnl.Xoa = false;
            quanLyQuanCaPheEntities.LoaiNguyenLieux.Add(lnl);
            quanLyQuanCaPheEntities.SaveChanges();
        }
        public void Delete(int id)
        {
            QuanLyQuanCaPheEntities qlcp = new QuanLyQuanCaPheEntities();
            var temp = qlcp.LoaiNguyenLieux.Where(x => x.MaLoaiNL == id).FirstOrDefault();
            temp.Xoa = true;
            qlcp.SaveChanges();
        }
        
    }
}

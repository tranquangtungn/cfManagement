using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using CNPM.Models;

namespace CNPM.Controllers
{
    class HomesController
    {
        public string GetName(int id)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var temp = quanLyQuanCaPheEntities.NhanViens.Where(x => x.MaNV == id).FirstOrDefault();
            return temp.TenNV;
        }
        public string GetImage(int id)
        {
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();
            var temp = quanLyQuanCaPheEntities.NhanViens.Where(x => x.MaNV == id).FirstOrDefault();
            return temp.HinhAnh;
        }


    }
}

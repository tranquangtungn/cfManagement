using CNPM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controllers
{
    class DoanhThusController
    {
        public DataTable LoadData(DateTime datefrom, DateTime dateto, ref Decimal? total )
        {
            total = 0;
            QuanLyQuanCaPheEntities quanLyQuanCaPheEntities = new QuanLyQuanCaPheEntities();

            var _temp = quanLyQuanCaPheEntities.ChiTietHoaDons.Where(x => x.HoaDon.NgayLap >= datefrom).Where(x => x.HoaDon.NgayLap <= dateto).Select(x => x);
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã hóa đơn");
            dt.Columns.Add("Tên món");
            dt.Columns.Add("Ngày lập");
            dt.Columns.Add("Số lượng");
            dt.Columns.Add("Đơn giá");
            dt.Columns.Add("Giảm giá");
            dt.Columns.Add("Thành tiền");
            foreach (var item in _temp)
            {
                dt.Rows.Add(item.MaHD, item.MonAn.TenMonAn, item.HoaDon.NgayLap.ToShortDateString(), item.SoLuong, item.MonAn.Gia, item.HoaDon.GiamGia.ToString() + "%", (item.SoLuong * item.MonAn.Gia) *(100 - item.HoaDon.GiamGia) / 100);
                total = total + (item.SoLuong * item.MonAn.Gia) * (100 - item.HoaDon.GiamGia) / 100;
            }
            return dt;
        }
    }
}

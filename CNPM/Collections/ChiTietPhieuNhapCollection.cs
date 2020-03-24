using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Collections
{
    class ChiTietPhieuNhapCollection
    {
        string tenNL;
        int soLuongNhap;
        int gia;
        string ghiChu;
        string hanSuDung;
        int tongTien;


        public string TenNL
        {
            get { return tenNL; }
            set { tenNL = value; }
        }
        public int SoLuongNhap
        {
            get { return soLuongNhap; }
            set { soLuongNhap = value; }
        }
        public string HanSuDung
        {
            get { return hanSuDung; }
            set { hanSuDung = value; }
        }
        public int Gia
        {
            get { return gia; }
            set { gia = value; }
        }
        public string GhiChu
        {
            get { return ghiChu; }
            set { ghiChu = value; }
        }
        public int TongTien
        {
            get { return tongTien; }
            set { tongTien = value; }
        }

    }
}

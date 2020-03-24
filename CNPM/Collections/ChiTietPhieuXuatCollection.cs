using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Collections
{
    class ChiTietPhieuXuatCollection
    {
        string tenNL;
        int soLuongXuat;
        string ghiChu;

        public string TenNL
        {
            get { return tenNL; }
            set { tenNL = value; }
        }
        public int SoLuongXuat
        {
            get { return soLuongXuat; }
            set { soLuongXuat = value; }
        }
        public string GhiChu
        {
            get { return ghiChu; }
            set { ghiChu = value; }
        }
    }
}

using CNPM.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNPM.Views
{
    /// <summary>
    /// Interaction logic for ucXuatHoaDon.xaml
    /// </summary>
    public partial class ucXuatHoaDon : UserControl
    {
        public ucXuatHoaDon()
        {
            InitializeComponent();
        }
        public ucXuatHoaDon(int ma)
        {
            InitializeComponent();
            QLGoiMon qlgm = new QLGoiMon();
            // null
            txtGiamGia.Text = qlgm.LayGiamGia(ma).ToString();
            
            txtNgayLap.Text = qlgm.LayNgayLap(ma).ToString();
            dgvHoaDon.ItemsSource = qlgm.XemChiTietHD(ma).DefaultView;
            DataTable dt = qlgm.XemChiTietHD(ma);
            int index = dt.Rows.Count;
            int rows = dt.Rows.Count;
            int kq = 0;
            int giamgia = Convert.ToInt32(txtGiamGia.Text);
            for (int i = 0; i < rows; i++)
                kq = kq + Convert.ToInt32(dt.Rows[i]["Thành tiền"].ToString());
            //tbxTongTien.Text = kq.ToString();
            txtTongTien.Text = (kq - kq * giamgia / 100).ToString();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.Parent as Window;

            if (parent != null)
            {
                parent.DialogResult = true;
                parent.Close();
            }
        }
    }
}

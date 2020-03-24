using System;
using System.Collections.Generic;
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
    /// Interaction logic for ucXuatPhieuLuong.xaml
    /// </summary>
    public partial class ucXuatPhieuLuong : UserControl
    {
        public ucXuatPhieuLuong()
        {
            InitializeComponent();
        }

        public ucXuatPhieuLuong(string ma, string ten, string soca, string luongCoBan, string tongLuong)
        {
            InitializeComponent();
            txtMaNV.Text = ma;
            txtTenNhanVien.Text = ten;
            txtTongCa.Text = soca;
            txtLuongCoBan.Text = luongCoBan;
            txtTongCong.Text = tongLuong;
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

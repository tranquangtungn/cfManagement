using CNPM.Controllers;
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
    /// Interaction logic for RevenueManagement.xaml
    /// </summary>
    public partial class RevenueManagement : UserControl
    {
        Decimal? total;
        public int right;
        public RevenueManagement()
        {
            InitializeComponent();
            btThongKe.Click += BtThongKe_Click;
            Loaded += RevenueManagement_Loaded;
        }

        private void RevenueManagement_Loaded(object sender, RoutedEventArgs e)
        {
            if (right == 2)
            {
                dtpFrom.SelectedDate = DateTime.Today;
                dtpTo.SelectedDate = DateTime.Today;
                dtpFrom.IsEnabled = false;
                dtpTo.IsEnabled = false;
            }
        }

        private void BtThongKe_Click(object sender, RoutedEventArgs e)
        {
            if (dtpFrom.SelectedDate == null)
            {
                MessageBox.Show("Ngày bắt đầu không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                dtpFrom.Focus();
            }
            else if (dtpTo.SelectedDate == null)
            {
                MessageBox.Show("Ngày kết thúc không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                dtpFrom.Focus();
            }
            else
            {
                DoanhThusController doanhThusController = new DoanhThusController();
                dgvDoanhThu.ItemsSource = doanhThusController.LoadData(dtpFrom.SelectedDate.Value.Date, dtpTo.SelectedDate.Value.Date, ref total).DefaultView;
                
            }
            if(total == 0)
            {
                MessageBox.Show("Không có doanh thu trong thời gian này!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                lblTotal.Content = total.ToString() + " VNĐ";
            }
            else
                lblTotal.Content = total.ToString() + " VNĐ";
        }
    }
}

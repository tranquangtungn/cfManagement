using CNPM.Controllers;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNPM
{
    public partial class ucTimeKeepManagement : System.Windows.Controls.UserControl
    {
        TaiKhoansController taiKhoansController = new TaiKhoansController();
        ChamCongsController chamCongSController = new ChamCongsController();
        public ucTimeKeepManagement()
        {
            InitializeComponent();
            Loaded += UcTimeKeepManagement_Loaded;
            btnReaload.Click += BtnReaload_Click;
            dgvChamCong.SelectionChanged += DgvChamCong_SelectionChanged;
            btnInsert.Click += BtnInsert_Click;
            btnDelete.Click += BtnDelete_Click;
            btnCancel.Click += BtnCancel_Click;
            btnSave.Click += BtnSave_Click;
            FindNhanVien.TextChanged += FindNhanVien_TextChanged;
        }

        private void FindNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FindNhanVien.Text != "" && FindNhanVien.Text != null)
                Search();
            else
                LoadData();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(cboSocalam.Text))
            {
                System.Windows.MessageBox.Show("Số ca làm không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                cboSocalam.Focus();
            }
            else if(string.IsNullOrEmpty(dtpNgayLam.Text))
            {
                System.Windows.MessageBox.Show("Ngày làm không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                dtpNgayLam.Focus();
            }
            else
            {
                chamCongSController.Create(cboTenNhanVien.SelectedItem.ToString(), Convert.ToDateTime(dtpNgayLam.SelectedDate), Convert.ToInt32(cboSocalam.SelectionBoxItem), txtNote.Text);
                LoadData();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult flag = System.Windows.Forms.MessageBox.Show("Bạn chắc chắn muốn xóa mẫu chấm công đang chọn?", "Question!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (flag == DialogResult.OK)
            {
                chamCongSController.Delete(cboTenNhanVien.SelectedItem.ToString(), Convert.ToDateTime(dtpNgayLam.SelectedDate));
                System.Windows.MessageBox.Show("Đã xóa mẫu chấm công vừa chọn!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
            else
            {
                System.Windows.MessageBox.Show("Xác nhận không xóa mẫu chấm công vừa chọn!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            cboSocalam.IsEnabled = true;
            cboTenNhanVien.IsEnabled = true;
            dtpNgayLam.IsEnabled = true;
            txtNote.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnInsert.IsEnabled = false;
            btnDelete.IsEnabled = false;
            cboSocalam.Text = null;
            txtNote.Text = null;
            dtpNgayLam.Text = null;
        }

        private void DgvChamCong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HomesController homesController = new HomesController();
            DataRowView row = dgvChamCong.SelectedItem as DataRowView;
            if (row != null)
            {
                cboTenNhanVien.Text = row.Row.ItemArray[1].ToString();
                dtpNgayLam.Text = row.Row.ItemArray[2].ToString();
                cboSocalam.Text = row.Row.ItemArray[3].ToString();
                txtNote.Text = row.Row.ItemArray[4].ToString();
            }
        }

        private void BtnReaload_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void UcTimeKeepManagement_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            cboTenNhanVien.ItemsSource = taiKhoansController.DsNhanVien();
            dgvChamCong.ItemsSource = chamCongSController.Details().DefaultView;
            dgvChamCong.Columns[0].Visibility = Visibility.Collapsed;
            cboSocalam.IsEnabled = false;
            cboTenNhanVien.IsEnabled = false;
            dtpNgayLam.IsEnabled = false;
            txtNote.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnInsert.IsEnabled = true;
            btnDelete.IsEnabled = true;
            dgvChamCong.SelectedIndex = 0;
        }
        private void Search()
        {
            cboTenNhanVien.ItemsSource = taiKhoansController.DsNhanVien();
            dgvChamCong.ItemsSource = chamCongSController.Details().DefaultView;
            dgvChamCong.Columns[0].Visibility = Visibility.Collapsed;
            cboSocalam.IsEnabled = false;
            cboTenNhanVien.IsEnabled = false;
            dtpNgayLam.IsEnabled = false;
            txtNote.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnInsert.IsEnabled = true;
            btnDelete.IsEnabled = true;
            dgvChamCong.SelectedIndex = 0;
        }
    }
}

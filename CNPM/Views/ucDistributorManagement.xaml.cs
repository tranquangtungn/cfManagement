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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNPM.Views
{
    public partial class ucDistributorManagement : UserControl
    {

        private bool isEditing = false;
        private int nccId = 0;
        public ucDistributorManagement()
        {
            InitializeComponent();

            Loaded += UcDistributorManagement_Loaded;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += BtnHuy_Click;
            btnReload.Click += BtnReload_Click;

            dgvNhaCungCap.SelectionChanged += DgvNhaCungCap_SelectionChanged;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
        }

        private void TxtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTimKiem.Text != "")
                SearchNhaCungCap();
            else
                LoadNhaCungCap();
        }

        private void DgvNhaCungCap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dgvNhaCungCap.SelectedItem as DataRowView;

            if (row != null)
            {
                txtTenNCC.Text = row.Row.ItemArray[1].ToString();
                txtDiaChi.Text = row.Row.ItemArray[2].ToString();
                txtSoDienThoai.Text = row.Row.ItemArray[3].ToString();
                nccId = Convert.ToInt32(row.Row.ItemArray[0].ToString());
            }
        }

        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            if (txtTimKiem.Text != "")
                SearchNhaCungCap();
            else
                LoadNhaCungCap();
        }

        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            isEditing = false;
            dgvNhaCungCap.IsEnabled = true;

            btnThem.IsEnabled = true;
            btnSua.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnReload.IsEnabled = true;
            btnHuy.IsEnabled = false;
            btnLuu.IsEnabled = false;

            txtTenNCC.IsEnabled = false;
            txtDiaChi.IsEnabled = false;
            txtSoDienThoai.IsEnabled = false;
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            NhaCungCapController ncc = new NhaCungCapController();

            if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTenNCC.Focus();
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn phải nhập địa chỉ nhà cung cấp!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtDiaChi.Focus();
            }
            else if (txtSoDienThoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số điện thoại nhà cung cấp!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtSoDienThoai.Focus();
            }
            else
            {
                if (isEditing)
                {
                    ncc.Edit(nccId, txtTenNCC.Text, txtDiaChi.Text, txtSoDienThoai.Text);
                    isEditing = false;
                    dgvNhaCungCap.IsEnabled = true;
                }
                else
                {
                    ncc.Create(txtTenNCC.Text, txtDiaChi.Text, txtSoDienThoai.Text);
                }

                dgvNhaCungCap.IsEnabled = true;
                LoadNhaCungCap();
                btnThem.IsEnabled = true;
                btnSua.IsEnabled = true;
                btnXoa.IsEnabled = true;
                btnReload.IsEnabled = true;
                btnLuu.IsEnabled = false;
                btnHuy.IsEnabled = false;
                txtTenNCC.IsEnabled = false;
                txtDiaChi.IsEnabled = false;
                txtSoDienThoai.IsEnabled = false;
            }
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này?", "Xác nhận!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                NhaCungCapController ncc = new NhaCungCapController();
                try
                {
                    ncc.Delete(nccId);
                    MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadNhaCungCap();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            isEditing = true;
            dgvNhaCungCap.IsEnabled = false;
            btnThem.IsEnabled = false;
            btnSua.IsEnabled = false;
            btnXoa.IsEnabled = false;
            btnHuy.IsEnabled = true;
            btnLuu.IsEnabled = true;
            btnReload.IsEnabled = false;

            txtTenNCC.IsEnabled = true;
            txtDiaChi.IsEnabled = true;
            txtSoDienThoai.IsEnabled = true;
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            dgvNhaCungCap.IsEnabled = false;
            btnThem.IsEnabled = false;
            btnSua.IsEnabled = false;
            btnXoa.IsEnabled = false;
            btnHuy.IsEnabled = true;
            btnLuu.IsEnabled = true;
            btnReload.IsEnabled = false;

            txtTenNCC.Text = string.Empty;
            txtTenNCC.IsEnabled = true;
            txtDiaChi.Text = string.Empty;
            txtDiaChi.IsEnabled = true;
            txtSoDienThoai.Text = string.Empty;
            txtSoDienThoai.IsEnabled = true;
        }

        private void UcDistributorManagement_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNhaCungCap();

            btnThem.IsEnabled = true;
            btnSua.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnReload.IsEnabled = true;
            btnHuy.IsEnabled = false;
            btnLuu.IsEnabled = false;

            txtTenNCC.IsEnabled = false;
            txtDiaChi.IsEnabled = false;
            txtSoDienThoai.IsEnabled = false;
        }

        private void LoadNhaCungCap()
        {
            NhaCungCapController ncc = new NhaCungCapController();
            dgvNhaCungCap.ItemsSource = ncc.GetData().DefaultView;

            dgvNhaCungCap.Columns[0].Visibility = Visibility.Collapsed;
            dgvNhaCungCap.SelectedIndex = 0;
        }
        private void SearchNhaCungCap()
        {
            NhaCungCapController ncc = new NhaCungCapController();
            dgvNhaCungCap.ItemsSource = ncc.Search(txtTimKiem.Text).DefaultView;

            dgvNhaCungCap.Columns[0].Visibility = Visibility.Collapsed;
            dgvNhaCungCap.SelectedIndex = 0;
        }
    }
}

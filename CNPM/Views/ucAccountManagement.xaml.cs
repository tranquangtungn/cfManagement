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
    /// <summary>
    /// Interaction logic for AccountManagement.xaml
    /// </summary>
    public partial class ucAccountManagement : System.Windows.Controls.UserControl
    {
        private TaiKhoansController taiKhoansController = new TaiKhoansController();
        private Boolean them;
        public ucAccountManagement()
        {
            InitializeComponent();
            Loaded += AccountManagement_Loaded;
            btnInsert.Click += BtnInsert_Click;
            btnReaload.Click += BtnReaload_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
            btnReset.Click += BtnReset_Click;
            dgvTaiKhoan.SelectionChanged += DgvTaiKhoan_SelectionChanged;
            btnCancel.Click += BtnCancel_Click;
            cboTenNhanVien.SelectionChanged += CboTenNhanVien_SelectionChanged;
        }

        private void CboTenNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int flag = Convert.ToInt32(taiKhoansController.FindID(cboTenNhanVien.SelectionBoxItem.ToString()));
            if (flag != -1)
            {
                HomesController homesController = new HomesController();
                try
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(Environment.CurrentDirectory + @homesController.GetImage(taiKhoansController.FindID(cboTenNhanVien.SelectionBoxItem.ToString())));
                    bitmapImage.EndInit();
                    pctbAvatar.Source = bitmapImage;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoadData();

            dgvTaiKhoan.IsEnabled = true;
        }

        private void DgvTaiKhoan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HomesController homesController = new HomesController();
            DataRowView row = dgvTaiKhoan.SelectedItem as DataRowView;
            if (row != null)
            {
                cboTenNhanVien.Text = row.Row.ItemArray[1].ToString();
                txtAccountUser.Text = row.Row.ItemArray[3].ToString();
                txtEmail.Text = row.Row.ItemArray[4].ToString();
                chkTinhTrang.IsChecked = Convert.ToBoolean(row.Row.ItemArray[5].ToString());
                try
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(@Environment.CurrentDirectory + @homesController.GetImage(Convert.ToInt32(row.Row.ItemArray[0].ToString())));
                    bitmapImage.EndInit();
                    pctbAvatar.Source = bitmapImage;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            DialogResult flag = System.Windows.Forms.MessageBox.Show("Bạn muốn cấp lại mật khẩu cho tài khoản đang chọn?", "Question!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (flag == DialogResult.OK)
            {
                try
                {
                    taiKhoansController.FogotPassWords(txtEmail.Text);
                    taiKhoansController.ChangeStatus(txtAccountUser.Text.ToString());
                    System.Windows.MessageBox.Show("Kiểm tra thư điện tử của bạn để lấy mật khẩu mới!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                }
                catch
                {
                    System.Windows.MessageBox.Show("Không thể cấp mật khẩu mới. Vui lòng thử lại!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Xác nhận không cấp lại mật khẩu cho tài khoản vừa chọn!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                System.Windows.MessageBox.Show("Thư điện tử không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.Focus();
            }
            else if (string.IsNullOrEmpty(txtAccountUser.Text))
            {
                System.Windows.MessageBox.Show("Tên đăng nhập không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAccountUser.Focus();
            }
            else
            {
                try
                {
                    if (them)
                    {
                        taiKhoansController.Create(cboTenNhanVien.SelectionBoxItem.ToString(), txtAccountUser.Text, txtEmail.Text, chkTinhTrang.IsChecked.Value);
                        System.Windows.MessageBox.Show("Đã tạo mới tài khoản! Tên đăng nhập và mật khẩu đã được gửi qua thư điện tử của nhân viên.", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    else
                    {
                        taiKhoansController.Edit(cboTenNhanVien.SelectionBoxItem.ToString(), txtAccountUser.Text, txtEmail.Text, chkTinhTrang.IsChecked.Value);
                        System.Windows.MessageBox.Show("Đã sửa đổi thông tin tài khoản!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }

                    cboTenNhanVien.IsEnabled = false;
                    txtAccountUser.IsEnabled = false;
                    txtEmail.IsEnabled = false;

                    dgvTaiKhoan.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DialogResult flag = System.Windows.Forms.MessageBox.Show("Bạn chắc chắn muốn xóa tài khoản đang chọn?", "Question!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (flag == DialogResult.OK)
            {
                try
                {
                    taiKhoansController.Delete(txtAccountUser.Text);
                    System.Windows.MessageBox.Show("Đã xóa tài khoản vừa chọn!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Xác nhận không xóa tài khoản vừa chọn!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            them = false;
            btnInsert.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnCancel.IsEnabled = true;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;

            cboTenNhanVien.IsEnabled = true;
            txtEmail.IsEnabled = true;

            chkTinhTrang.IsEnabled = true;

            dgvTaiKhoan.IsEnabled = false;
        }

        private void BtnReaload_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            them = true;
            btnInsert.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnCancel.IsEnabled = true;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            cboTenNhanVien.IsEnabled = true;

            txtAccountUser.IsEnabled = true;

            txtEmail.IsEnabled = true;

            chkTinhTrang.IsEnabled = true;
            txtAccountUser.Text = null;
            txtEmail.Text = null;
            chkTinhTrang.IsChecked = false;
            dgvTaiKhoan.IsEnabled = false;
        }

        private void AccountManagement_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();

        }
        private void LoadData()
        {
            cboTenNhanVien.ItemsSource = taiKhoansController.DsNhanVien();
            dgvTaiKhoan.ItemsSource = taiKhoansController.Details().DefaultView;
            dgvTaiKhoan.Columns[0].Visibility = Visibility.Collapsed;
            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnInsert.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnEdit.IsEnabled = true;
            cboTenNhanVien.IsEnabled = false;
            txtAccountUser.IsEnabled = false;
            txtEmail.IsEnabled = false;
            chkTinhTrang.IsEnabled = false;
            dgvTaiKhoan.SelectedIndex = 0;
        }
    }
}

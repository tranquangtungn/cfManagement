using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using CNPM.Controllers;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CNPM
{
    /// <summary>
    /// Interaction logic for ucEmpManagement.xaml
    /// </summary>
    public partial class ucEmpManagement : System.Windows.Controls.UserControl
    {
        private Boolean them;
        private string location = null;
        private NhanViensController nhanViensController = new NhanViensController();
        private int nvId = 0;

        //private BitmapImage bitmapImage = new BitmapImage();
        public ucEmpManagement()
        {
            InitializeComponent();
            Loaded += UcEmpManagement_Loaded;
            btnSave.Click += BtnSave_Click;
            btnSelectImg.Click += BtnSelectImg_Click;
            btnInsert.Click += BtnInsert_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnCancel.Click += BtnCancel_Click;
            btnReaload.Click += BtnReaload_Click;
            dgvNhanVien.SelectionChanged += DgvNhanVien_SelectionChanged;
            txtFind.TextChanged += TxtFind_TextChanged;
        }

        private void TxtFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFind.Text != "" && txtFind.Text != null)
                Search();
            else
                LoadData();
        }

        private void DgvNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HomesController homesController = new HomesController();
            DataRowView row = dgvNhanVien.SelectedItem as DataRowView;
            if (row != null)
            {
                txtTen.Text = row.Row.ItemArray[1].ToString();
                txtID.Text = row.Row.ItemArray[0].ToString();
                nvId = Convert.ToInt32(row.Row.ItemArray[0].ToString());
                txtCMND.Text = row.Row.ItemArray[3].ToString();
                txtdc.Text = row.Row.ItemArray[5].ToString();
                txtSdt.Text = row.Row.ItemArray[4].ToString();
                ngayLam.Text = row.Row.ItemArray[6].ToString();
                ngaySinh.Text = row.Row.ItemArray[2].ToString();
                cbxChucVu.Text = row.Row.ItemArray[7].ToString();
                cbxgt.Text = row.Row.ItemArray[8].ToString();
                //location = row.Row.ItemArray[9].ToString();
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(Environment.CurrentDirectory + @homesController.GetImage(Convert.ToInt32(txtID.Text)));
                bitmapImage.EndInit();
                pctbAvartar.Source = bitmapImage;
            }
        }
        private void Search()
        {
            grbNhanVien.IsEnabled = false;
            btnInsert.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnSelectImg.IsEnabled = false;
            btnCancel.IsEnabled = false;
            dgvNhanVien.ItemsSource = nhanViensController.Detail(txtFind.Text).DefaultView;
            dgvNhanVien.SelectedIndex = 0;
            dgvNhanVien.Columns[9].Visibility = Visibility.Collapsed;
            dgvNhanVien.Columns[0].Visibility = Visibility.Collapsed;
        }
        private void LoadData()
        {
            grbNhanVien.IsEnabled = false;
            btnInsert.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnSelectImg.IsEnabled = false;
            btnCancel.IsEnabled = false;
            dgvNhanVien.ItemsSource = nhanViensController.Detail().DefaultView;
            dgvNhanVien.SelectedIndex = 0;
            dgvNhanVien.Columns[9].Visibility = Visibility.Collapsed;
            dgvNhanVien.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void UcEmpManagement_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnReaload_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            dgvNhanVien.IsEnabled = true;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = dgvNhanVien.SelectedItem as DataRowView;
                int index = Convert.ToInt32(row.Row.ItemArray[0].ToString());
                DialogResult flag = System.Windows.Forms.MessageBox.Show("Chắc chắn xóa nhân viên đang chọn?", "Question!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (flag == DialogResult.OK)
                {
                    nhanViensController.Delete(index);
                    LoadData();
                    dgvNhanVien.SelectedIndex = 0;
                    System.Windows.Forms.MessageBox.Show("Đã xóa nhân viên vừa chọn!", "Notify!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Không xóa nhân viên vừa chọn!", "Notify!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            them = false;
            grbNhanVien.IsEnabled = true;
            btnSelectImg.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnInsert.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            dgvNhanVien.IsEnabled = false;
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            them = true;
            grbNhanVien.IsEnabled = true;
            
            txtTen.Text = "";
            txtSdt.Text = "";
            txtCMND.Text = "";
            txtdc.Text = "";
            txtID.Text = "";
            btnSelectImg.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnInsert.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            dgvNhanVien.IsEnabled = false;
        }

        private void BtnSelectImg_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage();
            var ofd = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };
            var result = ofd.ShowDialog();
            if (result == false) return;
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(@"" + ofd.FileName);
            location = new Uri(@"" + ofd.FileName).ToString();
            bitmapImage.EndInit();
            pctbAvartar.Source = bitmapImage;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            NhanViensController nhanViensController = new NhanViensController();
            if (string.IsNullOrEmpty(txtTen.Text))
            {
                System.Windows.MessageBox.Show("Tên nhân viên không được bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTen.Focus();
            }
            else if (string.IsNullOrEmpty(txtCMND.Text))
            {
                System.Windows.MessageBox.Show("Chứng minh nhân dân không được bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtCMND.Focus();
            }
            else if (string.IsNullOrEmpty(txtdc.Text))
            {
                System.Windows.MessageBox.Show("Địa chỉ không được bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtdc.Focus();
            }
            else if (string.IsNullOrEmpty(txtSdt.Text))
            {
                System.Windows.MessageBox.Show("Số điện thoại không được bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtSdt.Focus();
            }
            else if (txtSdt.Text.Length > 10)
            {
                System.Windows.MessageBox.Show("Số điện thoại không hợp lệ!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtSdt.Focus();
            }
            else if (ngaySinh.SelectedDate == null)
            {
                System.Windows.MessageBox.Show("Ngày sinh không được bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                ngaySinh.Focus();
            }
            else if (ngayLam.SelectedDate == null)
            {
                System.Windows.MessageBox.Show("Ngày vào làm không được bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                ngayLam.Focus();
            }
            else if (pctbAvartar.Source == null)
            {
                System.Windows.MessageBox.Show("Hình ảnh không được bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                btnSelectImg.Focus();
            }
            else if (cbxChucVu.SelectionBoxItem == null)
            {
                System.Windows.MessageBox.Show("Chức vụ không được bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                cbxChucVu.Focus();
            }
            else if (cbxgt.SelectionBoxItem == null)
            {
                System.Windows.MessageBox.Show("Nhân viên phải có giới tính!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    if (them)
                    {
                        if (location == null)
                            location = @"\Images\NhanVien\noimg.png";
                        nhanViensController.Create(txtTen.Text, txtCMND.Text, txtSdt.Text, txtdc.Text, Convert.ToDateTime(ngayLam.SelectedDate), Convert.ToDateTime(ngaySinh.SelectedDate), cbxChucVu.Text, cbxgt.Text, location);
                        System.Windows.MessageBox.Show("Thêm nhân viên thành công!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        nhanViensController.Edit(nvId, txtTen.Text, txtCMND.Text, txtSdt.Text, txtdc.Text, Convert.ToDateTime(ngayLam.SelectedDate), Convert.ToDateTime(ngaySinh.SelectedDate), cbxChucVu.SelectionBoxItem.ToString(), cbxgt.SelectionBoxItem.ToString(), location);
                        System.Windows.MessageBox.Show("Chỉnh sửa thông tin nhân viên thành công!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    location = null;
                    dgvNhanVien.IsEnabled = true;
                    LoadData();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        
    }
}

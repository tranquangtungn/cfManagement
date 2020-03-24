using CNPM.Controllers;
using CNPM.Models;
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
using MessageBox = System.Windows.MessageBox;

namespace CNPM
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ucRawManagement : UserControl
    {
        private bool isEditing = false;
        private bool isEditingNL = false;
        private int loaiNguyenLieuID = 0;
        private int nguyenLieuID = 0;
        public ucRawManagement()
        {

            InitializeComponent();
            Loaded += UcRawManagement_Loaded;


            dgvLoaiNguyenLieu.SelectionChanged += DgvLoaiNguyenLieu_SelectionChanged;
            btnThemLoaiNL.Click += BtnThemLoaiNL_Click;
            btnSuaLoaiNL.Click += BtnSuaLoaiNL_Click;
            btnLuuLoaiNL.Click += BtnLuuLoaiNL_Click;
            btnLoadLoaiNL.Click += BtnLoadLoaiNL_Click;
            btnHuyLoaiNL.Click += BtnHuyLoaiNL_Click;
            btnXoaLoaiNL.Click += BtnXoaLoaiNL_Click;


            dgvNguyenLieu.SelectionChanged += DgvNguyenLieu_SelectionChanged;
            btnThemNL.Click += BtnThemNL_Click;
            btnSuaNL.Click += BtnSuaNL_Click;
            btnLuuNL.Click += BtnLuuNL_Click;
            btnReloadNL.Click += BtnReloadNL_Click;
            btnHuyNL.Click += BtnHuyNL_Click;
            btnXoaNL.Click += BtnXoaNL_Click;


            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
        }



        private void UcRawManagement_Loaded(object sender, RoutedEventArgs e)
        {
            //loại nguyên liệu
            LoaiNguyenLieuController loaiNguyenLieu = new LoaiNguyenLieuController();
            LoadLoaiNguyenLieu();

            //nguyên liệu
            NguyenLieuController nguyenLieu = new NguyenLieuController();
            LoadNguyenLieu();
            cbbLoaiNguyenLieu.ItemsSource = loaiNguyenLieu.GetName();

            btnHuyNL.IsEnabled = false;
            btnLuuNL.IsEnabled = false;

            txtTenNguyenLieu.IsEnabled = false;
            cbbLoaiNguyenLieu.IsEnabled = false;
            txtSoLuong.IsEnabled = false;
        }


        #region Loại Nguyên Liệu

        private void BtnXoaLoaiNL_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa loại nguyên liệu này?", "Xác nhận!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                LoaiNguyenLieuController loaiNguyenLieu = new LoaiNguyenLieuController();
                try
                {
                    loaiNguyenLieu.Delete(loaiNguyenLieuID);
                    MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadLoaiNguyenLieu();
                    cbbLoaiNguyenLieu.ItemsSource = loaiNguyenLieu.GetName();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnHuyLoaiNL_Click(object sender, RoutedEventArgs e)
        {
            isEditing = false;
            dgvLoaiNguyenLieu.IsEnabled = true;

            btnThemLoaiNL.IsEnabled = true;
            btnSuaLoaiNL.IsEnabled = true;
            btnXoaLoaiNL.IsEnabled = true;
            btnLoadLoaiNL.IsEnabled = true;
            btnHuyLoaiNL.IsEnabled = false;
            btnLuuLoaiNL.IsEnabled = false;

            txtTenLoaiNL.IsEnabled = false;
            txtMoTaLoaiNL.IsEnabled = false;
        }

        private void BtnLoadLoaiNL_Click(object sender, RoutedEventArgs e)
        {
            LoadLoaiNguyenLieu();
        }


        private void BtnLuuLoaiNL_Click(object sender, RoutedEventArgs e)
        {
            LoaiNguyenLieuController loaiNguyenLieu = new LoaiNguyenLieuController();
            if (txtTenLoaiNL.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên loại nguyên liệu!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTenNguyenLieu.Focus();
            }
            else if (txtMoTaLoaiNL.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mô tả loại nguyên liệu!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtSoLuong.Focus();
            }
            else
            {
                if (isEditing)
                {
                    loaiNguyenLieu.Edit(loaiNguyenLieuID, txtTenLoaiNL.Text, txtMoTaLoaiNL.Text);
                    isEditing = false;
                    btnThemLoaiNL.IsEnabled = true;
                    btnSuaLoaiNL.IsEnabled = true;
                    btnXoaLoaiNL.IsEnabled = true;
                    btnLoadLoaiNL.IsEnabled = true;
                    LoadLoaiNguyenLieu();
                    dgvLoaiNguyenLieu.IsEnabled = true;
                }
                else
                {
                    loaiNguyenLieu.Create(txtTenLoaiNL.Text, txtMoTaLoaiNL.Text);
                    btnThemLoaiNL.IsEnabled = true;
                    btnSuaLoaiNL.IsEnabled = true;
                    btnXoaLoaiNL.IsEnabled = true;
                    btnLoadLoaiNL.IsEnabled = true;
                    LoadLoaiNguyenLieu();
                    dgvLoaiNguyenLieu.IsEnabled = true;
                }

                cbbLoaiNguyenLieu.ItemsSource = loaiNguyenLieu.GetName();
            }
        }

        private void BtnSuaLoaiNL_Click(object sender, RoutedEventArgs e)
        {
            if (txtTenLoaiNL.Text == string.Empty)
            {
                MessageBox.Show("Phải chọn loại nguyên liệu trước khi sửa!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            dgvLoaiNguyenLieu.IsEnabled = false;
            isEditing = true;
            btnThemLoaiNL.IsEnabled = false;
            btnSuaLoaiNL.IsEnabled = false;
            btnXoaLoaiNL.IsEnabled = false;
            btnLoadLoaiNL.IsEnabled = false;
            btnHuyLoaiNL.IsEnabled = true;
            btnLuuLoaiNL.IsEnabled = true;

            txtTenLoaiNL.IsEnabled = true;
            txtMoTaLoaiNL.IsEnabled = true;
        }

        private void BtnThemLoaiNL_Click(object sender, RoutedEventArgs e)
        {
            dgvLoaiNguyenLieu.IsEnabled = false;
            btnThemLoaiNL.IsEnabled = false;
            btnSuaLoaiNL.IsEnabled = false;
            btnXoaLoaiNL.IsEnabled = false;
            btnHuyLoaiNL.IsEnabled = true;
            btnLuuLoaiNL.IsEnabled = true;
            btnLoadLoaiNL.IsEnabled = false;

            txtTenLoaiNL.Text = string.Empty;
            txtTenLoaiNL.IsEnabled = true;
            txtMoTaLoaiNL.Text = string.Empty;
            txtMoTaLoaiNL.IsEnabled = true;
        }

        private void DgvLoaiNguyenLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dgvLoaiNguyenLieu.SelectedItem as DataRowView;

            if (row != null)
            {
                txtTenLoaiNL.Text = row.Row.ItemArray[1].ToString();
                txtMoTaLoaiNL.Text = row.Row.ItemArray[2].ToString();
                loaiNguyenLieuID = Convert.ToInt32(row.Row.ItemArray[0].ToString());
            }
        }

        private void LoadLoaiNguyenLieu()
        {
            LoaiNguyenLieuController loaiNguyenLieu = new LoaiNguyenLieuController();
            dgvLoaiNguyenLieu.ItemsSource = loaiNguyenLieu.GetData().DefaultView;

            dgvLoaiNguyenLieu.Columns[0].Visibility = Visibility.Collapsed;
            dgvLoaiNguyenLieu.SelectedIndex = 0;

            btnHuyLoaiNL.IsEnabled = false;
            btnLuuLoaiNL.IsEnabled = false;

            txtTenLoaiNL.IsEnabled = false;
            txtMoTaLoaiNL.IsEnabled = false;
        }


        #endregion


        #region Nguyên Liệu

        private void TxtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTimKiem.Text != "")
                TimKiemNguyenLieu();
            else
                LoadNguyenLieu();

        }
        private void TimKiemNguyenLieu()
        {
            NguyenLieuController nguyenLieu = new NguyenLieuController();
            dgvNguyenLieu.ItemsSource = nguyenLieu.Search(txtTimKiem.Text).DefaultView;

            dgvNguyenLieu.Columns[0].Visibility = Visibility.Collapsed;
            dgvNguyenLieu.SelectedIndex = 0;

        }
        private void LoadNguyenLieu()
        {
            NguyenLieuController nguyenLieu = new NguyenLieuController();
            dgvNguyenLieu.ItemsSource = nguyenLieu.GetData().DefaultView;

            dgvNguyenLieu.Columns[0].Visibility = Visibility.Collapsed;
            dgvNguyenLieu.SelectedIndex = 0;

        }

        private void BtnXoaNL_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa nguyên liệu này?", "Xác nhận!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                NguyenLieuController nguyenLieu = new NguyenLieuController();
                try
                {
                    nguyenLieu.Delete(nguyenLieuID);
                    MessageBox.Show("Xóa thành công!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadNguyenLieu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnHuyNL_Click(object sender, RoutedEventArgs e)
        {
            isEditingNL = false;
            dgvNguyenLieu.IsEnabled = true;

            btnThemNL.IsEnabled = true;
            btnSuaNL.IsEnabled = true;
            btnXoaNL.IsEnabled = true;
            btnReloadNL.IsEnabled = true;
            btnHuyNL.IsEnabled = false;
            btnLuuNL.IsEnabled = false;

            txtTenNguyenLieu.IsEnabled = false;
            cbbLoaiNguyenLieu.IsEnabled = false;
            txtSoLuong.IsEnabled = false;
        }

        private void BtnReloadNL_Click(object sender, RoutedEventArgs e)
        {
            LoadNguyenLieu();
        }

        private void BtnLuuNL_Click(object sender, RoutedEventArgs e)
        {
            LoaiNguyenLieuController loaiNguyenLieu = new LoaiNguyenLieuController();
            NguyenLieuController nguyenLieu = new NguyenLieuController();
            if (txtTenNguyenLieu.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên nguyên liệu!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTenNguyenLieu.Focus();
            }
            else if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng nguyên liệu!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtSoLuong.Focus();
            }
            else
            {
                int maLoai = loaiNguyenLieu.NameToId(cbbLoaiNguyenLieu.Text);
                int soLuong = Convert.ToInt32(txtSoLuong.Text);
                if (isEditingNL)
                {
                    nguyenLieu.Edit(nguyenLieuID, txtTenNguyenLieu.Text, maLoai, soLuong);
                    isEditing = false;
                }
                else
                {
                    nguyenLieu.Create(txtTenNguyenLieu.Text, maLoai, soLuong);
                }
                btnHuyNL.IsEnabled = false;
                btnLuuNL.IsEnabled = false;
                btnThemNL.IsEnabled = true;
                btnSuaNL.IsEnabled = true;
                btnXoaNL.IsEnabled = true;
                btnReloadNL.IsEnabled = true;

                txtTenNguyenLieu.IsEnabled = false;
                txtSoLuong.IsEnabled = false;
                cbbLoaiNguyenLieu.IsEnabled = false;

                LoadNguyenLieu();
                dgvNguyenLieu.IsEnabled = true;
            }
        }

        private void BtnSuaNL_Click(object sender, RoutedEventArgs e)
        {
            dgvNguyenLieu.IsEnabled = false;
            isEditingNL = true;
            btnThemNL.IsEnabled = false;
            btnSuaNL.IsEnabled = false;
            btnXoaNL.IsEnabled = false;
            btnReloadNL.IsEnabled = false;
            btnHuyNL.IsEnabled = true;
            btnLuuNL.IsEnabled = true;


            cbbLoaiNguyenLieu.IsEnabled = true;
            txtTenNguyenLieu.IsEnabled = true;
            txtSoLuong.IsEnabled = true;
        }

        private void BtnThemNL_Click(object sender, RoutedEventArgs e)
        {
            dgvNguyenLieu.IsEnabled = false;
            btnThemNL.IsEnabled = false;
            btnSuaNL.IsEnabled = false;
            btnXoaNL.IsEnabled = false;
            btnHuyNL.IsEnabled = true;
            btnLuuNL.IsEnabled = true;
            btnReloadNL.IsEnabled = false;

            txtTenNguyenLieu.Text = string.Empty;
            txtTenNguyenLieu.IsEnabled = true;
            cbbLoaiNguyenLieu.IsEnabled = true;
            txtSoLuong.Text = string.Empty;
            txtSoLuong.IsEnabled = true;
        }

        private void DgvNguyenLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dgvNguyenLieu.SelectedItem as DataRowView;

            if (row != null)
            {
                txtTenNguyenLieu.Text = row.Row.ItemArray[1].ToString();
                cbbLoaiNguyenLieu.SelectedItem = row.Row.ItemArray[2].ToString();
                txtSoLuong.Text = row.Row.ItemArray[3].ToString();
                nguyenLieuID = Convert.ToInt32(row.Row.ItemArray[0].ToString());
            }
        }

        #endregion

    }
}

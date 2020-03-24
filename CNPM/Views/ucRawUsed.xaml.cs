using CNPM.Collections;
using CNPM.Controllers;
using CNPM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CNPM
{

    public partial class ucRawUsed : UserControl
    {
        public int manv;
        ObservableCollection<ChiTietPhieuXuatCollection> ctpx;
        private bool isEditing;
        private int phieuXuatId = 0;
        public ucRawUsed()
        {
            InitializeComponent();

            Loaded += UcRawUsed_Loaded;

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnLuu.Click += BtnLuu_Click;
            btnXoa.Click += BtnXoa_Click;
            btnHuy.Click += BtnHuy_Click;
            btnLuuPhieuXuat.Click += BtnLuuPhieuXuat_Click;
            btnChiTiet.Click += BtnChiTiet_Click;

            dgvPhieuXuat.SelectionChanged += DgvPhieuXuat_SelectionChanged;
            dgvDSPhieuXuat.SelectionChanged += DgvDSPhieuXuat_SelectionChanged;

            ctpx = new ObservableCollection<ChiTietPhieuXuatCollection>();
            dgvPhieuXuat.ItemsSource = ctpx;

            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
        }

        #region Phiếu xuất gần đây
        private void TxtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTimKiem.Text == "" || txtTimKiem.Text.Equals(string.Empty))
            {
                LoadDSPhieuXuat();
            }
            else
            {
                SearchDSPhieuXuat(txtTimKiem.Text);
            }
        }

        private void DgvDSPhieuXuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dgvDSPhieuXuat.SelectedItem as DataRowView;

            if (row != null)
                phieuXuatId = Convert.ToInt32(row.Row.ItemArray[0].ToString());
        }


        private void BtnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window
            {
                Title = "Chi Tiết Phiếu Xuất",
                Content = new ucUsedDetails(phieuXuatId),
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize
            };

            window.ShowDialog();
        }

        #endregion


        #region Phiếu xuất mới

        private void DgvPhieuXuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChiTietPhieuXuatCollection chitiet = dgvPhieuXuat.SelectedItem as ChiTietPhieuXuatCollection;

            if (chitiet != null)
            {
                cbbTenNguyenLieu.SelectedItem = chitiet.TenNL;
                txtSoLuong.Text = chitiet.SoLuongXuat.ToString();
                txtGhiChu.Text = chitiet.GhiChu;
            }
        }

        private void BtnLuuPhieuXuat_Click(object sender, RoutedEventArgs e)
        {
            if (ctpx.Count > 0)
            {
                PhieuXuatController phieuXuat = new PhieuXuatController();
                NguyenLieuController nguyenLieu = new NguyenLieuController();
                DateTime ngayXuat = DateTime.Now;

                try
                {
                    phieuXuat.Create(manv, ngayXuat, txtGhiChuPX.Text);

                    int maPX = phieuXuat.GetLastId();

                    foreach (var item in ctpx)
                    {
                        int maNL = nguyenLieu.NameToId(item.TenNL);
                        phieuXuat.CreateDetails(maPX, maNL, item.SoLuongXuat, item.GhiChu);
                    }

                    MessageBox.Show("Lưu phiếu xuất thành công!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);

                    ctpx.Clear();
                    cbbTenNguyenLieu.IsEnabled = false;
                    cbbTenNguyenLieu.SelectedIndex = 0;
                    txtSoLuong.IsEnabled = false;
                    txtSoLuong.Text = "";
                    txtGhiChu.IsEnabled = false;
                    txtGhiChu.Text = "";

                    LoadDSPhieuXuat();

                    txtGhiChuPX.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnHuy_Click(object sender, RoutedEventArgs e)
        {
            btnThem.IsEnabled = true;
            btnSua.IsEnabled = true;
            btnXoa.IsEnabled = true;
            btnLuu.IsEnabled = false;
            btnHuy.IsEnabled = false;

            cbbTenNguyenLieu.IsEnabled = false;
            txtSoLuong.IsEnabled = false;
            txtGhiChu.IsEnabled = false;
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa nguyên liệu này?", "Xác nhận!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                ChiTietPhieuXuatCollection rm = dgvPhieuXuat.SelectedItem as ChiTietPhieuXuatCollection;
                ctpx.Remove(rm);
            }

            dgvPhieuXuat.SelectedIndex = 0;
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtSoLuong.Focus();
            }
            else
            {
                NguyenLieuController nguyenLieu = new NguyenLieuController();
                ChiTietPhieuXuatCollection chitiet = new ChiTietPhieuXuatCollection();
                chitiet.TenNL = cbbTenNguyenLieu.Text;
                chitiet.SoLuongXuat = Convert.ToInt32(txtSoLuong.Text);
                chitiet.GhiChu = txtGhiChu.Text;
                if (isEditing)
                {
                    ChiTietPhieuXuatCollection rm = dgvPhieuXuat.SelectedItem as ChiTietPhieuXuatCollection;
                    ctpx.Remove(rm);
                    isEditing = false;
                }
                ctpx.Add(chitiet);

                btnThem.IsEnabled = true;
                btnSua.IsEnabled = true;
                btnXoa.IsEnabled = true;
                btnLuu.IsEnabled = false;
                btnHuy.IsEnabled = false;

                cbbTenNguyenLieu.IsEnabled = false;
                txtSoLuong.IsEnabled = false;
                txtGhiChu.IsEnabled = false;

                dgvPhieuXuat.SelectedIndex = 0;
            }
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            isEditing = true;

            btnThem.IsEnabled = false;
            btnSua.IsEnabled = false;
            btnXoa.IsEnabled = false;

            btnLuu.IsEnabled = true;
            btnHuy.IsEnabled = true;

            cbbTenNguyenLieu.IsEnabled = true;
            txtSoLuong.IsEnabled = true;
            txtGhiChu.IsEnabled = true;
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            btnThem.IsEnabled = false;
            btnSua.IsEnabled = false;
            btnXoa.IsEnabled = false;

            btnLuu.IsEnabled = true;
            btnHuy.IsEnabled = true;

            cbbTenNguyenLieu.IsEnabled = true;
            cbbTenNguyenLieu.SelectedIndex = 0;
            txtSoLuong.IsEnabled = true;
            txtSoLuong.Text = "";
            txtGhiChu.IsEnabled = true;
            txtGhiChu.Text = "";
        }

        private void UcRawUsed_Loaded(object sender, RoutedEventArgs e)
        {
            NguyenLieuController nguyenLieu = new NguyenLieuController();
            PhieuXuatController phieuXuat = new PhieuXuatController();

            cbbTenNguyenLieu.ItemsSource = nguyenLieu.GetName();

            btnLuu.IsEnabled = false;
            btnHuy.IsEnabled = false;

            cbbTenNguyenLieu.IsEnabled = false;
            txtSoLuong.IsEnabled = false;
            txtGhiChu.IsEnabled = false;

            LoadDSPhieuXuat();

            dgvPhieuXuat.Columns[0].Header = "Tên nguyên liệu";
            dgvPhieuXuat.Columns[1].Header = "Số lượng";
            dgvPhieuXuat.Columns[2].Header = "Ghi chú";
        }
        private void LoadDSPhieuXuat()
        {
            PhieuXuatController phieuXuat = new PhieuXuatController();

            dgvDSPhieuXuat.ItemsSource = phieuXuat.GetList().DefaultView;
            dgvDSPhieuXuat.Columns[0].Visibility = Visibility.Collapsed;
            dgvDSPhieuXuat.SelectedIndex = 0;
        }

        private void SearchDSPhieuXuat(string search)
        {
            PhieuXuatController phieuXuat = new PhieuXuatController();

            dgvDSPhieuXuat.ItemsSource = phieuXuat.SearchList(search).DefaultView;
            dgvDSPhieuXuat.Columns[0].Visibility = Visibility.Collapsed;
            dgvDSPhieuXuat.SelectedIndex = 0;
        }

        #endregion
    }
}

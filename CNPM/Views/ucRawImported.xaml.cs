using CNPM.Controllers;
using CNPM.Collections;
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
using CNPM.Views;

namespace CNPM
{
    public partial class ucRawImported : UserControl
    {
        public int manv;
        ObservableCollection<ChiTietPhieuNhapCollection> ctpn;
        private bool isEditing;
        private int phieuNhapId = 0;
        public ucRawImported()
        {
            InitializeComponent();
            Loaded += UcRawImported_Loaded;

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnLuu.Click += BtnLuu_Click;
            btnXoa.Click += BtnXoa_Click;
            btnHuy.Click += BtnHuy_Click;

            btnLuuPhieuNhap.Click += BtnLuuPhieuNhap_Click;

            btnChiTiet.Click += BtnChiTiet_Click;

            dgvPhieuNhap.SelectionChanged += DgvPhieuNhap_SelectionChanged;
            dgvDSPhieuNhap.SelectionChanged += DgvDSPhieuNhap_SelectionChanged;

            ctpn = new ObservableCollection<ChiTietPhieuNhapCollection>();
            dgvPhieuNhap.ItemsSource = ctpn;

            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
        }

        #region Phiếu nhập gần đây

        private void TxtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTimKiem.Text == "" || txtTimKiem.Text.Equals(string.Empty))
            {
                LoadDSPhieuNhap();
            }
            else
            {
                SearchDSPhieuNhap(txtTimKiem.Text);
            }
        }

        private void DgvDSPhieuNhap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dgvDSPhieuNhap.SelectedItem as DataRowView;

            if (row != null)
                phieuNhapId = Convert.ToInt32(row.Row.ItemArray[0].ToString());

        }

        private void BtnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window
            {
                Title = "Chi Tiết Phiếu Nhập",
                Content = new ucImportedDetails(phieuNhapId),
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize
            };

            window.ShowDialog();
        }

        #endregion

        #region Phiếu nhập mới

        private void BtnLuuPhieuNhap_Click(object sender, RoutedEventArgs e)
        {
            if (ctpn.Count > 0)
            {
                NhaCungCapController nhaCungCap = new NhaCungCapController();
                PhieuNhapController phieuNhap = new PhieuNhapController();
                NguyenLieuController nguyenLieu = new NguyenLieuController();
                int mancc = nhaCungCap.NameToId(cbbNhaCungCap.Text);
                DateTime ngayNhap = DateTime.Now;
                int tongTien = Convert.ToInt32(txtTongTien.Text);

                try
                {
                    phieuNhap.Create(manv, mancc, ngayNhap, txtGhiChuPN.Text, tongTien);

                    int maPN = phieuNhap.GetLastId();

                    foreach (var item in ctpn)
                    {
                        int maNL = nguyenLieu.NameToId(item.TenNL);
                        DateTime hsd = Convert.ToDateTime(item.HanSuDung);
                        phieuNhap.CreateDetails(maPN, maNL, hsd, item.SoLuongNhap, item.Gia, item.GhiChu);
                    }

                    MessageBox.Show("Lưu phiếu nhập thành công!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);

                    ctpn.Clear();
                    cbbTenNguyenLieu.IsEnabled = false;
                    cbbTenNguyenLieu.SelectedIndex = 0;
                    txtSoLuongNhap.IsEnabled = false;
                    txtSoLuongNhap.Text = "";
                    dtpHanSuDung.IsEnabled = false;
                    dtpHanSuDung.SelectedDate = DateTime.Now;
                    txtGia.IsEnabled = false;
                    txtGia.Text = "";
                    txtGhiChu.IsEnabled = false;
                    txtGhiChu.Text = "";

                    LoadDSPhieuNhap();

                    txtGhiChuPN.Text = "";
                    tongTien = 0;
                    txtTongTien.Text = tongTien.ToString();
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
            txtSoLuongNhap.IsEnabled = false;
            dtpHanSuDung.IsEnabled = false;
            txtGia.IsEnabled = false;
            txtGhiChu.IsEnabled = false;
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa nguyên liệu này?", "Xác nhận!", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                ChiTietPhieuNhapCollection rm = dgvPhieuNhap.SelectedItem as ChiTietPhieuNhapCollection;
                ctpn.Remove(rm);
            }

            dgvPhieuNhap.SelectedIndex = 0;
            int tongtien = 0;
            foreach (var item in ctpn)
            {
                tongtien += item.TongTien;
            }
            txtTongTien.Text = tongtien.ToString();
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtSoLuongNhap.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtSoLuongNhap.Focus();
            }
            else if (txtGia.Text == "")
            {
                MessageBox.Show("Bạn phải nhập giá nguyên liệu!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtGia.Focus();
            }
            else
            {
                NguyenLieuController nguyenLieu = new NguyenLieuController();
                ChiTietPhieuNhapCollection chitiet = new ChiTietPhieuNhapCollection();
                chitiet.TenNL = cbbTenNguyenLieu.Text;
                chitiet.SoLuongNhap = Convert.ToInt32(txtSoLuongNhap.Text);
                DateTime hsd = (DateTime)dtpHanSuDung.SelectedDate;
                chitiet.HanSuDung = hsd.ToShortDateString();
                chitiet.Gia = Convert.ToInt32(txtGia.Text);
                chitiet.GhiChu = txtGhiChu.Text;
                chitiet.TongTien = chitiet.Gia * chitiet.SoLuongNhap;

                if (isEditing)
                {
                    ChiTietPhieuNhapCollection rm = dgvPhieuNhap.SelectedItem as ChiTietPhieuNhapCollection;
                    ctpn.Remove(rm);
                    isEditing = false;
                }
                else
                {
                    ctpn.Add(chitiet);
                }

                btnThem.IsEnabled = true;
                btnSua.IsEnabled = true;
                btnXoa.IsEnabled = true;
                btnLuu.IsEnabled = false;
                btnHuy.IsEnabled = false;

                cbbTenNguyenLieu.IsEnabled = false;
                txtSoLuongNhap.IsEnabled = false;
                dtpHanSuDung.IsEnabled = false;
                txtGia.IsEnabled = false;
                txtGhiChu.IsEnabled = false;

                dgvPhieuNhap.SelectedIndex = 0;
                int tongtien = 0;
                foreach (var item in ctpn)
                {
                    tongtien += item.TongTien;
                }
                txtTongTien.Text = tongtien.ToString();
            }
        }

        private void DgvPhieuNhap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChiTietPhieuNhapCollection chitiet = dgvPhieuNhap.SelectedItem as ChiTietPhieuNhapCollection;

            if (chitiet != null)
            {
                cbbTenNguyenLieu.SelectedItem = chitiet.TenNL;
                txtSoLuongNhap.Text = chitiet.SoLuongNhap.ToString();
                dtpHanSuDung.SelectedDate = Convert.ToDateTime(chitiet.HanSuDung);
                txtGia.Text = chitiet.Gia.ToString();
                txtGhiChu.Text = chitiet.GhiChu;
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

            cbbTenNguyenLieu.IsEnabled = false;
            txtSoLuongNhap.IsEnabled = true;
            dtpHanSuDung.IsEnabled = true;
            txtGia.IsEnabled = true;
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
            txtSoLuongNhap.IsEnabled = true;
            txtSoLuongNhap.Text = "";
            dtpHanSuDung.IsEnabled = true;
            dtpHanSuDung.SelectedDate = DateTime.Now;
            txtGia.IsEnabled = true;
            txtGia.Text = "";
            txtGhiChu.IsEnabled = true;
            txtGhiChu.Text = "";
        }

        private void UcRawImported_Loaded(object sender, RoutedEventArgs e)
        {
            NhaCungCapController nhaCungCap = new NhaCungCapController();
            NguyenLieuController nguyenLieu = new NguyenLieuController();
            PhieuNhapController phieuNhap = new PhieuNhapController();

            cbbNhaCungCap.ItemsSource = nhaCungCap.GetName();
            cbbTenNguyenLieu.ItemsSource = nguyenLieu.GetName();

            cbbNhaCungCap.SelectedIndex = 0;

            btnLuu.IsEnabled = false;
            btnHuy.IsEnabled = false;

            cbbTenNguyenLieu.IsEnabled = false;
            txtSoLuongNhap.IsEnabled = false;
            dtpHanSuDung.IsEnabled = false;
            txtGia.IsEnabled = false;
            txtGhiChu.IsEnabled = false;

            LoadDSPhieuNhap();

            dgvPhieuNhap.Columns[0].Header = "Tên nguyên liệu";
            dgvPhieuNhap.Columns[1].Header = "Số lượng";
            dgvPhieuNhap.Columns[2].Header = "Hạn sử dụng";
            dgvPhieuNhap.Columns[3].Header = "Giá";
            dgvPhieuNhap.Columns[4].Header = "Ghi chú";
            dgvPhieuNhap.Columns[5].Header = "Tổng tiền";

        }
        private void LoadDSPhieuNhap()
        {
            PhieuNhapController phieuNhap = new PhieuNhapController();

            dgvDSPhieuNhap.ItemsSource = phieuNhap.GetList().DefaultView;
            dgvDSPhieuNhap.Columns[0].Visibility = Visibility.Collapsed;
            dgvDSPhieuNhap.SelectedIndex = 0;
        }

        private void SearchDSPhieuNhap(string search)
        {
            PhieuNhapController phieuNhap = new PhieuNhapController();

            dgvDSPhieuNhap.ItemsSource = phieuNhap.SearchList(search).DefaultView;
            dgvDSPhieuNhap.Columns[0].Visibility = Visibility.Collapsed;
            dgvDSPhieuNhap.SelectedIndex = 0;
        }

        #endregion

        
    }
}

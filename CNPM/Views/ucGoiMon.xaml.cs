using CNPM.Controller;
using CNPM.Models;
using CNPM.Views;
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

namespace CNPM
{
    /// <summary>
    /// Interaction logic for ucGoiMon.xaml
    /// </summary>
    public partial class ucGoiMon : UserControl
    {
        private bool flag = false;
        private int maNV;
        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
        private int ThanhTien = 0; //cột thành tiền trong dgv
        private QLMonAnLoaiMon qlmalm = new QLMonAnLoaiMon();
        QLGoiMon qlgm = new QLGoiMon();
        DataTable table = new DataTable();
        public ucGoiMon()
        {
            InitializeComponent();
            Loaded += UcGoiMon_Loaded;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            txtGiamGia.TextChanged += TxtGiamGia_TextChanged;
        }

        private void TxtGiamGia_TextChanged(object sender, TextChangedEventArgs e)
        {
            //txtThanhToan.Text = (Convert.ToInt32(txtThanhToan.Text) - (Convert.ToInt32(txtThanhToan.Text) * Convert.ToDecimal(txtGiamGia.Text)) / 100).ToString();
        }

        private void TxtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTimKiem.Text == "")
                LoadDSMonAn();
            else
                SearchDSMonAn();
        }

        private void UcGoiMon_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDSMonAn();

            table.Columns.Add("Tên món");
            table.Columns.Add("Số lượng");
            table.Columns.Add("Thành tiền");

            tbxTenMon.IsEnabled = false;
            txtThanhToan.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int row = table.Rows.Count;
            try
            {
                if (row != 0)
                {

                    DateTime now = DateTime.Now;
                    qlgm.ThemHoaDon(maNV, now, Convert.ToInt32(txtGiamGia.Text));
                    int ma = qlgm.LayMaHoaDon();
                    for (int i = 0; i < row; i++)
                    {
                        int maMon = qlgm.LayMaMon(table.Rows[i][0].ToString());
                        qlgm.ThemChiTietHD(maMon,
                                           ma,
                                           Convert.ToInt32(table.Rows[i][1]),
                                           Convert.ToInt32(table.Rows[i][2])
                            );
                    }
                    Window window = new Window
                    {
                        Title = "Chi Tiết Hóa Đơn",
                        Content = new ucXuatHoaDon(ma),
                        SizeToContent = SizeToContent.WidthAndHeight,
                        ResizeMode = ResizeMode.NoResize
                    };

                    window.ShowDialog();
                    dgvHoaDon.Items.Clear();
                    table.Rows.Clear();
                    //dgvHoaDon.ItemsSource = table.DefaultView;

                    txtThanhToan.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void SearchDSMonAn()
        {
            grBxThanhToan.IsEnabled = false;
            dgvDSMonAn.ItemsSource = qlmalm.TimKiemMonAn(txtTimKiem.Text).DefaultView;
            dgvDSMonAn.SelectedIndex = 0;
            dgvDSMonAn.Columns[0].Visibility = Visibility.Collapsed;
            dgvDSMonAn.Columns[4].Visibility = Visibility.Collapsed;
        }
        private void LoadDSMonAn()
        {
            grBxThanhToan.IsEnabled = false;
            dgvDSMonAn.ItemsSource = qlmalm.XemMonAn().DefaultView;
            dgvDSMonAn.SelectedIndex = 0;
            dgvDSMonAn.Columns[0].Visibility = Visibility.Collapsed;
            dgvDSMonAn.Columns[4].Visibility = Visibility.Collapsed;
        }

        private void btnCong_Click(object sender, RoutedEventArgs e)
        {
            int kq = Convert.ToInt32(tbxSoLuong.Text);
            kq++;
            tbxSoLuong.Text = kq.ToString();
            ThanhTien *= Convert.ToInt32(tbxSoLuong.Text);
        }

        private void btnTru_Click(object sender, RoutedEventArgs e)
        {
            int kq = Convert.ToInt32(tbxSoLuong.Text);
            if (kq > 1)
                kq--;
            tbxSoLuong.Text = kq.ToString();
            ThanhTien *= Convert.ToInt32(tbxSoLuong.Text);
        }

        private void dgvDSMonAn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dgvDSMonAn.SelectedItem as DataRowView;
            if (row != null)
            {

                //maMonAn = Convert.ToInt32(row.Row.ItemArray[0]);
                tbxTenMon.Text = row.Row.ItemArray[1].ToString();

                imgMonAn.Source = new BitmapImage(new Uri("pack://siteoforigin:,,," + row.Row.ItemArray[4].ToString()));

                ThanhTien = Convert.ToInt32(row.Row.ItemArray[3]);
            }
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (DataRow row in table.Rows)
            {
                if (row.ItemArray[0].ToString() == tbxTenMon.Text)
                {
                    MessageBox.Show("Món ăn đã có trong hóa đơn. Vui lòng xóa trước khi thêm lại món ăn này!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            grBxThanhToan.IsEnabled = true;
            MonAnIsSelected data = new MonAnIsSelected { tenMon = tbxTenMon.Text, soLuong = tbxSoLuong.Text, thanhTien = ThanhTien.ToString() };
            table.Rows.Add(tbxTenMon.Text, tbxSoLuong.Text, ThanhTien.ToString());
            
            dgvHoaDon.Items.Add(data);
            //dgvHoaDon.ItemsSource = table.DefaultView;

            if (table != null)
            {
                int rows = table.Rows.Count;
                int kq = 0;
                int giamgia = Convert.ToInt32(txtGiamGia.Text);
                for (int i = 0; i < rows; i++)
                    kq = kq + Convert.ToInt32(table.Rows[i]["Thành tiền"].ToString());
                //tbxTongTien.Text = kq.ToString();
                txtThanhToan.Text = (kq - kq * giamgia / 100).ToString();
            }
            tbxSoLuong.Text = "1";
        }
        public class MonAnIsSelected
        {
            public string tenMon { get; set; }
            public string soLuong { get; set; }
            public string thanhTien { get; set; }
        }

        private void btnXoaMonAn_Click(object sender, RoutedEventArgs e)
        {
            string tenMon = "";

            DataRowView row = dgvDSMonAn.SelectedItem as DataRowView;
            if (row != null)
            {
                tenMon = row.Row.ItemArray[1].ToString();
            }
            int index = table.Rows.Count;
            for (int i = 0; i < index; i++)
            {
                if (tenMon == table.Rows[i]["Tên món"].ToString())
                {
                    table.Rows.Remove(table.Rows[i]);
                    break;
                }
            }

            int rows = table.Rows.Count;
            int kq = 0;
            int giamgia = Convert.ToInt32(txtGiamGia.Text);
            for (int i = 0; i < rows; i++)
                kq = kq + Convert.ToInt32(table.Rows[i]["Thành tiền"].ToString());
            //tbxTongTien.Text = kq.ToString();
            txtThanhToan.Text = (kq - kq * giamgia / 100).ToString();
            dgvHoaDon.Items.Remove(dgvHoaDon.SelectedItem);
            //dgvHoaDon.ItemsSource = table.DefaultView;
        }

        private void btnXoaHoaDon_Click(object sender, RoutedEventArgs e)
        {
            table.Clear();
            //dgvHoaDon.ItemsSource = table.DefaultView;
            dgvHoaDon.Items.Clear();
            txtThanhToan.Clear();
        }

       
    }
}

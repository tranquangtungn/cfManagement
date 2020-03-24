using CNPM.Controller;
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
    /// Interaction logic for ucTinhLuong.xaml
    /// </summary>
    /// int TongCa = 0;
   
    public partial class ucTinhLuong : UserControl
    {
        public int TongLuong =0;
        public int MucLuong =0;
        public int TongCa = 0;
        int CachTimKiem = 2; // 1: theo tên
                            //2: theo mã NV
                               // 3: theo CMND
        
        
        QLTinhLuong qltl = new QLTinhLuong();
        public ucTinhLuong()
        {
            InitializeComponent();
            
    }

      
        private void btnXacNhanTGLamViec_Click(object sender, RoutedEventArgs e)
        {
            TongLuong = 0;
            TongCa = 0;
            if(cbxChonDoiTuong.Text == "Toàn bộ nhân viên")
            {
                
                
                dgvChamCong.ItemsSource = qltl.XemChamCongToanBoNV(dpNgayBatDau.SelectedDate,dpNgayKetThuc.SelectedDate).DefaultView;
                DataTable dt = new DataTable();
                dt = qltl.XemChamCongToanBoNV(dpNgayBatDau.SelectedDate, dpNgayKetThuc.SelectedDate);
                int index = dt.Rows.Count;
                for (int i = 0; i < index; i++)
                {
                    TongCa += Convert.ToInt32(dt.Rows[i][2]);
                }
                TongLuong = TongCa * MucLuong;
                txtTongLuong.Text = TongLuong.ToString();

            }
            else
            {
                gbxThongTinNV.IsEnabled = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            gbxThongTinNV.IsEnabled = false;
        }

        private void txtTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable dt = new DataTable();
            if (cbxLoaiTimKiem.Text == "Theo tên")
                CachTimKiem = 1;
            if (cbxLoaiTimKiem.Text == "Theo mã NV")
                CachTimKiem = 2;
            if (cbxLoaiTimKiem.Text == "Theo CMND")
                CachTimKiem = 3;
            switch (CachTimKiem)
            {
                case 1: dt = qltl.TimKiemNVTheoTen(txtTimKiem.Text); break;
                case 2: dt = qltl.TimKiemNVTheoMa(txtTimKiem.Text); break;
                case 3: dt = qltl.TimKiemNVTheoCMND(txtTimKiem.Text); break;
            }
            if(dt.Rows.Count > 0)
            {
                txtMaNV.Text = dt.Rows[0][0].ToString();
                txtHoTen.Text = dt.Rows[0][1].ToString();
                txtNgaySinh.Text = dt.Rows[0][2].ToString();
            }
            else
            {
                txtMaNV.Clear();
                txtHoTen.Clear();
                txtNgaySinh.Clear();
            }
        }

        private void btnXacNhanTTNV_Click(object sender, RoutedEventArgs e)
        {
            TongCa = 0;
            if (txtMaNV.Text != "")
            {
                dgvChamCong.ItemsSource = qltl.XemChamCong(Convert.ToInt32(txtMaNV.Text), dpNgayBatDau.SelectedDate, dpNgayKetThuc.SelectedDate).DefaultView;
                DataTable dt = new DataTable();
                dt = qltl.XemChamCong(Convert.ToInt32(txtMaNV.Text), dpNgayBatDau.SelectedDate, dpNgayKetThuc.SelectedDate);
                int index = dt.Rows.Count;
                for(int i =0;i<index;i++)
                {
                    TongCa += Convert.ToInt32(dt.Rows[i][2]);
                }
                TongLuong = TongCa * MucLuong;
                txtTongLuong.Text = TongLuong.ToString();
            }
        }

        private void txtMucLuong_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtMucLuong.Text != "")
            {
                MucLuong = Convert.ToInt32(txtMucLuong.Text);
                TongLuong = TongCa * MucLuong;

                
            }
            if(txtTongLuong !=null)
            {
                txtTongLuong.Text = TongLuong.ToString();
            }
        }

        private void btnIn_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window
            {
                Title = "Chi Tiết Hóa Đơn",
                Content = new ucXuatPhieuLuong(txtMaNV.Text,txtHoTen.Text,TongCa.ToString(),MucLuong.ToString(),TongLuong.ToString()),
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize
            };

            window.ShowDialog();
        }
    }
}

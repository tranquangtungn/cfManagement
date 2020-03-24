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
using CNPM.Controller;
using System.Drawing;
using Microsoft.Win32;
using System.IO;

namespace CNPM
{

    /// <summary>
    /// Interaction logic for ucQLMonAnLoaiMon.xaml
    /// </summary>
    public partial class ucQLMonAnLoaiMon : UserControl
    {
        private bool themMonAn;
        private bool themLoaiMon;
        private int maLoaiMon;
        private int maMonAn;
        private QLMonAnLoaiMon qlmalm = new QLMonAnLoaiMon();
        public ucQLMonAnLoaiMon()
        {
            InitializeComponent();
            txtTimKiemMonAn.TextChanged += TxtTimKiemMonAn_TextChanged;
            txtTimKiemLoaiMon.TextChanged += TxtTimKiemLoaiMon_TextChanged;
        }




        #region LoaiMon
        private void TxtTimKiemLoaiMon_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTimKiemLoaiMon.Text == "")
                LoadLoaiMon();
            else
                SearchLoaiMon();
        }
        public void SearchLoaiMon()
        {

            txtTenLoaiMon.Clear();
            grdLoaiMon.IsEnabled = false;
            btnHuyLoaiMon.IsEnabled = false;
            btnLuuLoaiMon.IsEnabled = false;

            btnSuaLoaiMon.IsEnabled = true;
            btnXoaLoaiMon.IsEnabled = true;
            btnThemLoaiMon.IsEnabled = true;


            DgvLoaiMon.ItemsSource = qlmalm.TimKiemLoaiMon(txtTimKiemLoaiMon.Text).DefaultView;
            DgvLoaiMon.SelectedIndex = 0;

        }
        public void LoadLoaiMon()
        {

            txtTenLoaiMon.Clear();
            grdLoaiMon.IsEnabled = false;
            btnHuyLoaiMon.IsEnabled = false;
            btnLuuLoaiMon.IsEnabled = false;

            btnSuaLoaiMon.IsEnabled = true;
            btnXoaLoaiMon.IsEnabled = true;
            btnThemLoaiMon.IsEnabled = true;


            DgvLoaiMon.ItemsSource = qlmalm.XemLoaiMon().DefaultView;
            DgvLoaiMon.SelectedIndex = 0;

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnReLoadLoaiMon_Click(object sender, RoutedEventArgs e)
        {
            LoadLoaiMon();
            //MessageBox.Show(DateTime./.Now.ToString());
        }

        private void DgvLoaiMon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = DgvLoaiMon.SelectedItem as DataRowView;
            if (row != null)
            {
                txtTenLoaiMon.Text = row.Row.ItemArray[1].ToString();
                maLoaiMon = Convert.ToInt32(row.Row.ItemArray[0]);
            }
        }

        private void DgvLoaiMon_MouseDown(object sender, MouseButtonEventArgs e)
        {





        }

        private void ucQLMonAnLoaiMon1_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoaiMon();
            LoadMonAn();
            //cbxLoaiMon.SelectedIndex = 0;
        }

        private void btnThemLoaiMon_Click(object sender, RoutedEventArgs e)
        {
            grdLoaiMon.IsEnabled = true;
            themLoaiMon = true;

            btnLuuLoaiMon.IsEnabled = true;
            btnHuyLoaiMon.IsEnabled = true;

            btnThemLoaiMon.IsEnabled = false;
            btnSuaLoaiMon.IsEnabled = false;
            btnXoaLoaiMon.IsEnabled = false;

            DgvLoaiMon.IsEnabled = false;

            txtTenLoaiMon.Clear();

        }

        private void btnSuaLoaiMon_Click(object sender, RoutedEventArgs e)
        {
            themLoaiMon = false;
            btnLuuLoaiMon.IsEnabled = true;
            btnHuyLoaiMon.IsEnabled = true;
            grdLoaiMon.IsEnabled = true;
            btnThemLoaiMon.IsEnabled = false;
            btnSuaLoaiMon.IsEnabled = false;
            btnXoaLoaiMon.IsEnabled = false;

            DgvLoaiMon.IsEnabled = false;
        }

        private void btnXoaLoaiMon_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MessageBoxResult flag =
                  MessageBox.Show("Chắc chắn xóa loại món đang chọn?", "THông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (flag == MessageBoxResult.Yes)
                {
                    qlmalm.XoaLoaiMon(maLoaiMon);
                    LoadLoaiMon();
                    MessageBox.Show("Đã xóa loại món vừa chọn!", "Notify!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Không xóa loại món vừa chọn!", "Notify!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Xóa thất bại. Vui lòng thử lại!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLuuLoaiMon_Click(object sender, RoutedEventArgs e)
        {
            if (txtTenLoaiMon.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên loại món!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTenLoaiMon.Focus();
            }
            else
            {
                try
                {
                    if (themLoaiMon)
                    {
                        QLMonAnLoaiMon qlmalm = new QLMonAnLoaiMon();
                        qlmalm.ThemLoaiMon(txtTenLoaiMon.Text);
                        MessageBox.Show("Đã thêm xong!", "Notify!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        QLMonAnLoaiMon qlmalm = new QLMonAnLoaiMon();
                        qlmalm.CapNhatLoaiMon(maLoaiMon, txtTenLoaiMon.Text);
                        MessageBox.Show("Đã sửa thông tin loại món vừa chọn!", "Notify!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    LoadLoaiMon();
                    DgvLoaiMon.IsEnabled = true;
                    cbxLoaiMon.ItemsSource = qlmalm.DSLoaiMon().DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnHuyLoaiMon_Click(object sender, RoutedEventArgs e)
        {

            btnLuuLoaiMon.IsEnabled = false;
            btnHuyLoaiMon.IsEnabled = false;
            grdLoaiMon.IsEnabled = false;
            btnThemLoaiMon.IsEnabled = true;
            btnSuaLoaiMon.IsEnabled = true;
            btnXoaLoaiMon.IsEnabled = true;
            DgvLoaiMon.IsEnabled = true;
            // dgvLoaiMon_CellClick(null, null);

        }

        #endregion
        #region MonAn

        private void TxtTimKiemMonAn_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTimKiemMonAn.Text != "")
                SearchMonAn();
            else
                LoadMonAn();
        }
        public void SearchMonAn()
        {
            grdMonAn.IsEnabled = false;
            btnHuyMonAn.IsEnabled = false;
            btnLuuMonAn.IsEnabled = false;

            btnSuaMonAn.IsEnabled = true;
            btnXoaMonAn.IsEnabled = true;
            btnThemMonAn.IsEnabled = true;

            cbxLoaiMon.ItemsSource = qlmalm.DSLoaiMon().DefaultView;
            cbxLoaiMon.DisplayMemberPath = "Tên loại món";
            dgvMonAn.ItemsSource = qlmalm.TimKiemMonAn(txtTimKiemMonAn.Text).DefaultView;
            dgvMonAn.SelectedIndex = 0;
            dgvMonAn.Columns[0].Visibility = Visibility.Collapsed;
            dgvMonAn.Columns[4].Visibility = Visibility.Collapsed;
        }
        public void LoadMonAn()
        {
            grdMonAn.IsEnabled = false;
            btnHuyMonAn.IsEnabled = false;
            btnLuuMonAn.IsEnabled = false;

            btnSuaMonAn.IsEnabled = true;
            btnXoaMonAn.IsEnabled = true;
            btnThemMonAn.IsEnabled = true;

            cbxLoaiMon.ItemsSource = qlmalm.DSLoaiMon().DefaultView;
            cbxLoaiMon.DisplayMemberPath = "Tên loại món";
            dgvMonAn.ItemsSource = qlmalm.XemMonAn().DefaultView;
            dgvMonAn.SelectedIndex = 0;
            dgvMonAn.Columns[0].Visibility = Visibility.Collapsed;
            dgvMonAn.Columns[4].Visibility = Visibility.Collapsed;

        }
        private void btnLuuMonAn_Click(object sender, RoutedEventArgs e)
        {
            if (txtTenMonAn.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên món!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTenMonAn.Focus();
            }
            else if (txtGiaTien.Text == "")
            {
                MessageBox.Show("Bạn phải nhập giá tiền!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTenMonAn.Focus();
            }
            else
            if (cbxLoaiMon.Text == "")
            {
                MessageBox.Show("Bạn phải chọn loại món!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTenMonAn.Focus();
            }
            else
            {
                try
                {
                    if (themMonAn)
                    {

                        string filePath = "./Images/HinhMonAn/HinhMonAn" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgMonAn.Source));
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            encoder.Save(stream);
                        QLMonAnLoaiMon qlmalm = new QLMonAnLoaiMon();
                        qlmalm.ThemMonAn(txtTenMonAn.Text, cbxLoaiMon.Text, Convert.ToInt32(txtGiaTien.Text), filePath);
                        MessageBox.Show("Đã thêm xong!", "Notify!", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    else
                    {

                        string filePath = "./Images/HinhMonAn/HinhMonAn" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imgMonAn.Source));
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            encoder.Save(stream);
                        QLMonAnLoaiMon qlmalm = new QLMonAnLoaiMon();
                        qlmalm.CapNhatMonAn(maMonAn, txtTenMonAn.Text, cbxLoaiMon.Text, Convert.ToInt32(txtGiaTien.Text), filePath);

                        //File.Delete(filePath);
                        MessageBox.Show("Đã sửa thông tin loại món vừa chọn!", "Notify!", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                    LoadMonAn();
                    dgvMonAn.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnThemMonAn_Click(object sender, RoutedEventArgs e)
        {
            grdMonAn.IsEnabled = true;
            themMonAn = true;

            btnLuuMonAn.IsEnabled = true;
            btnHuyMonAn.IsEnabled = true;

            btnThemMonAn.IsEnabled = false;
            btnSuaMonAn.IsEnabled = false;
            btnXoaMonAn.IsEnabled = false;

            dgvMonAn.IsEnabled = false;

            txtTenMonAn.Clear();
            txtGiaTien.Clear();

        }




        private void btnChonAnh_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgMonAn.Source = new BitmapImage(new Uri(op.FileName));
            }

        }
        private void btnReLoadMonAn_Click(object sender, RoutedEventArgs e)
        {
            LoadLoaiMon();

        }


        private void btnSuaMonAn_Click(object sender, RoutedEventArgs e)
        {
            themMonAn = false;
            btnLuuMonAn.IsEnabled = true;
            btnHuyMonAn.IsEnabled = true;
            grdMonAn.IsEnabled = true;
            dgvMonAn.IsEnabled = false;

            btnThemMonAn.IsEnabled = false;
            btnSuaMonAn.IsEnabled = false;
            btnXoaMonAn.IsEnabled = false;
        }

        private void btnXoaMonAn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MessageBoxResult flag =
                  MessageBox.Show("Chắc chắn xóa loại món đang chọn?", "THông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (flag == MessageBoxResult.Yes)
                {
                    qlmalm.XoaMonAn(maMonAn);
                    LoadLoaiMon();
                    MessageBox.Show("Đã xóa loại món vừa chọn!", "Notify!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Không xóa loại món vừa chọn!", "Notify!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Xóa thất bại. Vui lòng thử lại!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnHuyMonAn_Click(object sender, RoutedEventArgs e)
        {


            btnLuuMonAn.IsEnabled = false;
            btnHuyMonAn.IsEnabled = false;
            grdMonAn.IsEnabled = false;
            btnThemMonAn.IsEnabled = true;
            btnSuaMonAn.IsEnabled = true;
            btnXoaMonAn.IsEnabled = true;

            dgvMonAn.IsEnabled = true;


        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgvMonAn_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dgvMonAn.SelectedItem as DataRowView;
            if (row != null)
            {
                maMonAn = Convert.ToInt32(row.Row.ItemArray[0]);
                txtTenMonAn.Text = row.Row.ItemArray[1].ToString();
                cbxLoaiMon.Text = row.Row.ItemArray[2].ToString();
                txtGiaTien.Text = row.Row.ItemArray[3].ToString();
                //Uri resourceUri = new Uri(@"" + row.Row.ItemArray[4].ToString(), UriKind.Relative);
                imgMonAn.Source = new BitmapImage(new Uri("pack://siteoforigin:,,," + row.Row.ItemArray[4].ToString()));
            }
        }

        private void btnReloadMonAn_Click_1(object sender, RoutedEventArgs e)
        {
            LoadMonAn();
            dgvMonAn.SelectedIndex = 0;
        }
        #endregion
    }
}

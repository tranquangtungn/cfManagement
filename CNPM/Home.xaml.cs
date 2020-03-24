using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CNPM.Controllers;
using CNPM.Views;

namespace CNPM
{
    public partial class Home : Window
    {
        private int right = 0;
        private int id = 1;

        private int dp;
        public int Dp
        {
            get { return dp; }
            set { dp = value; }
        }


        bool StateClosed = true;
        TabItem _tabUserPage;
        private HomesController homesController = new HomesController();
        public int  Right 
        { 
            get { return right; }
            set { right = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public Home()
        {
            //Init Home
            InitializeComponent();
            Loaded += Home_Loaded;

            //Assign Click Events for Options Menu
            btnLogout.Click += BtnLogout_Click;
            btnExit.Click += BtnExit_Click;
            btnChangePassword.Click += BtnChangePassword_Click;

            //Assign Click Events for Main Menu
            lviRawManagement.PreviewMouseLeftButtonDown += lviRawManagement_PreviewMouseLeftButtonDown;
            lviRawImported.PreviewMouseLeftButtonDown += LviRawImported_PreviewMouseLeftButtonDown;
            lviRawUsed.PreviewMouseLeftButtonDown += LviRawUsed_PreviewMouseLeftButtonDown;
            lviAccountManagement.PreviewMouseLeftButtonDown += LviAccountManagement_PreviewMouseLeftButtonDown;
            lviEmpManagement.PreviewMouseLeftButtonDown += LviEmpManagement_PreviewMouseLeftButtonDown;
            lviTimeKeepManagement.PreviewMouseLeftButtonDown += LviTimeKeepManagement_PreviewMouseLeftButtonDown;
            lviRevenueReport.PreviewMouseLeftButtonDown += LviRevenueReport_PreviewMouseLeftButtonDown;
            lviDistributorManagement.PreviewMouseLeftButtonDown += LviDistributorManagement_PreviewMouseLeftButtonDown;
            lviFoodManagement.PreviewMouseLeftButtonDown += LviFoodManagement_PreviewMouseLeftButtonDown;
            lviOrder.PreviewMouseLeftButtonDown += LviOrder_PreviewMouseLeftButtonDown;
            lviSalaryCalculator.PreviewMouseLeftButtonDown += LviSalaryCalculator_PreviewMouseLeftButtonDown;
        }

        private void LviSalaryCalculator_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Tính lương";
            UControl.Items.Clear();
            var change = new ucTinhLuong();
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void LviOrder_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Gọi món";
            UControl.Items.Clear();
            var change = new ucGoiMon();
            change.MaNV = this.id;
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void LviFoodManagement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Quản lý loại món & món ăn";
            UControl.Items.Clear();
            var change = new ucQLMonAnLoaiMon();
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void LviDistributorManagement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Quản lý nhà cung cấp";
            UControl.Items.Clear();
            var change = new ucDistributorManagement();
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void LviRevenueReport_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Thống kê doanh thu";
            UControl.Items.Clear();
            var change = new RevenueManagement();
            change.right = this.right;
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Close();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LviTimeKeepManagement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Quản lý chấm công";
            UControl.Items.Clear();
            var change = new ucTimeKeepManagement();
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void LviEmpManagement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Quản lý nhân viên";
            UControl.Items.Clear();
            var change = new ucEmpManagement();
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void LviAccountManagement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Quản lý tài khoản";
            UControl.Items.Clear();
            var change = new ucAccountManagement();
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void LviRawUsed_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Sử dụng nguyên liệu";
            UControl.Items.Clear();
            var change = new ucRawUsed();
            change.manv = id;
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void LviRawImported_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Nhập nguyên liệu";
            UControl.Items.Clear();
            var change = new ucRawImported();
            change.manv = id;
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            UserChip.Content = homesController.GetName(id);

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(@Environment.CurrentDirectory + @homesController.GetImage(id));
            bitmapImage.EndInit();

            UserChipImg.Source = bitmapImage;


            //Set what user can use depend on Account's Right
            //Managers get all Right to use Application
            if (Right == 2) //Sellers' Right
            {
                lviTimeKeepManagement.Visibility = Visibility.Collapsed;
                lviFoodManagement.Visibility = Visibility.Collapsed;
                lviRawManagement.Visibility = Visibility.Collapsed;
                lviEmpManagement.Visibility = Visibility.Collapsed;
                lviAccountManagement.Visibility = Visibility.Collapsed;
                lviSalaryCalculator.Visibility = Visibility.Collapsed;
                lviRawImported.Visibility = Visibility.Collapsed;
                lviRawUsed.Visibility = Visibility.Collapsed;
                lviDistributorManagement.Visibility = Visibility.Collapsed;
            }
            else if (Right == 1) //Store Keepers' Right
            {
                lviTimeKeepManagement.Visibility = Visibility.Collapsed;
                lviFoodManagement.Visibility = Visibility.Collapsed;
                lviOrder.Visibility = Visibility.Collapsed;
                lviEmpManagement.Visibility = Visibility.Collapsed;
                lviAccountManagement.Visibility = Visibility.Collapsed;
                lviSalaryCalculator.Visibility = Visibility.Collapsed;
                lviRevenueReport.Visibility = Visibility.Collapsed;
                lviDistributorManagement.Visibility = Visibility.Collapsed;
            }
        }

        private void lviRawManagement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblTitle.Text = "Quản lý nguyên liệu";
            UControl.Items.Clear();
            var change = new ucRawManagement();
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            UControl.Items.Refresh();
        }

        

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            tblTitle.Text = "Đổi mật khẩu";
            UControl.Items.Clear();
            var change = new ucChangePassword();
            _tabUserPage = new TabItem { Content = change };
            UControl.Items.Add(_tabUserPage);
            change.User = id;
            UControl.Items.Refresh();
        }


        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            //Open Side Menu
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
            //Close Side Menu
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }
            //Change State
            StateClosed = !StateClosed;
        }
    }
}

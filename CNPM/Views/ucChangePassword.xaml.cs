using CNPM.Controllers;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ucChangePassword : System.Windows.Controls.UserControl
    {
        private int user;
        private TaiKhoansController taiKhoansController = new TaiKhoansController();
        public int User
        {
            get { return user; }
            set { user = value; }
        }
        public ucChangePassword()
        {
            InitializeComponent();
            Loaded += UcChangePassword_Loaded;
            btnCancel.Click += BtnCancel_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtPassWord.Text))
            {
                System.Windows.MessageBox.Show("Mật khẩu không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPassWord.Focus();
            }
            else if (string.IsNullOrEmpty(txtNewPassWord.Text))
            {
                System.Windows.MessageBox.Show("Mật khẩu mới không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNewPassWord.Focus();
            }
            else if (string.IsNullOrEmpty(txtPassWord.Text))
            {
                System.Windows.MessageBox.Show("Xác nhận mật khẩu mới không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtConfirmPassWord.Focus();
            }
            else if (txtPassWord.Text == txtNewPassWord.Text)
            {
                System.Windows.MessageBox.Show("Mật khẩu mới không được giống mật khẩu cũ!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtNewPassWord.Focus();
            }
            else if(txtNewPassWord.Text != txtConfirmPassWord.Text)
            {
                System.Windows.MessageBox.Show("Xác nhận mật khẩu mới bị sai!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtConfirmPassWord.Focus();
            }
            else
            {
                if(taiKhoansController.GetAcc(lblUserName.Content.ToString(), txtPassWord.Text))
                {
                    taiKhoansController.ChangePass(lblUserName.Content.ToString(), txtNewPassWord.Text);
                    System.Windows.MessageBox.Show("Mật khẩu của bạn đã được thay đổi. Vui lòng đăng nhập lại để tiếp tục phiên làm việc!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Window parent = Window.GetWindow(this);
                    parent.Hide();
                    Login login = new Login();
                    login.ShowDialog();
                    parent.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("Mật khẩu không đúng! Vui lòng nhập lại.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtPassWord.Focus();
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtConfirmPassWord.Text = null;
            txtNewPassWord.Text = null;
            txtPassWord.Text = null;
        }

        private void UcChangePassword_Loaded(object sender, RoutedEventArgs e)
        {
            lblUserName.Content = taiKhoansController.GetName(user);
        }
    }
}

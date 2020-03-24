using System.Windows;
using CNPM.Controllers;

namespace CNPM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        TaiKhoansController taiKhoansController = new TaiKhoansController();
        private int id, right; 
        public Login()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
            txtPass.TextChanged += TxtPass_TextChanged;
            checkBox_ShowPass.Checked += CheckBox_ShowPass_Checked;
            checkBox_ShowPass.Unchecked += CheckBox_ShowPass_Unchecked;
            lblForgotPassWord.MouseDown += LblForgotPassWord_MouseDown;
        }

        private void LblForgotPassWord_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ForgotPassWord forgotPassWord = new ForgotPassWord();
            forgotPassWord.ShowDialog();
        }

        private void TxtPass_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            passBox.Password = txtPass.Text;
        }

        private void CheckBox_ShowPass_Unchecked(object sender, RoutedEventArgs e)
        {
            passBox.Visibility = Visibility.Visible;
            txtPass.Visibility = Visibility.Collapsed;
        }

        private void CheckBox_ShowPass_Checked(object sender, RoutedEventArgs e)
        {
            txtPass.Text = passBox.Password;
            passBox.Visibility = Visibility.Collapsed;
            txtPass.Visibility = Visibility.Visible;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            

            if (string.IsNullOrEmpty(txtUser.Text))
            {
                MessageBox.Show("Tên đăng nhập không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(string.IsNullOrEmpty(passBox.Password))
            {
                MessageBox.Show("Mật khẩu không thể bỏ trống!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool flag = taiKhoansController.GetAcc(txtUser.Text, passBox.Password, ref id, ref right);
                if (flag)
                {
                    Home home = new Home();
                    home.Right = right;
                    home.Id = id;
                    this.Hide();
                    home.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtUser.Focus();
                }
            }
        }
    }
}

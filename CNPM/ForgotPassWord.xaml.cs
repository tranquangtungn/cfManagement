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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CNPM
{
    /// <summary>
    /// Interaction logic for ForgotPassWord.xaml
    /// </summary>
    public partial class ForgotPassWord : Window
    {
        TaiKhoansController taiKhoansController = new TaiKhoansController();
        public ForgotPassWord()
        {
            InitializeComponent();
            btnResetPassWord.Click += BtnResetPassWord_Click;
        }

        private void BtnResetPassWord_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Địa chỉ mail không được bỏ trống. Vui lòng nhập lại!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                txtEmail.Focus();
            }
            else
            {
                if (!taiKhoansController.CheckMail(txtEmail.Text))
                {
                    MessageBox.Show("Email bạn vừa nhập không tồn tại trong hệ thống. Vui lòng kiểm tra lại!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtEmail.Focus();
                }
                else
                {
                    taiKhoansController.FogotPassWords(txtEmail.Text);
                    MessageBox.Show("Kiểm tra thư điện tử của bạn để lấy mật khẩu mới!", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNPM.Views
{
    /// <summary>
    /// Interaction logic for ucUsedDetails.xaml
    /// </summary>
    public partial class ucUsedDetails : UserControl
    {
        public ucUsedDetails()
        {
            InitializeComponent();
        }
        public ucUsedDetails(int id)
        {
            InitializeComponent();
            PhieuXuatController phieuXuat = new PhieuXuatController();

            dgvPhieuNhapDetails.ItemsSource = phieuXuat.GetDetails(id).DefaultView;
            txtGhiChuPX.Text = phieuXuat.GetNote(id);

            btnClose.Click += BtnClose_Click;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.Parent as Window;

            if (parent != null)
            {
                parent.DialogResult = true;
                parent.Close();
            }
        }
    }
}

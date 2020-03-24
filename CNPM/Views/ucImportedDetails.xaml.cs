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
    
    public partial class ucImportedDetails : UserControl
    {
        public ucImportedDetails()
        {
            InitializeComponent();
        }
        public ucImportedDetails(int id)
        {
            InitializeComponent();
            PhieuNhapController phieuNhap = new PhieuNhapController();
            NhaCungCapController ncc = new NhaCungCapController();

            dgvPhieuNhapDetails.ItemsSource = phieuNhap.GetDetails(id).DefaultView;
            int nccid = phieuNhap.GetDistributorId(id);
            txtNhaCungCap.Text = ncc.IdToName(nccid);
            txtGhiChuPN.Text = phieuNhap.GetNote(id);
            txtTongTien.Text = phieuNhap.GetTotal(id);

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

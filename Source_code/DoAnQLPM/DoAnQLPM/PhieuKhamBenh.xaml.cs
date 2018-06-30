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
using System.Windows.Shapes;

namespace DoAnQLPM
{
    /// <summary>
    /// Interaction logic for PhieuKhamBenh.xaml
    /// </summary>
    public partial class PhieuKhamBenh : Window
    {
        public static int MaPhieuKhamIndex;
        public PhieuKhamBenh()
        {
            InitializeComponent();
        }
        
        private void DanhSachChiTietPK_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            MaPhieuKhamIndex = DanhSachChiTietPKB.SelectedIndex;
        }
    }
}

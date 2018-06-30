using System;
using DoAnQLPM.ViewModel;
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
using System.Diagnostics;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using DoAnQLPM.Model;
using System.Windows.Media.Animation;

namespace DoAnQLPM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnStr;
                conn.Open();
                SqlCommand command1 = new SqlCommand("[Select QuyDinh]", conn);
                command1.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader sdr = command1.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        tienKham.Text = sdr[0].ToString();
                        soBenhNhan.Text = sdr[1].ToString();
                    }
                }
            }
        }
        private void mainWindow_Activated(object sender, EventArgs e)
        {
            labelTenHienThi.Text = LoginViewModel.tenHienThi;
            if (LoginViewModel.typeAccount == 1)
                labelLoaiTK.Text = "Quản Lý";
            else
                labelLoaiTK.Text = "Nhân Viên";
            if (LoginViewModel.typeAccount != 1)
            {
                TaiKhoan.IsEnabled = false;
                QuyetDinh.IsEnabled = false;
                KhoThuoc.IsEnabled = false;
            }
            else
            {
                TaiKhoan.IsEnabled = true;
                QuyetDinh.IsEnabled = true;
                KhoThuoc.IsEnabled = true;
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var QuyDinh = DataProvider.Ins.DB.QuyetDinhs.Where(x => x.id == 1).SingleOrDefault();
            QuyDinh.SLBenhNhan = int.Parse(soBenhNhan.Text);
            QuyDinh.TienKham =int.Parse(tienKham.Text);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thay đổi quy định thành công", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void mainWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }
    }
}

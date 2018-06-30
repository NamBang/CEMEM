using DoAnQLPM.Model;
using DoAnQLPM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for BillWindow.xaml
    /// </summary>
    
    public partial class BillWindow : Window
    {
        public static int sum = 0;
        public BillWindow()
        {
            InitializeComponent();
        }

        private void PhuThu_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            int tienkham, tienthuoc,phuthu;
            int.TryParse(TienKham.Text, out tienkham);
            int.TryParse(TienThuoc.Text, out tienthuoc);
            int.TryParse(PhuThu.Text, out phuthu);
            sum = tienkham + tienthuoc + phuthu;
        }

        private void comboboxNgayKham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tienkham, tienthuoc, phuthu;
            int.TryParse(TienKham.Text, out tienkham);
            int.TryParse(TienThuoc.Text, out tienthuoc);
            int.TryParse(PhuThu.Text, out phuthu);
            sum = tienkham + tienthuoc + phuthu;
        }

        private void comboboxNgayKham_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Bill = new HoaDon() { MaPKB = BillViewModel.MaPK, TienKham = int.Parse(TienKham.Text), TienThuoc = int.Parse(TienThuoc.Text), TongTien = int.Parse(TongTien.Text) };
            DataProvider.Ins.DB.HoaDons.Add(Bill);
            DataProvider.Ins.DB.SaveChanges();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}

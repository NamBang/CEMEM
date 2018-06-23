using DoAnQLPM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DoAnQLPM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedViewConmand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand LapPhieuKhamBenhCommand { get; set; }

        public ObservableCollection<string> comboGioiTinh { get; set; }
        private ObservableCollection<BenhNhan> _ListBenhNhan;
        public ObservableCollection<BenhNhan> ListBenhNhan { get { return _ListBenhNhan; } set { _ListBenhNhan = value; OnPropertyChanged(); } }
        private BenhNhan _SelectedItemBenhNhan;
        public BenhNhan SelectedItemBenhNhan
        {
            get { return _SelectedItemBenhNhan; }
            set
            {
                _SelectedItemBenhNhan = value;
                OnPropertyChanged();
                if (SelectedItemBenhNhan != null)
                {

                    HoVaTen = SelectedItemBenhNhan.HoVaTen;
                    GioiTinh = SelectedItemBenhNhan.GioiTinh;
                    NamSinh = SelectedItemBenhNhan.NamSinh;
                    DiaChi = SelectedItemBenhNhan.DiaChi;

                }
            }
        }
        private string _HoVaTen;
        public string HoVaTen { get { return _HoVaTen; } set { _HoVaTen = value; OnPropertyChanged(); } }
        private string _GioiTinh;
        public string GioiTinh { get { return _GioiTinh; } set { _GioiTinh = value; OnPropertyChanged(); } }
        private int _NamSinh;
        public int NamSinh { get { return _NamSinh; } set { _NamSinh = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get { return _DiaChi; } set { _DiaChi = value; OnPropertyChanged(); } }




        public MainViewModel()
        {


            LoadedViewConmand = new RelayCommand<Window>((p) => { return true; }, (p) =>
             {
                 IsLoaded = true;
                 if (p == null)
                     return;
                 p.Hide();
                 LoginWindow loginWindow = new LoginWindow();
                 loginWindow.ShowDialog();

                 if (loginWindow.DataContext == null)
                     return;
                 var loginVM = loginWindow.DataContext as LoginViewModel;

                 if (loginVM.IsLogin)
                 {
                     p.Show();
                 }
                 else
                 {
                     p.Close();
                 }
             });
            comboGioiTinh = new ObservableCollection<String>();
            comboGioiTinh.Add("Nam");
            comboGioiTinh.Add("Nữ");

            ListBenhNhan = new ObservableCollection<BenhNhan>(DataProvider.Ins.DB.BenhNhans);
            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var Patient = new BenhNhan() { HoVaTen = HoVaTen, GioiTinh = GioiTinh, NamSinh = NamSinh, DiaChi = DiaChi };

                DataProvider.Ins.DB.BenhNhans.Add(Patient);
                DataProvider.Ins.DB.SaveChanges();

                ListBenhNhan.Add(Patient);
            });


            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemBenhNhan == null)
                    return false;
                var IdList = DataProvider.Ins.DB.BenhNhans.Where(x => x.MaBN == SelectedItemBenhNhan.MaBN);
                if (IdList != null || IdList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Patient = DataProvider.Ins.DB.BenhNhans.Where(x => x.MaBN == SelectedItemBenhNhan.MaBN).SingleOrDefault();
                Patient.HoVaTen = HoVaTen;
                Patient.GioiTinh = GioiTinh;
                Patient.NamSinh = NamSinh;
                Patient.DiaChi = DiaChi;
                DataProvider.Ins.DB.SaveChanges();
            });

            LapPhieuKhamBenhCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                MessageBox.Show("co chay vo day");
                PhieuKhamBenh PhieuKhamBenhwindown = new PhieuKhamBenh();
             //   PhieuKhamBenhwindown.HoVaTen.DataContext = SelectedItemBenhNhan.HoVaTen;
                PhieuKhamBenhwindown.Show();
            });


        }
    }
}

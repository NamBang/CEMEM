using DoAnQLPM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoAnQLPM.ViewModel
{
    class PatientViewModel : BaseViewModel
    {
        public ObservableCollection<string> comboGioiTinh { get; set; }
        private ObservableCollection<BenhNhan> _List;
        public ObservableCollection<BenhNhan> List { get { return _List; } set { _List = value; OnPropertyChanged(); } }
               private BenhNhan _SelectedItem;
        public BenhNhan SelectedItem { get { return _SelectedItem;}
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                  
                    HoVaTen = SelectedItem.HoVaTen;
                    GioiTinh = SelectedItem.GioiTinh;
                    NamSinh = SelectedItem.NamSinh;
                    DiaChi = SelectedItem.DiaChi;

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


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }



        public PatientViewModel()
        {
            comboGioiTinh = new ObservableCollection<String>();
            comboGioiTinh.Add("Nam");
            comboGioiTinh.Add("Nữ");
          
            List = new ObservableCollection<BenhNhan>(DataProvider.Ins.DB.BenhNhans);

            AddCommand = new RelayCommand<object>( (p) =>
            {
              


                return true;
                 

            }, (p) =>
            {
                var Patient = new BenhNhan() { HoVaTen = HoVaTen, GioiTinh=GioiTinh, NamSinh=NamSinh, DiaChi=DiaChi };

                DataProvider.Ins.DB.BenhNhans.Add(Patient);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Patient);
            });


            EditCommand = new RelayCommand<object>((p) => 
            {
                if(SelectedItem == null)
                return false;
                var IdList = DataProvider.Ins.DB.BenhNhans.Where(x => x.MaBN == SelectedItem.MaBN);
                if (IdList != null || IdList.Count()!=0 )
                    return true;
                return false;
            }, (p) =>
            {
                var Patient = DataProvider.Ins.DB.BenhNhans.Where(x => x.MaBN == SelectedItem.MaBN).SingleOrDefault();
                Patient.HoVaTen = HoVaTen;
                Patient.GioiTinh = GioiTinh;
                Patient.NamSinh = NamSinh;
                Patient.DiaChi = DiaChi;
             

        
                DataProvider.Ins.DB.SaveChanges();
              
                
            });


        }
    }
}

using DoAnQLPM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoAnQLPM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        //Login
        public ICommand LoadedViewConmand { get; set; }
        //Bệnh Nhân
        public ICommand AddCommandBenhNhan { get; set; }
        public ICommand EditCommandBenhNhan { get; set; }
        public ICommand DeleteCommandBenhNhan { get; set; }
        public ICommand IsOnCommandBenhNhan { get; set; }
        public ICommand closeCommandBenhNhan { get; set; }
        public ICommand TextChangedCommandBenhNhan { get; set; }
        public ICommand ResetCommandBenhNhan { get; set; }
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
        private string _TimHoVaTen;
        public string TimHoVaTen { get { return _TimHoVaTen; } set { _TimHoVaTen = value; OnPropertyChanged(); } }
        private string _MaBN;
        public string MaBN { get { return _MaBN; } set { _MaBN = value; OnPropertyChanged(); } }
        private string _HoVaTen;
        public string HoVaTen { get { return _HoVaTen; } set { _HoVaTen = value; OnPropertyChanged(); } }
        private string _GioiTinh;
        public string GioiTinh { get { return _GioiTinh; } set { _GioiTinh = value; OnPropertyChanged(); } }
        private int _NamSinh;
        public int NamSinh { get { return _NamSinh; } set { _NamSinh = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get { return _DiaChi; } set { _DiaChi = value; OnPropertyChanged(); } }
        private bool IsOn = false;
        //Báo Cáo
        private bool isOnClick = false;
        private int _Thang;
        public int Thang { get { return _Thang; } set { _Thang = value; OnPropertyChanged(); } }

        private DataView _allData;
        public DataView allData
        {
            get { return _allData; }
            set
            {
                if (value != _allData)
                {
                    _allData = value;
                    OnPropertyChanged("allData");
                }
            }
        }

        public ICommand SelectionChangedCommandBaoCao { get; set; }
        public ICommand ClickFindCommandBaoCao { get; set; }
        public ICommand IsOnClick { get; set; }
        public MainViewModel()
        {

            //Login
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

            //Benh Nhan
            comboGioiTinh = new ObservableCollection<String>();
            comboGioiTinh.Add("Nam");
            comboGioiTinh.Add("Nữ");

            ListBenhNhan = new ObservableCollection<BenhNhan>(DataProvider.Ins.DB.BenhNhans);
            AddCommandBenhNhan = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var Patient = new BenhNhan() { HoVaTen = HoVaTen, GioiTinh = GioiTinh, NamSinh = NamSinh, DiaChi = DiaChi };

                DataProvider.Ins.DB.BenhNhans.Add(Patient);
                DataProvider.Ins.DB.SaveChanges();

                ListBenhNhan = new ObservableCollection<BenhNhan>(DataProvider.Ins.DB.BenhNhans);
            });


            EditCommandBenhNhan = new RelayCommand<object>((p) =>
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


            closeCommandBenhNhan = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Text = "";
            });

            IsOnCommandBenhNhan = new RelayCommand<Button>((p) =>
            {
                return IsOn;
            }, (p) =>
            {

            });

            TextChangedCommandBenhNhan = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                _TimHoVaTen = p.Text;
                if (_TimHoVaTen == "")
                {
                    ListBenhNhan = new ObservableCollection<BenhNhan>(DataProvider.Ins.DB.BenhNhans);
                    IsOn = false;

                }
                else
                {
                    IsOn = true;
                    string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConnStr;
                        conn.Open();
                        SqlCommand command = new SqlCommand("[Search by Keyword]", conn);
                        command.Parameters.Add("@keyword", SqlDbType.VarChar).Value = _TimHoVaTen;
                        command.CommandType = CommandType.StoredProcedure;

                        ListBenhNhan.Clear();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                while (reader.Read())
                                {
                                    string hovaten, gioitinh, diachi;
                                    int namsinh;
                                    hovaten = reader[1].ToString();
                                    gioitinh = reader[2].ToString();
                                    int.TryParse(reader[3].ToString(), out namsinh);
                                    diachi = reader[4].ToString();
                                    BenhNhan a = new BenhNhan() { HoVaTen = hovaten, GioiTinh = gioitinh, NamSinh = namsinh, DiaChi = diachi };
                                    ListBenhNhan.Add(a);
                                }
                            }
                            else
                            {
                                ListBenhNhan = new ObservableCollection<BenhNhan>(DataProvider.Ins.DB.BenhNhans.Where((x) => x.HoVaTen.StartsWith(_TimHoVaTen)));
                            }

                        }

                    }
                }
            });

            ResetCommandBenhNhan = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                HoVaTen = null;
                GioiTinh = null;
                NamSinh = 0;
                DiaChi = null;
                SelectedItemBenhNhan = null;
            });

            DeleteCommandBenhNhan = new RelayCommand<object>((p) =>
            {
                if (SelectedItemBenhNhan == null)
                    return false;
                var IdList = DataProvider.Ins.DB.BenhNhans.Where(x => x.MaBN == SelectedItemBenhNhan.MaBN);
                if (IdList != null || IdList.Count() != 0)
                    return true;
                return false;
            }, p =>
            {
                string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Delete BenhNhan]", conn);
                    command.Parameters.Add("@key", SqlDbType.Int).Value = SelectedItemBenhNhan.MaBN;
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    ListBenhNhan = new ObservableCollection<BenhNhan>(DataProvider.Ins.DB.BenhNhans);
                }
            });

            LapPhieuKhamBenhCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                //MessageBox.Show("co chay vo day");
                PhieuKhamBenh PhieuKhamBenhwindown = new PhieuKhamBenh();
             //   PhieuKhamBenhwindown.HoVaTen.DataContext = SelectedItemBenhNhan.HoVaTen;
                PhieuKhamBenhwindown.Show();
            });

            //Báo Cáo
            ClickFindCommandBaoCao = new RelayCommand<ListView>((p) =>
            {
                return true;
            }, (p) =>
            {
                string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Sales by Month]", conn);
                    command.Parameters.Add("@month", SqlDbType.Int).Value = _Thang + 1;
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter ad = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    allData = dt.DefaultView;
                }
            }
           );
            IsOnClick = new RelayCommand<object>((p) =>
            {
                return isOnClick;
            }, (p) =>
            {

            }
           );
            SelectionChangedCommandBaoCao = new RelayCommand<ComboBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                isOnClick = true;
                _Thang = p.SelectedIndex;
            }
            );

        }
    }
}

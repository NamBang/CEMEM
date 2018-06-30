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
        public ICommand ThanhToanCommand { get; set; }
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
        private string _HoVaTen;
        public string HoVaTen { get { return _HoVaTen; } set { _HoVaTen = value; OnPropertyChanged(); } }
        private string _GioiTinh;
        public string GioiTinh { get { return _GioiTinh; } set { _GioiTinh = value; OnPropertyChanged(); } }
        private int _NamSinh;
        public int NamSinh { get { return _NamSinh; } set { _NamSinh = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get { return _DiaChi; } set { _DiaChi = value; OnPropertyChanged(); } }
        private bool IsOnBenhNhan = false;
        public bool ShowButtonPKB = false;
        private IEnumerable<BenhNhan> ConvertToBenhNhan(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new BenhNhan
                {
                    MaBN = Convert.ToInt32(row["MaBN"]),
                    HoVaTen = Convert.ToString(row["HoVaTen"]),
                    GioiTinh = Convert.ToString(row["GioiTinh"]),
                    NamSinh = Convert.ToInt32(row["NamSinh"]),
                    DiaChi = Convert.ToString(row["DiaChi"])
                };
            }
        }
        public static int MaBN;
        //Thuốc
        public ICommand AddCommandThuoc { get; set; }
        public ICommand EditCommandThuoc { get; set; }
        public ICommand DeleteCommandThuoc { get; set; }
        public ICommand IsOnCommandThuoc { get; set; }
        public ICommand closeCommandThuoc { get; set; }
        public ICommand TextChangedCommandThuoc { get; set; }
        public ICommand ResetCommandThuoc { get; set; }
        public ObservableCollection<string> comboDonVi { get; set; }
        private ObservableCollection<Thuoc> _ListThuoc;
        public ObservableCollection<Thuoc> ListThuoc { get { return _ListThuoc; } set { _ListThuoc = value; OnPropertyChanged(); } }
        private Thuoc _SelectedItemThuoc;
        public Thuoc SelectedItemThuoc
        {
            get { return _SelectedItemThuoc; }
            set
            {
                _SelectedItemThuoc = value;
                OnPropertyChanged();
                if (SelectedItemThuoc != null)
                {

                    TenThuoc = SelectedItemThuoc.TenThuoc;
                    DonVi = SelectedItemThuoc.DonVi;
                    DonGia = SelectedItemThuoc.DonGia;

                }
            }
        }
        private string _TimTenThuoc;
        public string TimTenThuoc { get { return _TimTenThuoc; } set { _TimTenThuoc = value; OnPropertyChanged(); } }
        private string _TenThuoc;
        public string TenThuoc { get { return _TenThuoc; } set { _TenThuoc = value; OnPropertyChanged(); } }
        private string _DonVi;
        public string DonVi { get { return _DonVi; } set { _DonVi = value; OnPropertyChanged(); } }
        private int _DonGia;
        public int DonGia { get { return _DonGia; } set { _DonGia = value; OnPropertyChanged(); } }
        private bool IsOnThuoc = false;
        private IEnumerable<Thuoc> ConvertToThuoc(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new Thuoc
                {
                    MaThuoc = Convert.ToInt32(row["MaThuoc"]),
                    TenThuoc = Convert.ToString(row["TenThuoc"]),
                    DonVi = Convert.ToString(row["DonVi"]),
                    DonGia = Convert.ToInt32(row["DonGia"]),
                };
            }
        }
        //Báo Cáo Doanh Thu
        private bool onClickDoanhThu = false;
        private int _ThangDoanhThu;
        public int ThangDoanhThu { get { return _ThangDoanhThu; } set { _ThangDoanhThu = value; OnPropertyChanged(); } }

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

        public ICommand SelectionChangedCommandBaoCaoDoanhThu { get; set; }
        public ICommand ClickFindCommandBaoCaoDoanhThu { get; set; }
        public ICommand IsOnClickDoanhThu { get; set; }
        //Báo cáo Thuốc
        private bool onClickSDThuoc = false;
        private int _ThangSDThuoc;
        public int ThangSDThuoc { get { return _ThangSDThuoc; } set { _ThangSDThuoc = value; OnPropertyChanged(); } }

        private DataView _allDataSDThuoc;
        public DataView allDataSDThuoc
        {
            get { return _allDataSDThuoc; }
            set
            {
                if (value != _allDataSDThuoc)
                {
                    _allDataSDThuoc = value;
                    OnPropertyChanged("allDataSDThuoc");
                }
            }
        }

        public ICommand SelectionChangedCommandBaoCaoSDThuoc { get; set; }
        public ICommand ClickFindCommandBaoCaoSDThuoc { get; set; }
        public ICommand IsOnClickSDThuoc { get; set; }
        //Tài Khoản
        public ICommand AddCommandTaiKhoan { get; set; }
        public ICommand EditCommandTaiKhoan { get; set; }
        public ICommand DeleteCommandTaiKhoan { get; set; }
        public ICommand IsOnCommandTaiKhoan { get; set; }
        public ICommand closeCommandTaiKhoan { get; set; }
        public ICommand TextChangedCommandTaiKhoan { get; set; }
        public ICommand ResetCommandTaiKhoan { get; set; }
        public ICommand IsAdmin { get; set; }
        private ObservableCollection<TaiKhoan> _ListTaiKhoan;
        public ObservableCollection<TaiKhoan> ListTaiKhoan { get { return _ListTaiKhoan; } set { _ListTaiKhoan = value; OnPropertyChanged(); } }
        private TaiKhoan _SelectedItemTaiKhoan;
        public TaiKhoan SelectedItemTaiKhoan
        {
            get { return _SelectedItemTaiKhoan; }
            set
            {
                _SelectedItemTaiKhoan = value;
                OnPropertyChanged();
                if (SelectedItemTaiKhoan != null)
                {
                    username = SelectedItemTaiKhoan.username;
                    tenhienthi = SelectedItemTaiKhoan.tenhienthi;
                    password = SelectedItemTaiKhoan.password;
                }
            }
        }
        private string _TimTenTaiKhoan;
        public string TimTenTaiKhoan { get { return _TimTenTaiKhoan; } set { _TimTenTaiKhoan = value; OnPropertyChanged(); } }
        private string _username;
        public string username { get { return _username; } set { _username = value; OnPropertyChanged(); } }
        private string _password;
        public string password { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        private string _tenhienthi;
        public string tenhienthi { get { return _tenhienthi; } set { _tenhienthi = value; OnPropertyChanged(); } }
        private bool IsOnTaiKhoan = false;
        private IEnumerable<TaiKhoan> ConvertToTaiKhoan(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new TaiKhoan
                {
                    username = Convert.ToString(row["username"]),
                    tenhienthi = Convert.ToString(row["tenhienthi"]),
                    password = Convert.ToString(row["password"])
                };
            }
        }
        //Thay đổi quyết định

        private string _BenhNhanToiDa;
        public string BenhNhanToiDa { get { return _BenhNhanToiDa; } set { _BenhNhanToiDa = value; OnPropertyChanged(); } }
        private string _TienKham;
        public string TienKham { get { return _TienKham; } set { _TienKham = value; OnPropertyChanged(); } }

        // Đổi Mật Khẩu
        public ICommand ThayDoiThongTin { get; set; }
        private string _MatKhauThayDoi ="";
        public string MatKhauThayDoi { get { return _MatKhauThayDoi; } set { _MatKhauThayDoi = value; OnPropertyChanged(); } }
        private string _TenThayDoi = "";
        public string TenThayDoi { get { return _TenThayDoi; } set { _TenThayDoi = value; OnPropertyChanged(); } }
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
                if (HoVaTen == null || GioiTinh == null || NamSinh < 0 || NamSinh == 0 || DiaChi == null)
                    return false;
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
                ListBenhNhan = new ObservableCollection<BenhNhan>(DataProvider.Ins.DB.BenhNhans);
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
                return IsOnBenhNhan;
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
                    IsOnBenhNhan = false;

                }
                else
                {
                    IsOnBenhNhan = true;
                    string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConnStr;
                        conn.Open();
                        SqlCommand command = new SqlCommand("[Search by Keyword]", conn);
                        command.Parameters.Add("@keyword", SqlDbType.VarChar).Value = _TimHoVaTen;
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adp = new SqlDataAdapter(command);

                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ListBenhNhan = new ObservableCollection<BenhNhan>(ConvertToBenhNhan(dt));
                        }
                        else
                        {
                            ListBenhNhan = new ObservableCollection<BenhNhan>(DataProvider.Ins.DB.BenhNhans.Where((x) => x.HoVaTen.StartsWith(_TimHoVaTen)));
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
                MaBN = SelectedItemBenhNhan.MaBN;
                PhieuKhamBenh PhieuKhamBenhwindown = new PhieuKhamBenh();
                PhieuKhamBenhwindown.ShowDialog();
            });
            ThanhToanCommand = new RelayCommand<object>((p) =>
            {
                return true;
    
            }, (p) =>
            {
                MaBN = SelectedItemBenhNhan.MaBN;
                BillWindow ThanhToanwindow = new BillWindow();
                ThanhToanwindow.ShowDialog();
            });
            //Thuốc
            comboDonVi = new ObservableCollection<String>();
            comboDonVi.Add("Viên");
            comboDonVi.Add("Chai");
            ListThuoc = new ObservableCollection<Thuoc>(DataProvider.Ins.DB.Thuocs);
            AddCommandThuoc = new RelayCommand<object>((p) =>
            {
                if (TenThuoc == null)
                    return false;
                if (DonVi == null)
                    return false;
                if (DonGia < 0)
                    return false;
                return true;

            }, (p) =>
            {
                var Drug = new Thuoc() { TenThuoc = TenThuoc, DonVi = DonVi, DonGia = DonGia };

                DataProvider.Ins.DB.Thuocs.Add(Drug);
                DataProvider.Ins.DB.SaveChanges();
                ListThuoc = new ObservableCollection<Thuoc>(DataProvider.Ins.DB.Thuocs);
            });


            EditCommandThuoc = new RelayCommand<object>((p) =>
            {
                if (SelectedItemThuoc == null)
                    return false;
                var IdList = DataProvider.Ins.DB.Thuocs.Where(x => x.MaThuoc == SelectedItemThuoc.MaThuoc);
                if (IdList != null || IdList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Drug = DataProvider.Ins.DB.Thuocs.Where(x => x.MaThuoc == SelectedItemThuoc.MaThuoc).SingleOrDefault();
                Drug.TenThuoc = TenThuoc;
                Drug.DonVi = DonVi;
                Drug.DonGia = DonGia;
                DataProvider.Ins.DB.SaveChanges();
                ListThuoc = new ObservableCollection<Thuoc>(DataProvider.Ins.DB.Thuocs);
            });


            closeCommandThuoc = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Text = "";
            });

            IsOnCommandThuoc = new RelayCommand<Button>((p) =>
            {
                return IsOnThuoc;
            }, (p) =>
            {

            });

            TextChangedCommandThuoc = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                _TimTenThuoc = p.Text;
                if (_TimTenThuoc == "")
                {
                    ListThuoc = new ObservableCollection<Thuoc>(DataProvider.Ins.DB.Thuocs);
                    IsOnThuoc = false;

                }
                else
                {
                    IsOnThuoc = true;
                    string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConnStr;
                        conn.Open();
                        SqlCommand command = new SqlCommand("[Search by Keyword Drug]", conn);
                        command.Parameters.Add("@keyword", SqlDbType.VarChar).Value = _TimTenThuoc;
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adp = new SqlDataAdapter(command);

                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ListThuoc = new ObservableCollection<Thuoc>(ConvertToThuoc(dt));
                        }
                        else
                        {
                            ListThuoc = new ObservableCollection<Thuoc>(DataProvider.Ins.DB.Thuocs.Where((x) => x.TenThuoc.StartsWith(_TimTenThuoc)));
                        }

                    }
                }
            });

            ResetCommandThuoc = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TenThuoc = null;
                DonVi = null;
                DonGia = 0;
                SelectedItemThuoc = null;
            });

            DeleteCommandThuoc = new RelayCommand<object>((p) =>
            {
                if (SelectedItemThuoc == null)
                    return false;
                var IdList = DataProvider.Ins.DB.Thuocs.Where(x => x.MaThuoc == SelectedItemThuoc.MaThuoc);
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
                    SqlCommand command = new SqlCommand("[Delete Thuoc]", conn);
                    command.Parameters.Add("@key", SqlDbType.Int).Value = SelectedItemThuoc.MaThuoc;
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    ListThuoc = new ObservableCollection<Thuoc>(DataProvider.Ins.DB.Thuocs);
                }
            });
            //Báo Cáo Doanh Thu
            ClickFindCommandBaoCaoDoanhThu = new RelayCommand<ListView>((p) =>
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
                    command.Parameters.Add("@month", SqlDbType.Int).Value = _ThangDoanhThu + 1;
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter ad = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    allData = dt.DefaultView;
                }
            });
            IsOnClickDoanhThu = new RelayCommand<object>((p) =>
            {
                return onClickDoanhThu;
            }, (p) =>
            {

            } );
            SelectionChangedCommandBaoCaoDoanhThu = new RelayCommand<ComboBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                onClickDoanhThu = true;
                _ThangDoanhThu = p.SelectedIndex;
            } );

            // Báo Cáo Thuốc
            
            ClickFindCommandBaoCaoSDThuoc = new RelayCommand<ListView>((p) =>
            {
                return true;
            }, (p) =>
            {
                string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Sales by Month Drug]", conn);
                    command.Parameters.Add("@moth", SqlDbType.Int).Value = _ThangSDThuoc + 1;
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter ad = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    allDataSDThuoc = dt.DefaultView;
                }
            });
            IsOnClickSDThuoc = new RelayCommand<object>((p) =>
            {
                return onClickSDThuoc;
            }, (p) =>
            {

            });
            SelectionChangedCommandBaoCaoSDThuoc = new RelayCommand<ComboBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                onClickSDThuoc = true;
                _ThangSDThuoc = p.SelectedIndex;
            });

            //Tài Khoản
            
            ListTaiKhoan = new ObservableCollection<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
            AddCommandTaiKhoan = new RelayCommand<object>((p) =>
            {
                if (username == null)
                    return false;
                if (password == null)
                    return false;
                return true;

            }, (p) =>
            {
                var account = new TaiKhoan() { username = username,tenhienthi = tenhienthi, password = password, TYPE = 2};

                DataProvider.Ins.DB.TaiKhoans.Add(account);
                DataProvider.Ins.DB.SaveChanges();
                ListTaiKhoan = new ObservableCollection<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
            });


            EditCommandTaiKhoan = new RelayCommand<object>((p) =>
            {
                if (SelectedItemTaiKhoan == null)
                    return false;
                var IdList = DataProvider.Ins.DB.TaiKhoans.Where(x => x.username == SelectedItemTaiKhoan.username);
                if (IdList != null || IdList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var account = DataProvider.Ins.DB.TaiKhoans.Where(x => x.username == SelectedItemTaiKhoan.username).SingleOrDefault();
                account.username = username;
                account.password = password;
                DataProvider.Ins.DB.SaveChanges();
                ListTaiKhoan = new ObservableCollection<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
            });


            closeCommandTaiKhoan = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Text = "";
            });

            IsOnCommandTaiKhoan = new RelayCommand<Button>((p) =>
            {
                return IsOnTaiKhoan;
            }, (p) =>
            {

            });

            TextChangedCommandTaiKhoan = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                _TimTenTaiKhoan = p.Text;
                if (_TimTenTaiKhoan == "")
                {
                    ListTaiKhoan = new ObservableCollection<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
                    IsOnTaiKhoan = false;

                }
                else
                {
                    IsOnTaiKhoan = true;
                    string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConnStr;
                        conn.Open();
                        SqlCommand command = new SqlCommand("[Search by Keyword Account]", conn);
                        command.Parameters.Add("@keyword", SqlDbType.VarChar).Value = _TimTenTaiKhoan;
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adp = new SqlDataAdapter(command);

                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ListTaiKhoan = new ObservableCollection<TaiKhoan>(ConvertToTaiKhoan(dt));
                        }
                        else
                        {
                            ListTaiKhoan = new ObservableCollection<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans.Where((x) => x.username.StartsWith(_TimTenTaiKhoan)));
                        }
                    }
                }
            });

            ResetCommandTaiKhoan = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                username = null;
                password = null;
                SelectedItemTaiKhoan = null;
            });

            DeleteCommandTaiKhoan = new RelayCommand<object>((p) =>
            {
                if (SelectedItemTaiKhoan == null)
                    return false;
                var IdList = DataProvider.Ins.DB.TaiKhoans.Where(x => x.username == SelectedItemTaiKhoan.username);
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
                    SqlCommand command = new SqlCommand("[Delete TaiKhoan]", conn);
                    command.Parameters.Add("@key", SqlDbType.VarChar).Value = SelectedItemTaiKhoan.username;
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    ListTaiKhoan = new ObservableCollection<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
                }
            });

            //Thay đổi mật khẩu
            ThayDoiThongTin = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
             {
                 var Account = DataProvider.Ins.DB.TaiKhoans.Where(x => x.username == LoginViewModel._usename).SingleOrDefault();
                 Account.password = MatKhauThayDoi.ToString();
                 Account.tenhienthi = TenThayDoi.ToString();
                 DataProvider.Ins.DB.SaveChanges();
                 MessageBox.Show("Mật khẩu thay đổi thành công!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);

             });

        }
    }
}

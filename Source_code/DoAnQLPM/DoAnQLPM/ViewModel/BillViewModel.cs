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
    class BillViewModel : BaseViewModel
    {
        private string _HoVaTenTamTinh;
        public string HoVaTenTamTinh { get { return _HoVaTenTamTinh; } set { _HoVaTenTamTinh = value; OnPropertyChanged(); } }
        private string _TienKhamTamTinh;
        public string TienKhamTamTinh { get { return _TienKhamTamTinh; } set { _TienKhamTamTinh = value; OnPropertyChanged(); } }
        private string _TienThuocTamTinh;
        public string TienThuocTamTinh { get { return _TienThuocTamTinh; } set { _TienThuocTamTinh = value; OnPropertyChanged(); } }
        private string _PhuThuTamTinh;
        public string PhuThuTamTinh { get { return _PhuThuTamTinh; } set { _PhuThuTamTinh = value; OnPropertyChanged(); } }
        private string _TongTienTamTinh;
        public string TongTienTamTinh { get { return _TongTienTamTinh; } set { _TongTienTamTinh = value; OnPropertyChanged(); } }
        private string _SelectNgayKham;
        public string SelectNgayKham { get { return _SelectNgayKham; } set { _SelectNgayKham = value; OnPropertyChanged(); } }
        private int _TongTienTienThuoc;
        public static int MaPK;
        public ObservableCollection<string> ComboBoxNgayKham { get; set; }
        public ICommand LoadedCommand { get; set; }
        public ICommand ClosedCommand { get; set; }
        public ICommand SelectionChangedCommandNgayKham { get; set; }
        public ICommand TextChangedCommandPhuThu { get; set; }
        public ICommand ThanhToanCommand { get; set; }
        private DataView _allThongKeThuoc;
        public DataView allThongKeThuoc
        {
            get { return _allThongKeThuoc; }
            set
            {
                if (value != _allThongKeThuoc)
                {
                    _allThongKeThuoc = value;
                    OnPropertyChanged("allThongKeThuoc");
                }
            }
        }
        private DataView _allThongHoaDon;
        public DataView allThongKeHoaDon
        {
            get { return _allThongHoaDon; }
            set
            {
                if (value != _allThongHoaDon)
                {
                    _allThongHoaDon = value;
                    OnPropertyChanged("allThongKeHoaDon");
                }
            }
        }


        public BillViewModel()
        {
            ComboBoxNgayKham = new ObservableCollection<String>();
            LoadedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Search by keyword Bill]", conn);
                    SqlCommand command1 = new SqlCommand("[Select QuyDinh]", conn);
                    command.Parameters.Add("@key", SqlDbType.Int).Value = MainViewModel.MaBN;
                    command.CommandType = CommandType.StoredProcedure;
                    command1.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            HoVaTenTamTinh = sdr[0].ToString();
                            ComboBoxNgayKham.Add(sdr[1].ToString());
                        }
                    }
                    using (SqlDataReader sdr = command1.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            TienKhamTamTinh = sdr[0].ToString();
                        }
                    }
                }
            });

            ClosedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
             {
                 HoVaTenTamTinh = null;
                 ComboBoxNgayKham.Clear();
             });

            SelectionChangedCommandNgayKham = new RelayCommand<ComboBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                if(p.SelectedItem != null)
                {
                    _SelectNgayKham = p.SelectedItem.ToString();
                    PhuThuTamTinh = "0";
                    TongTienTamTinh = BillWindow.sum.ToString();
                    string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConnStr;
                        conn.Open();
                        SqlCommand command = new SqlCommand("[Get Build]", conn);
                        command.Parameters.Add("@date", SqlDbType.Date).Value = _SelectNgayKham;
                        command.Parameters.Add("@maBN", SqlDbType.Int).Value = MainViewModel.MaBN;
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter ad = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        ad.Fill(dt);
                        allThongKeThuoc = dt.DefaultView;

                        _TongTienTienThuoc = 0;
                        using (SqlDataReader sdr = command.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                int sum;
                                int.TryParse(sdr[4].ToString(), out sum);
                                _TongTienTienThuoc += sum;
                                int.TryParse(sdr[5].ToString(),out MaPK);
                            }
                        }
                        TienThuocTamTinh = _TongTienTienThuoc.ToString();

                        command = new SqlCommand("[Sale by Bill]", conn);
                        command.Parameters.Add("@key", SqlDbType.Int).Value = MainViewModel.MaBN;
                        command.CommandType = CommandType.StoredProcedure;
                        ad = new SqlDataAdapter(command);
                        dt = new DataTable();
                        ad.Fill(dt);
                        allThongKeHoaDon = dt.DefaultView;
                    }
                }
            });
            TextChangedCommandPhuThu = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
             {
                 TongTienTamTinh = BillWindow.sum.ToString(); 

             });
            ThanhToanCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },(p) =>
            {
                string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Sale by Bill]", conn);
                    command.Parameters.Add("@key", SqlDbType.Int).Value = MainViewModel.MaBN;
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter ad = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    allThongKeHoaDon = dt.DefaultView;
                }
            });
        }
    }
}

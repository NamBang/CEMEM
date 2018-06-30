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
    public class PKBViewModel : BaseViewModel
    {
        private int _MaCTPKB;
        public int MaCTPKB { get { return _MaCTPKB; } set { _MaCTPKB = value; OnPropertyChanged(); } }
        private int _MaPKB;
        public int MaPKB { get { return _MaPKB; } set { _MaPKB = value; OnPropertyChanged(); } }
        private int _MaBN;
        public int MaBN { get { return _MaBN; } set { _MaBN = value; OnPropertyChanged(); } }
        private string _NgayKham;
        public string NgayKham { get { return _NgayKham; } set { _NgayKham = value; OnPropertyChanged(); } }
        private string _TrieuChung;
        public string TrieuChung { get { return _TrieuChung; } set { _TrieuChung = value; OnPropertyChanged(); } }
        private string _LoaiBenh;
        public string LoaiBenh { get { return _LoaiBenh; } set { _LoaiBenh = value; OnPropertyChanged(); } }
        private string _TenBenhNhan;
        public string TenBenhNhan { get { return _TenBenhNhan; } set { _TenBenhNhan = value; OnPropertyChanged(); } }
        private int _MaThuoc;
        public int MaThuoc { get { return _MaThuoc; } set { _MaThuoc = value; OnPropertyChanged(); } }
        private int _SoLuong;
        public int SoLuong { get { return _SoLuong; } set { _SoLuong = value; OnPropertyChanged(); } }
        private string _CachDung;
        public string CachDung { get { return _CachDung; } set { _CachDung = value; OnPropertyChanged(); } }
        public ObservableCollection<string> comboThuoc { get; set; }
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

        public ICommand AddThuocCommand { get; set; }
        public ICommand DeleteThuocCommand { get; set; }
        public ICommand AddPKBCommand { get; set; }
        public ICommand LoadedCommand { get; set; }
        public ICommand hehehehe { get; set; }

        public ICommand SelectionChangedCommandThuoc { get; set; }
        private bool isThemCTPKB;
        private bool isThemPKB;
        public PKBViewModel()
        {
            List<int> MaThuocs = new List<int>();
            List<int> MaPhieuKhams = new List<int>();
            comboThuoc = new ObservableCollection<String>();
            string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";
            LoadedCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                isThemCTPKB = false;
                isThemPKB = true;
                MaBN = MainViewModel.MaBN;
                MaThuocs.Clear();
                comboThuoc.Clear();
                MaPhieuKhams.Clear();
                SoLuong = 1;
                NgayKham = DateTime.Now.ToShortDateString();
                
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Sales by TenThuoc]", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader rdr = command.ExecuteReader())
                    {
                       
                        while (rdr.Read())
                        {
                            comboThuoc.Add(rdr[0].ToString());
                            MaThuocs.Add(int.Parse(rdr[1].ToString()));
                        }
                    }

                    command = new SqlCommand("[Sales TenBenhNhan]", conn);
                    command.Parameters.Add("@key", SqlDbType.Int).Value =MaBN;
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            TenBenhNhan = rdr[0].ToString();
                        }
                    }
                }
            });
           

           
            AddPKBCommand = new RelayCommand<object>((p) =>
            {

                if (LoaiBenh == null || TrieuChung == null)
                {
                    return false;
                }
                return isThemPKB;
            }, (p) =>
            {
                var PKB = new PhieuKB() { MaBN = MaBN, NgayKham = DateTime.Now, LoaiBenh = LoaiBenh, TrieuChung = TrieuChung };
                DataProvider.Ins.DB.PhieuKBs.Add(PKB);
                DataProvider.Ins.DB.SaveChanges();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Final add PhieuKham]", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            MaPKB = int.Parse(rdr[0].ToString());
                        }
                    }
                }
                isThemCTPKB = true;
                isThemPKB = false;
                MessageBox.Show("Thêm phiếu khám bệnh thành công!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            });

            SelectionChangedCommandThuoc = new RelayCommand<ComboBox>((p) =>
            {
                if (p.SelectedIndex < 0)
                    return false;
                return true;
            }, (p) =>
             {
                 MaThuoc = MaThuocs[p.SelectedIndex];
             });

            AddThuocCommand = new RelayCommand<object>((p) =>
            {
                if (isThemPKB == true)
                {
                    return false;
                }
                else if (comboThuoc == null || SoLuong < 1 || CachDung == null)
                {
                    return false;
                }
                else
                {
                    return isThemCTPKB;
                }
            }, (p) =>
            {
                var CTPhieuKM = new CHITIETPKB() { MaPKB = MaPKB,MaThuoc = MaThuoc, SLThuoc = SoLuong, CachDung = CachDung};
                DataProvider.Ins.DB.CHITIETPKBs.Add(CTPhieuKM);
                DataProvider.Ins.DB.SaveChanges();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Final add CTPhieuKham]", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            MaPhieuKhams.Add(int.Parse(rdr[0].ToString()));
                        }
                    }
                }

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Search Danh Sach CTPhieuKham by MaPKB]", conn);
                    command.Parameters.Add("@key", SqlDbType.Int).Value = MaPKB;
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter ad = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    allDataSDThuoc = dt.DefaultView;
                }

            });

            hehehehe = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
             {
                 MessageBox.Show("sadasdsa");
             }
            );

            DeleteThuocCommand = new RelayCommand<object>((p) =>
            {
                return isThemCTPKB;
            }, (p) =>
            {

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Delete ChiTietPhieuKham]", conn);
                    if(PhieuKhamBenh.MaPhieuKhamIndex >= 0)
                    {
                        try
                        {
                            cmd.Parameters.Add("@key", SqlDbType.Int).Value = MaPhieuKhams[PhieuKhamBenh.MaPhieuKhamIndex];
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                            MaPhieuKhams.Remove(MaPhieuKhams[PhieuKhamBenh.MaPhieuKhamIndex]);
                        }
                        catch
                        {
                            MessageBox.Show("Vui lòng chọn thuốc để xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }        

                    SqlCommand command = new SqlCommand("[Search Danh Sach CTPhieuKham by MaPKB]", conn);
                    command.Parameters.Add("@key", SqlDbType.Int).Value = MaPKB;
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter ad = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    allDataSDThuoc = dt.DefaultView;
                }

            });


        }
    }
}

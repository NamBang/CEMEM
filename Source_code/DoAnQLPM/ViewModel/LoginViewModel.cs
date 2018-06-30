using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using DoAnQLPM.Model;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace DoAnQLPM.ViewModel
{
    class LoginViewModel : BaseViewModel
 {
        public bool IsLogin { get; set; }
        public static string _usename;
        private string _TaiKhoan = "";
        public string TaiKhoan { get { return _TaiKhoan; } set { _TaiKhoan = value; OnPropertyChanged(); } } 
        private string _MatKhau = "";
        public string MatKhau { get { return _MatKhau; } set { _MatKhau = value; OnPropertyChanged(); } } 
        public static int typeAccount;
        public static string tenHienThi;
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public LoginViewModel()
        {
            IsLogin = false;

            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Login(p);
                    } );
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { MatKhau = p.Password; });

        }
        void Login(Window p)
        {
            if (p == null)
                return;
            var accCount = DataProvider.Ins.DB.TaiKhoans.Where(x => x.username == _TaiKhoan && x.password == _MatKhau).Count();
            if(accCount > 0)
            {
                string ConnStr = "Data Source=(local);Initial Catalog=QLPM4;Integrated Security=true;";

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConnStr;
                    conn.Open();
                    SqlCommand command = new SqlCommand("[Search LoaiTK]", conn);
                    command.Parameters.Add("@key", SqlDbType.VarChar).Value = _TaiKhoan.ToString();
                    command.CommandType = CommandType.StoredProcedure;
                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int.TryParse(rdr[0].ToString(),out typeAccount);
                            tenHienThi = rdr[1].ToString();
                        }
                    }
                }
                _usename = _TaiKhoan.ToString();
                IsLogin = true;
                p.Close();
            }
            else
            {
                IsLogin = false;
                if (_MatKhau == "" || _TaiKhoan == "")
                    MessageBox.Show("Vui lòng nhập tài khoản hoặc mật khẩu!","Thông Báo",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                else
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu. Vui lòng thử lại!","Thông Báo",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        public void PasswordChangedAction(object obj)
        {
            var b = obj as PasswordBox;
            _MatKhau = b.Password.ToString();
        }
     
    }
}

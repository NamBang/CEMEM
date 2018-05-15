using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using DoAnQLPM.Model;
using System.Windows;
namespace DoAnQLPM.ViewModel
{
    class LoginViewModel : BaseViewModel
 {
        public bool IsLoaded = false;
        private string _TaiKhoan;
        public string TaiKhoan { get { return _TaiKhoan; } set { _TaiKhoan = value; OnPropertyChanged(); } } 
        private string _MatKhau;
        public string MatKhau { get { return _MatKhau; } set { _MatKhau = value; OnPropertyChanged(); } } 

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public LoginViewModel()
        {

            LoginCommand = new RelayCommand<object>((p) => { return true; }, showPatientAction);
            PasswordChangedCommand = new RelayCommand<object>((p) => { return true; }, PasswordChangedAction);

            //MessageBox.Show(DataProvider.Ins.DB.TaiKhoans.First().username);

        }

        public void showPatientAction(object obj)
        {
            var b = obj as PasswordBox;
            _MatKhau = b.Password.ToString();
            
            var accCount = DataProvider.Ins.DB.TaiKhoans.Where(x => x.username == TaiKhoan  && x.password == _MatKhau).Count();
            if (accCount > 0)
            {
                

                MainWindow MainWindow = new MainWindow(); 
     
                MainWindow.ShowDialog();
                

                //p.Close();
            }
            else
            {
              //  IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }

         //   Application.Current.MainWindow = PatientWindow;
         //   PatientWindow.Show();
        }
        public void PasswordChangedAction(object obj)
        {
            var b = obj as PasswordBox;
            _MatKhau = b.Password.ToString();
           // MessageBox.Show(b.Password.ToString());
            //   Application.Current.MainWindow = PatientWindow;
            //   PatientWindow.Show();
        }
     
      
    }
}

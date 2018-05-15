using DoAnQLPM.Model;
using System;
using System.Collections.Generic;
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
        public ICommand PatientCommand { get; set; }
        public ICommand StoreCommand { get; set; }
        public ICommand ReportCommand { get; set; }
        public ICommand ConfigCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public MainViewModel()
        {

            PatientCommand = new RelayCommand<object>((p) => { return true; }, showPatientAction);
            StoreCommand = new RelayCommand<object>((p) => { return true; }, showStoreAction);
            ReportCommand = new RelayCommand<object>((p) => { return true; }, showReportAction);
            ConfigCommand = new RelayCommand<object>((p) => { return true; }, showConfigAction);
            LoginCommand = new RelayCommand<object>((p) => { return true; }, showLoginAction);

            //MessageBox.Show(DataProvider.Ins.DB.TaiKhoans.First().username);

        }

        public void showPatientAction(object obj)
        {
            PatientWindows PatientWindow = new PatientWindows(); PatientWindow.ShowDialog();
         //   Application.Current.MainWindow = PatientWindow;
         //   PatientWindow.Show();
        }
        public void showStoreAction(object obj)
        {
            StoreWindow StoreWindow = new StoreWindow(); StoreWindow.ShowDialog();
         //   Application.Current.MainWindow = StoreWindow;
         //   StoreWindow.Show();
        }
        public void showReportAction(object obj)
        {
            ReportWindow ReportWindow = new ReportWindow(); ReportWindow.ShowDialog();
          //  Application.Current.MainWindow = ReportWindow;
          //  ReportWindow.Show();
        }
        public void showConfigAction(object obj)
        {
            ConfigWindow ConfigWindow = new ConfigWindow(); ConfigWindow.ShowDialog();
          //  Application.Current.MainWindow = ConfigWindow;
          //  ConfigWindow.Show();
        }
        public void showLoginAction(object obj)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.ShowDialog();
            //  Application.Current.MainWindow = ConfigWindow;
            //  ConfigWindow.Show();
        }
      
    }
}

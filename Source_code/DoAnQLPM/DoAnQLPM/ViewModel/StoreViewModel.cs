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
    public class StoreViewModel : BaseViewModel
    {
        private ObservableCollection<Thuoc> _StoreList;
        public ObservableCollection<Thuoc> StoreList { get { return _StoreList; } set { _StoreList = value; OnPropertyChanged(); } }

}
}

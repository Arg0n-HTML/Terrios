using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Terrios.Core;

namespace Terrios.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ServicesViewCommand { get; set; }
        public RelayCommand EmployeViewCommand { get; set; }

        public ServicesViewModel ServicesVm { get; set; }
        public EmployeViewModel EmployeVm { get; set; }

        private object _currentView;
        public HomeViewModel HomeVm { get; set; }
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            ServicesVm = new ServicesViewModel();
            EmployeVm = new EmployeViewModel();
            CurrentView = HomeVm;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVm;
            });

            ServicesViewCommand = new RelayCommand(o =>
            {
                CurrentView = ServicesVm;
            });
            EmployeViewCommand = new RelayCommand(o =>
            {
                CurrentView = EmployeVm;
            });

        }

        private RelayCommand employeViewCommand;


        private void EmployeView(object commandParameter)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using StoreApp.Infrastructure.Commands;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.MVVM.Model;
using StoreApp.MVVM.View.Pages;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class MainWindowViewModel:BaseViewModel
    {
        public MainWindowViewModel()
        {
            //WindowLoadedCommand = new RelayCommand(OnAppWindowLoadedCommandExecute,
            //    CanAppWindowLoadedCommandExecute);

            StoreManagement = new Store();
            
            ToDepartamentsPageCommand = new RelayCommand(OnAppToDepartamentsPageCommandExecute, CanAppToDepartamentsPageCommandExecute);
            ToEmployeesPageCommand = new RelayCommand(OnAppToEmployeesPageCommandExecute, CanAppToEmployeesPageCommandExecute);
            ToHomePageCommand = new RelayCommand(OnAppToHomePageCommandExecute, CanAppToHomePageCommandExecute);
            ToOrdersPageCommand = new RelayCommand(OnAppToOrdersPageCommandExecute, CanAppToOrdersPageCommandExecute);
            ToProductsPageCommand = new RelayCommand(OnAppToProductsPageCommandExecute, CanAppToProductsPageCommandExecute);

            HomeViewModel = new HomePageViewModel() { MainWM = this };
            DepartamentsPageViewModel = new DepartamentsPageViewModel() { MainVM = this };
            OrdersPageViewModel = new OrdersPageViewModel() { MainVM = this };
            EmployeesPageViewModel = new EmployeesPageViewModel() { MainVM = this };
            ProductsPageViewModel = new ProductsPageViewModel() {MainVM = this};

            HomePage = new HomePage() { DataContext = HomeViewModel };
            DepartamentsPage = new DepartamentsPage() { DataContext = DepartamentsPageViewModel };
            EmployeesPage = new EmployeesPage() { DataContext = EmployeesPageViewModel };
            OrdersPage = new OrdersPage() { DataContext = OrdersPageViewModel };
            ProductsPage = new ProductsPage() {DataContext = ProductsPageViewModel};

            CurrentPage = HomePage;
        }


        #region ViewModels

        public HomePageViewModel HomeViewModel { get; set; }
        public DepartamentsPageViewModel DepartamentsPageViewModel { get; set; }
        public OrdersPageViewModel OrdersPageViewModel { get; set; }
        public EmployeesPageViewModel EmployeesPageViewModel { get; set; }
        public ProductsPageViewModel ProductsPageViewModel { get; set; }



        #endregion

        #region Commands

        //public RelayCommand WindowLoadedCommand { get; }

        public RelayCommand ToDepartamentsPageCommand { get; }
        public RelayCommand ToEmployeesPageCommand { get; }
        public RelayCommand ToHomePageCommand { get; }
        public RelayCommand ToOrdersPageCommand { get; }
        public RelayCommand ToProductsPageCommand { get;}

        #endregion


        #region Properties

        public Employee Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }
        public Store StoreManagement { get; set; }

        #endregion

        #region Fields

        private Page _currentPage;
        private Employee _employee;

        #endregion


        #region Pages

        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public DepartamentsPage DepartamentsPage { get; set; }
        public EmployeesPage EmployeesPage { get; set; }
        public HomePage HomePage { get; set; }
        public OrdersPage OrdersPage { get; set; }
        public ProductsPage ProductsPage { get; set; }

        #endregion

        private bool CanAppToHomePageCommandExecute(object arg) => true;
        private void OnAppToHomePageCommandExecute(object obj)
        {
            MovePage(HomePage);
        }

        private bool CanAppToDepartamentsPageCommandExecute(object arg) => true;
        private void OnAppToDepartamentsPageCommandExecute(object obj)
        {
            MovePage(DepartamentsPage);
            ProductsPage.IsEnabled = false;
        }
        private bool CanAppToOrdersPageCommandExecute(object arg) => true;
        private void OnAppToOrdersPageCommandExecute(object obj)
        {
            MovePage(OrdersPage);
        }

        private bool CanAppToEmployeesPageCommandExecute(object arg) => true;
        private void OnAppToEmployeesPageCommandExecute(object obj)
        {
            MovePage(EmployeesPage);
        }

        private bool CanAppToProductsPageCommandExecute(object arg) => true;
        private void OnAppToProductsPageCommandExecute(object obj)
        {
            MovePage(ProductsPage);
            ProductsPage.IsEnabled = true;
        }

        //private bool CanAppWindowLoadedCommandExecute(object arg) => true;
        //private void OnAppWindowLoadedCommandExecute(object obj)
        //{
        //    HomePage = new HomePage() { DataContext = HomeViewModel };
        //    DepartamentsPage = new DepartamentsPage();
        //    EmployeesPage = new EmployeesPage();
        //    OrdersPage = new OrdersPage();

        //    CurrentPage = HomePage;
        //}

        private void MovePage(Page ToPage)
        {
            CurrentPage = ToPage;
        }
    }
}

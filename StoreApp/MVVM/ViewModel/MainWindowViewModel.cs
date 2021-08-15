using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using StoreApp.Infrastructure.Commands;
using StoreApp.MVVM.Model;
using StoreApp.MVVM.View.Pages;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class MainWindowViewModel:BaseViewModel
    {
        public MainWindowViewModel() { }

        public MainWindowViewModel(Employee employee)
        {
            this.Employee = employee;

            ToDepartmentsPageCommand = new RelayCommand(OnAppToDepartmentsPageCommandExecute);
            ToUsersPageCommand = new RelayCommand(OnAppToUsersPageCommandExecute);
            ToHomePageCommand = new RelayCommand(OnAppToHomePageCommandExecute);
            ToOrdersPageCommand = new RelayCommand(OnAppToOrdersPageCommandExecute);
            ToProductsPageCommand = new RelayCommand(OnAppToProductsPageCommandExecute);
            ToAccessLevelsPageCommand = new RelayCommand(OnAppToAccessLevelsPageCommandExecute);

            HomePage = new HomePage();
            (HomePage.DataContext as HomePageViewModel).Employee = Employee;

            DepartmentsPage = new DepartmentsPage();
            UsersPage = new UsersPage();
            OrdersPage = new OrdersPage();
            ProductsPage = new ProductsPage();
            AccessLevelsPage = new AccessLevelsPage();

            MovePage(HomePage);
        }

        #region Events


        #endregion


        #region Commands

        public RelayCommand ToDepartmentsPageCommand { get; }
        public RelayCommand ToUsersPageCommand { get; }
        public RelayCommand ToHomePageCommand { get; }
        public RelayCommand ToOrdersPageCommand { get; }
        public RelayCommand ToProductsPageCommand { get;}
        public RelayCommand ToAccessLevelsPageCommand { get; }
        public RelayCommand MainWindowLoadedCommand { get; }

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

        public DepartmentsPage DepartmentsPage { get; set; }
        public UsersPage UsersPage { get; set; }
        public HomePage HomePage { get; set; }
        public OrdersPage OrdersPage { get; set; }
        public ProductsPage ProductsPage { get; set; }
        public AccessLevelsPage AccessLevelsPage { get; set; }

        #endregion

        private void OnAppToHomePageCommandExecute(object obj)
        {
            MovePageAsync(HomePage);
        }

        private void OnAppToDepartmentsPageCommandExecute(object obj)
        {
            MovePageAsync(DepartmentsPage);
        }
        private void OnAppToOrdersPageCommandExecute(object obj)
        {
            MovePageAsync(OrdersPage);
        }

        private void OnAppToUsersPageCommandExecute(object obj)
        {
            MovePageAsync(UsersPage);

        }

        private void OnAppToProductsPageCommandExecute(object obj)
        {
            MovePageAsync(ProductsPage);
        }


        private void OnAppToAccessLevelsPageCommandExecute(object obj)
        {
            MovePageAsync(AccessLevelsPage);
        }

        private void MovePage(Page ToPage)
        {
            CurrentPage = ToPage;
            GC.Collect();
        }

        private async void MovePageAsync(Page ToPage)
        {
            await Task.Run(() => MovePage(ToPage));
        }
    }
}

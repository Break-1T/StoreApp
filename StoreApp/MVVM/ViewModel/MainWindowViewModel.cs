using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
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
        public MainWindowViewModel( Employee employee)
        {
            //WindowLoadedCommand = new RelayCommand(OnAppWindowLoadedCommandExecute,
            //    CanAppWindowLoadedCommandExecute);

            StoreManagement = new Store();
            this.Employee = employee;
            
            ToDepartamentsPageCommand = new RelayCommand(OnAppToDepartamentsPageCommandExecute, CanAppToDepartamentsPageCommandExecute);
            ToUsersPageCommand = new RelayCommand(OnAppToUsersPageCommandExecute, CanAppToUsersPageCommandExecute);
            ToHomePageCommand = new RelayCommand(OnAppToHomePageCommandExecute, CanAppToHomePageCommandExecute);
            ToOrdersPageCommand = new RelayCommand(OnAppToOrdersPageCommandExecute, CanAppToOrdersPageCommandExecute);
            ToProductsPageCommand = new RelayCommand(OnAppToProductsPageCommandExecute, CanAppToProductsPageCommandExecute);

            HomeViewModel = new HomePageViewModel();
            DepartamentsPageViewModel = new DepartamentsPageViewModel();
            OrdersPageViewModel = new OrdersPageViewModel() ;
            UsersPageViewModel = new UsersPageViewModel();
            ProductsPageViewModel = new ProductsPageViewModel();

            HomePage = new HomePage() { DataContext = HomeViewModel };
            DepartamentsPage = new DepartamentsPage() { DataContext = DepartamentsPageViewModel };
            UsersPage = new UsersPage() { DataContext = UsersPageViewModel };
            OrdersPage = new OrdersPage() { DataContext = OrdersPageViewModel };
            ProductsPage = new ProductsPage() {DataContext = ProductsPageViewModel};

            //DisposeViewModelExcept(HomeViewModel);
            ActivatePage(HomePage);
            GC.Collect();
        }


        #region ViewModels

        public HomePageViewModel HomeViewModel { get; set; }
        public DepartamentsPageViewModel DepartamentsPageViewModel { get; set; }
        public OrdersPageViewModel OrdersPageViewModel { get; set; }
        public UsersPageViewModel UsersPageViewModel { get; set; }
        public ProductsPageViewModel ProductsPageViewModel { get; set; }



        #endregion

        #region Commands

        //public RelayCommand WindowLoadedCommand { get; }

        public RelayCommand ToDepartamentsPageCommand { get; }
        public RelayCommand ToUsersPageCommand { get; }
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
        public UsersPage UsersPage { get; set; }
        public HomePage HomePage { get; set; }
        public OrdersPage OrdersPage { get; set; }
        public ProductsPage ProductsPage { get; set; }

        #endregion

        private bool CanAppToHomePageCommandExecute(object arg) => true;
        private void OnAppToHomePageCommandExecute(object obj)
        {
            ActivatePage(HomePage);
            GC.Collect();
        }

        private bool CanAppToDepartamentsPageCommandExecute(object arg) => true;
        private void OnAppToDepartamentsPageCommandExecute(object obj)
        {
            ActivatePage(DepartamentsPage);
            GC.Collect();
        }
        private bool CanAppToOrdersPageCommandExecute(object arg) => true;
        private void OnAppToOrdersPageCommandExecute(object obj)
        {
            ActivatePage(OrdersPage);
            GC.Collect();

        }

        private bool CanAppToUsersPageCommandExecute(object arg) => true;
        private void OnAppToUsersPageCommandExecute(object obj)
        {
            ActivatePage(UsersPage);
            GC.Collect();
        }

        private bool CanAppToProductsPageCommandExecute(object arg) => true;
        private void OnAppToProductsPageCommandExecute(object obj)
        {
            ActivatePage(ProductsPage);
            GC.Collect();
        }

        //private bool CanAppWindowLoadedCommandExecute(object arg) => true;
        //private void OnAppWindowLoadedCommandExecute(object obj)
        //{
        //    HomePage = new HomePage() { DataContext = HomeViewModel };
        //    DepartamentsPage = new DepartamentsPage();
        //    UsersPage = new UsersPage();
        //    OrdersPage = new OrdersPage();

        //    CurrentPage = HomePage;
        //}

        private void MovePage(Page ToPage)
        {
            CurrentPage = ToPage;
        }
        private void DisposeViewModel<T>(T ViewModel)
        {
            try
            {
                if (ViewModel is null)
                    return;
                switch (ViewModel)
                {
                    case ProductsPageViewModel model:
                        model.Dispose();
                        break;
                    case DepartamentsPageViewModel model:
                        model.Dispose();
                        break;
                    case HomePageViewModel model:
                        model.Dispose();
                        break;
                    case OrdersPageViewModel model:
                        model.Dispose();
                        break;
                    case UsersPageViewModel model:
                        model.Dispose();
                        break;
                    default: return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void ActivatePage(Page Page)
        {
            try
            {
                BaseViewModel ViewModel = Page.DataContext as BaseViewModel;

                if (ViewModel != null && ViewModel.IsActiveViewModel)
                {
                    return;
                } 
                if(CurrentPage!=null)
                    DisposeViewModel(CurrentPage.DataContext);
                MovePage(Page);
                ViewModel?.FillViewModel();

                if (Page.DataContext is HomePageViewModel)
                {
                    ((HomePageViewModel)Page.DataContext).Employee =
                        ((HomePageViewModel)Page.DataContext).GetEmployeeCopy(this.Employee);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// Высвобождает ресурсы всех ViewModel кроме введённой модели
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ViewModel"> ViewModel которая не должна быть очищена</param>
        private void DisposeViewModelExcept<T>(T ViewModel)
        {
            try
            {
                if (ViewModel is null)
                    return;
                switch (ViewModel)
                {
                    case ProductsPageViewModel model:
                        DisposeViewModel(DepartamentsPageViewModel);
                        DisposeViewModel(HomeViewModel);
                        DisposeViewModel(OrdersPageViewModel);
                        DisposeViewModel(UsersPageViewModel);
                        break;
                    case DepartamentsPageViewModel model:
                        DisposeViewModel(ProductsPageViewModel);
                        DisposeViewModel(HomeViewModel);
                        DisposeViewModel(OrdersPageViewModel);
                        DisposeViewModel(UsersPageViewModel); 
                        break;
                    case HomePageViewModel model:
                        DisposeViewModel(ProductsPageViewModel);
                        DisposeViewModel(DepartamentsPageViewModel);
                        DisposeViewModel(OrdersPageViewModel);
                        DisposeViewModel(UsersPageViewModel); 
                        break;
                    case OrdersPageViewModel model:
                        DisposeViewModel(ProductsPageViewModel);
                        DisposeViewModel(DepartamentsPageViewModel);
                        DisposeViewModel(HomeViewModel);
                        DisposeViewModel(UsersPageViewModel); 
                        break;
                    case UsersPageViewModel model:
                        DisposeViewModel(ProductsPageViewModel);
                        DisposeViewModel(DepartamentsPageViewModel);
                        DisposeViewModel(HomeViewModel);
                        DisposeViewModel(OrdersPageViewModel); 
                        break;
                    default: 
                        return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

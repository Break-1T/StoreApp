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
        public MainWindowViewModel()
        {
            ToDepartmentsPageCommand = new RelayCommand(OnAppToDepartmentsPageCommandExecute);
            ToUsersPageCommand = new RelayCommand(OnAppToUsersPageCommandExecute);
            ToHomePageCommand = new RelayCommand(OnAppToHomePageCommandExecute);
            ToOrdersPageCommand = new RelayCommand(OnAppToOrdersPageCommandExecute);
            ToProductsPageCommand = new RelayCommand(OnAppToProductsPageCommandExecute);
            ToAccessLevelsPageCommand = new RelayCommand(OnAppToAccessLevelsPageCommandExecute);
            MainWindowLoadedCommand = new RelayCommand(OnAppMainWindowLoadedCommandExecute);

            

            //DepartmentsPage = new DepartmentsPage();
            //UsersPage = new UsersPage();
            //OrdersPage = new OrdersPage();
            //ProductsPage = new ProductsPage();
            //AccessLevelsPage = new AccessLevelsPage();
            
            //ActivatePage(HomePage);
            //GC.Collect();
        }

        #region Events

        private void OnAppMainWindowLoadedCommandExecute(object obj)
        {
            HomePage = new HomePage();
            ((HomePageViewModel)HomePage.DataContext).Employee = Employee;
            MovePage(HomePage);
        }

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
            if (HomePage==null)
            {
                HomePage = new HomePage();
                MovePageAsync(HomePage);
            }
            else
                MovePageAsync(HomePage);
            //GC.Collect();
        }

        private void OnAppToDepartmentsPageCommandExecute(object obj)
        {
            if (DepartmentsPage==null)
            {
                DepartmentsPage = new DepartmentsPage();
                MovePageAsync(DepartmentsPage);
            }
            else
            {
                MovePageAsync(DepartmentsPage);
            }
            //GC.Collect();
        }
        private void OnAppToOrdersPageCommandExecute(object obj)
        {
            if (OrdersPage==null)
            {
                OrdersPage = new OrdersPage();
                MovePageAsync(OrdersPage);
            }
            else
            {
                MovePageAsync(OrdersPage);
            }
            //GC.Collect();
        }

        private void OnAppToUsersPageCommandExecute(object obj)
        {
            if (UsersPage==null)
            {
                UsersPage = new UsersPage();
                MovePageAsync(UsersPage);
            }
            else
            {
                MovePageAsync(UsersPage);
            }
            //GC.Collect();
        }

        private void OnAppToProductsPageCommandExecute(object obj)
        {
            if (ProductsPage==null)
            {
                ProductsPage = new ProductsPage();
                MovePageAsync(ProductsPage);
            }
            else
            {
                MovePageAsync(ProductsPage);
            }
            //GC.Collect();
        }


        private void OnAppToAccessLevelsPageCommandExecute(object obj)
        {
            if (AccessLevelsPage==null)
            {
                AccessLevelsPage = new AccessLevelsPage();
                MovePageAsync(AccessLevelsPage);
            }
            else
            {
                MovePageAsync(AccessLevelsPage);
            }
            //GC.Collect();
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
                    case AccessLevelsViewModel model:
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
                //if(CurrentPage!=null)
                //    DisposeViewModel(CurrentPage.DataContext);
                if (CurrentPage != null)
                    (CurrentPage.DataContext as BaseViewModel)?.Dispose();

                MovePage(Page);
                ViewModel?.FillViewModel();

                if (Page.DataContext is HomePageViewModel model)
                {
                    model.Employee = model.GetEmployeeCopy(this.Employee);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

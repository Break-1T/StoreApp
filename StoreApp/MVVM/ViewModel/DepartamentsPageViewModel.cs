using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StoreApp.Infrastructure.Commands;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.MVVM.Model;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class DepartamentsPageViewModel:BaseViewModel
    {
        public DepartamentsPageViewModel()
        {

            #region Commands

            OdenAddNewDepartamentCommand = new RelayCommand(OnAppOdenAddNewDepartamentCommandExecute,
                CanAppOdenAddNewDepartamentCommandExecute);
            UploadNewDepartamentPhotoCommand = new RelayCommand(OnAppUploadNewDepartamentPhotoCommandExecute,
                CanAppUploadNewDepartamentPhotoCommandExecute);
            SaveNewDepartamentCommand = new RelayCommand(OnAppSaveNewDepartamentCommandExecute,
                CanAppSaveNewDepartamentCommandExecute);

            DeleteDepartamentCommand = new RelayCommand(OnAppDeleteDepartamentCommandExecute,
                CanAppDeleteDepartamentCommandExecute);
            AddProductCommand = new RelayCommand(OnAppAddProductCommandExecute,
                CanAppAddProductCommandExecute);
            UploadNewProductPhotoCommand = new RelayCommand(OnAppUploadNewProductPhotoCommandExecute,
                CanAppUploadNewProductPhotoCommandExecute);
            OpenAddProductGridCommand = new RelayCommand(OnAppOpenAddProductGridCommandExecute,
                CanAppOpenAddProductGridCommandExecute);
            ShowAllProductsCommand = new RelayCommand(OnAppShowAllProductsCommandExecute,
                CanAppShowAllProductsCommandExecute);
            DeleteProductCommand = new RelayCommand(OnAppDeleteProductCommandExecute,
                CanAppDeleteProductCommandExecute);
            OpenSearchGridCommand = new RelayCommand(OnAppOpenSearchGridCommandExecute,
                CanAppOpenSearchGridCommandExecute);
            SearchProductCommand = new RelayCommand(OnAppSearchProductCommandExecute,
                CanAppSearchProductCommandExecute);
            OpenChangeGridCommand = new RelayCommand(OnAppOpenChangeGridCommandExecute,
                CanAppOpenChangeGridCommandExecute);
            CloseChangeGridCommand = new RelayCommand(OnAppCloseChangeGridCommandExecute,
                CanAppCloseChangeGridCommandExecute);
            ChangeProductCommand = new RelayCommand(OnAppChangeProductCommandExecute,
                CanAppChangeProductCommandExecute);
            ChangeProductPhotoCommand = new RelayCommand(OnAppChangeProductPhotoCommandExecute,
                CanAppChangeProductPhotoCommandExecute);

            #endregion

            #region Properties

            ExpanderHeight = 0;
            OpenAddProductGridHeight = 0;
            OpenSearchProductGridHeight = 0;
            OpenChangeProductGridHeight = 0;

            SearchId = "";
            SearchName = "";
            SearchPrice = "";

            NewEmployee = new Employee();
            NewDepartament = new Department();
            SelectedDepartament = new Department();

            SelectedEmployees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();

            FillDepartaments();
            FillAllEmployees();
            SelectedEmployees = AllEmployees;

            #endregion

        }

        #region Events


        #endregion

        #region Commands

        public RelayCommand OdenAddNewDepartamentCommand { get; set; }
        public RelayCommand UploadNewDepartamentPhotoCommand { get; set; }
        public RelayCommand SaveNewDepartamentCommand { get; set; }
        public RelayCommand DeleteDepartamentCommand { get; set; }
        
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand UploadNewProductPhotoCommand { get; set; }
        public RelayCommand OpenAddProductGridCommand { get; set; }
        public RelayCommand ShowAllProductsCommand { get; set; }
        public RelayCommand DeleteProductCommand { get; set; }
        public RelayCommand OpenSearchGridCommand { get; set; }
        public RelayCommand SearchProductCommand { get; set; }
        public RelayCommand ChangeProductPhotoCommand { get; set; }
        public RelayCommand ChangeProductCommand { get; set; }
        public RelayCommand OpenChangeGridCommand { get; set; }
        public RelayCommand CloseChangeGridCommand { get; set; }

        #endregion

        #region Fields

        private double _expanderHeight;
        private Department _selectedDepartament;
        private ObservableCollection<Employee> _selectedEmployees;
        private double _openAddProductGridHeight;
        private double _openSearchProductGridHeight;
        private string _searchId;
        private string _searchPrice;
        private string _searchName;
        private double _openChangeProductGridHeight;
        
        private ObservableCollection<Department> _departments;
        private Department _newDepartament;
        private ObservableCollection<Employee> _allEmployees;
        private Employee _newEmployee;

        #endregion

        #region Properties

        public MainWindowViewModel MainVM { get; set; }
        public Department NewDepartament
        {
            get => _newDepartament;
            set
            {
                if (Equals(value, _newDepartament)) return;
                _newDepartament = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Department> Departments
        {
            get => _departments;
            set
            {
                if (Equals(value, _departments)) return;
                _departments = value;
                OnPropertyChanged();
            }
        }

        public Department SelectedDepartament
        {
            get => _selectedDepartament;
            set
            {
                if (Equals(value, _selectedDepartament)) return;
                _selectedDepartament = value;
                OnPropertyChanged();
                FillSelectedEmployees();
            }
        }
        public ObservableCollection<Employee> SelectedEmployees
        {
            get => _selectedEmployees;
            set
            {
                if (Equals(value, _selectedEmployees)) return;
                _selectedEmployees = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Employee> AllEmployees
        {
            get => _allEmployees;
            set
            {
                if (Equals(value, _allEmployees)) return;
                _allEmployees = value;
                OnPropertyChanged();
            }
        }
        public Employee NewEmployee
        {
            get => _newEmployee;
            set
            {
                if (Equals(value, _newEmployee)) return;
                _newEmployee = value;
                OnPropertyChanged();
            }
        }

        public double ExpanderHeight
        {
            get => _expanderHeight;
            set
            {
                if (value.Equals(_expanderHeight)) return;
                _expanderHeight = value;
                OnPropertyChanged();
            }
        }
        public double OpenAddProductGridHeight
        {
            get => _openAddProductGridHeight;
            set
            {
                if (value.Equals(_openAddProductGridHeight)) return;
                _openAddProductGridHeight = value;
                OnPropertyChanged();
            }
        }
        public double OpenSearchProductGridHeight
        {
            get => _openSearchProductGridHeight;
            set
            {
                if (value.Equals(_openSearchProductGridHeight)) return;
                _openSearchProductGridHeight = value;
                OnPropertyChanged();
            }
        }
        public double OpenChangeProductGridHeight
        {
            get => _openChangeProductGridHeight;
            set
            {
                if (value.Equals(_openChangeProductGridHeight)) return;
                _openChangeProductGridHeight = value;
                OnPropertyChanged();
            }
        }

        public string SearchId
        {
            get => _searchId;
            set
            {
                if (value == _searchId) return;
                _searchId = value;
                OnPropertyChanged();
            }
        }
        public string SearchName
        {
            get => _searchName;
            set
            {
                if (value == _searchName) return;
                _searchName = value;
                OnPropertyChanged();
            }
        }
        public string SearchPrice
        {
            get => _searchPrice;
            set
            {
                if (value == _searchPrice) return;
                _searchPrice = value;
                OnPropertyChanged();
            }
        }

        public Department SelectedDepartment
        {
            get => _selectedProduct1;
            set
            {
                if (Equals(value, _selectedProduct1)) return;
                _selectedProduct1 = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region CommandMethods

        private bool CanAppChangeProductPhotoCommandExecute(object arg) => true;
        private void OnAppChangeProductPhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
                SelectedDepartment.Image = File.ReadAllBytes(dialog.FileName);
        }

        private bool CanAppChangeProductCommandExecute(object arg) => true;
        private async void OnAppChangeProductCommandExecute(object obj)
        {
            if (await MainVM.StoreManagement.DataBaseControl.ChangeProductAsync(SelectedDepartment))
            {
                FillCategories(SelectedDepartament);
                FillAllProducts();
                FillSelectedProducts();
                if (OpenChangeProductGridHeight == 0d)
                    OpenChangeProductGridHeight = 40d;
            }
        }

        private bool CanAppCloseChangeGridCommandExecute(object arg) => true;
        private void OnAppCloseChangeGridCommandExecute(object obj)
        {
            OpenChangeProductGridHeight = OpenChangeProductGridHeight == 40 ? 0d : 40d;
        }

        private bool CanAppOpenChangeGridCommandExecute(object arg) => true;
        private void OnAppOpenChangeGridCommandExecute(object obj)
        {
            if (OpenChangeProductGridHeight == 0d)
                OpenChangeProductGridHeight = 40d;
        }

        private bool CanAppSearchProductCommandExecute(object arg) => true;
        private void OnAppSearchProductCommandExecute(object obj)
        {
            decimal Price;
            decimal Id;
            int count = 0;

            var list = new List<IEnumerable<Product>>();

            if (!string.IsNullOrEmpty(SearchPrice))
            {
                if (decimal.TryParse(SearchPrice.Replace(".", ","), out Price))
                {
                    count++;
                    var tmp = AllProducts.Where(x => x.Price == Price);
                    list.Add(tmp);
                }
                else
                {
                    count++;
                    list.Add(new List<Product>());
                }
            }

            if (!string.IsNullOrEmpty(SearchId))
            {
                if (decimal.TryParse(SearchId, out Id))
                {
                    count++;
                    var tmp = AllProducts.Where(x => x.Id == Id);
                    list.Add(tmp);
                }
                else
                {
                    count++;
                    list.Add(new List<Product>());
                }
            }

            if (!string.IsNullOrEmpty(SearchName))
            {
                count++;
                var tmp = AllProducts.Where(x => x.Name.ToLower() == SearchName.ToLower());
                list.Add(tmp);
            }

            IEnumerable<Product> Result = new List<Product>();

            switch (count)
            {
                case 3:
                    Result = from i in list[0]
                             from j in list[1]
                             from k in list[2]
                             where i == j && j == k
                             select i;
                    break;
                case 2:
                    Result = from i in list[0]
                             from j in list[1]
                             where i == j
                             select i;
                    break;
                case 1:
                    Result = from i in list[0]
                             select i;
                    break;
                case 0:
                    Result = new ObservableCollection<Product>();
                    break;
            }

            SelectedEmployees = new ObservableCollection<Product>(Result);

        }

        private bool CanAppOpenSearchGridCommandExecute(object arg) => true;
        private void OnAppOpenSearchGridCommandExecute(object obj)
        {
            OpenAddProductGridHeight = 0;
            OpenSearchProductGridHeight = OpenSearchProductGridHeight == 40 ? 0d : 40d;
        }

        private bool CanAppDeleteProductCommandExecute(object arg) => true;
        private async void OnAppDeleteProductCommandExecute(object obj)
        {
            await MainVM.StoreManagement.DataBaseControl.RemoveProductAsync(SelectedDepartment.Name, SelectedDepartment.Id);
            FillCategories(SelectedDepartament);
            FillAllProducts();
            FillSelectedProducts();
        }

        private bool CanAppOdenAddNewDepartamentCommandExecute(object arg) => true;
        private void OnAppOdenAddNewDepartamentCommandExecute(object obj)
        {
            ExpanderHeight = ExpanderHeight == 55d ? 0d : 55d;
        }

        private bool CanAppUploadNewDepartamentPhotoCommandExecute(object arg) => true;
        private void OnAppUploadNewDepartamentPhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName != null)
                NewDepartament.Image = File.ReadAllBytes(dialog.FileName);
        }

        private bool CanAppSaveNewDepartamentCommandExecute(object arg) => true;
        private async void OnAppSaveNewDepartamentCommandExecute(object obj)
        {
            if (NewDepartament.Image != null && NewDepartament.Image.Any())
            {
                await MainVM.StoreManagement.DataBaseControl.AddDepartamentAsync(NewDepartament.Name, NewDepartament.Image);
                FillDepartaments(SelectedDepartament);
                FillAllEmployees();
                FillSelectedEmployees();
            }
            else
            {
                await MainVM.StoreManagement.DataBaseControl.AddDepartamentAsync(NewDepartament.Name);
                FillDepartaments(SelectedDepartament);
                FillAllEmployees();
                FillSelectedEmployees();
            }
        }

        private bool CanAppDeleteDepartamentCommandExecute(object arg) => true;
        private async void OnAppDeleteDepartamentCommandExecute(object obj)
        {
            await MainVM.StoreManagement.DataBaseControl.RemoveDepartamentAsync(SelectedDepartament.Id, SelectedDepartament.Name);
            FillDepartaments();
            FillAllEmployees();
            FillSelectedEmployees();
            SelectedEmployees = AllEmployees;
        }

        private bool CanAppAddProductCommandExecute(object arg) => true;
        private async void OnAppAddProductCommandExecute(object obj)
        {
            if (NewProduct.Image.Any())
            {
                await MainVM.StoreManagement.DataBaseControl.AddProductAsync(NewProduct.Name, NewProduct.Price, NewProduct.Image, SelectedDepartament);
            }
            else
            {
                await MainVM.StoreManagement.DataBaseControl.AddProductAsync(NewProduct.Name, NewProduct.Price, SelectedDepartament);
            }
            FillCategories(SelectedDepartament);
            FillAllProducts();
            FillSelectedProducts();
        }

        private bool CanAppOpenAddProductGridCommandExecute(object arg) => true;
        private void OnAppOpenAddProductGridCommandExecute(object obj)
        {
            OpenSearchProductGridHeight = 0;
            OpenAddProductGridHeight = OpenAddProductGridHeight == 40 ? 0d : 40;
        }

        private bool CanAppUploadNewProductPhotoCommandExecute(object arg) => true;
        private void OnAppUploadNewProductPhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName != null)
                NewProduct.Image = File.ReadAllBytes(dialog.FileName);
        }

        private bool CanAppShowAllProductsCommandExecute(object arg) => true;
        private void OnAppShowAllProductsCommandExecute(object obj)
        {
            SelectedEmployees = AllProducts;
        }

        #endregion

        #region Methods

        private void FillDepartaments()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Departments = new ObservableCollection<Department>(db.Departments.Include(x => x.Employees).ToList());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void FillDepartaments(Department selectedDepartment)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Departments = new ObservableCollection<Department>(db.Departments.Include(x => x.Employees).ToList());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                SelectedDepartament = Departments.FirstOrDefault(x => x.Id == selectedDepartment.Id);
            }
        }
        private void FillSelectedEmployees()
        {
            try
            {
                SelectedEmployees = SelectedDepartament != null
                    ? new ObservableCollection<Employee>(AllEmployees.Where(x => x.Id == SelectedDepartament.Id).ToList())
                    : new ObservableCollection<Employee>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void FillSelectedEmployees(Department selectedDepartment)
        {
            try
            {
                SelectedEmployees = selectedDepartment != null
                    ? new ObservableCollection<Employee>(AllEmployees.Where(x => x.Department.Id == selectedDepartment.Id).ToList())
                    : new ObservableCollection<Employee>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void FillAllEmployees()
        {
            try
            {
                AllEmployees = new ObservableCollection<Employee>(Departments.Select(x => x.Employees).SelectMany(list => list).ToList());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #endregion
    }
}

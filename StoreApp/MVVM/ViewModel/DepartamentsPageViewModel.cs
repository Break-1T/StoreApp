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
using StoreApp.Infrastructure.StoreManagement;
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

            SaveNewEmployeeCommand = new RelayCommand(OnAppSaveNewEmployeeCommandExecute,
                CanAppSaveNewEmployeeCommandExecute);

            UploadNewEmployeePhotoCommand = new RelayCommand(OnAppUploadNewEmployeePhotoCommandExecute,
                CanAppUploadNewEmployeePhotoCommandExecute);
            
            OpenAddProductGridCommand = new RelayCommand(OnAppOpenAddProductGridCommandExecute,
                CanAppOpenAddProductGridCommandExecute);

            ShowAllEmployeesCommand = new RelayCommand(OnAppShowAllEmployeesCommandExecute,
                CanAppShowAllEmployeesCommandExecute);
            
            DeleteProductCommand = new RelayCommand(OnAppDeleteProductCommandExecute,
                CanAppDeleteProductCommandExecute);
            OpenSearchGridCommand = new RelayCommand(OnAppOpenSearchGridCommandExecute,
                CanAppOpenSearchGridCommandExecute);

            SearchEmployeeCommand = new RelayCommand(OnAppSearchEmployeeCommandExecute,
                CanAppSearchEmployeeCommandExecute);
            
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

            SearchEmployee = new SearchEmployee();
            NewEmployee = new Employee();
            NewDepartament = new Department();
            SelectedDepartament = new Department();

            SelectedEmployees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();

            FillDepartaments();
            FillAllEmployees();
            FillAccessLevels();
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
        
        public RelayCommand SaveNewEmployeeCommand { get; set; }
        public RelayCommand UploadNewEmployeePhotoCommand { get; set; }
        public RelayCommand OpenAddProductGridCommand { get; set; }
        
        public RelayCommand ShowAllEmployeesCommand { get; set; }
        
        public RelayCommand DeleteProductCommand { get; set; }
        public RelayCommand OpenSearchGridCommand { get; set; }
        
        public RelayCommand SearchEmployeeCommand { get; set; }
        
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
        private double _openChangeProductGridHeight;
        
        private ObservableCollection<Department> _departments;
        private Department _newDepartament;
        private ObservableCollection<Employee> _allEmployees;
        private Employee _newEmployee;
        private ObservableCollection<AccessLevel> _accessLevels;

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
        public SearchEmployee SearchEmployee { get; set; }

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

        public Department SelectedDepartment
        {
            get => _selectedDepartament;
            set
            {
                if (Equals(value, _selectedDepartament)) return;
                _selectedDepartament = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<AccessLevel> AccessLevels
        {
            get => _accessLevels;
            set
            {
                if (Equals(value, _accessLevels)) return;
                _accessLevels = value;
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
            //if (await MainVM.StoreManagement.DataBaseControl.ChangeProductAsync(SelectedDepartment))
            //{
            //    //FillCategories(SelectedDepartament);
            //    //FillAllProducts();
            //    //FillSelectedProducts();
            //    if (OpenChangeProductGridHeight == 0d)
            //        OpenChangeProductGridHeight = 40d;
            //}
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

        private bool CanAppSearchEmployeeCommandExecute(object arg) => true;
        private void OnAppSearchEmployeeCommandExecute(object obj)
        {
            int count = 0;

            var list = new List<IEnumerable<Employee>>();
            int id;
            if (!string.IsNullOrEmpty(SearchEmployee.Id))
            {
                if (int.TryParse(SearchEmployee.Id.Replace(".", ","), out id))
                {
                    count++;
                    var tmp = AllEmployees.Where(x => x.Id == id);
                    list.Add(tmp);
                }
                else
                {
                    count++;
                    list.Add(new List<Employee>());
                }
            }
            if (!string.IsNullOrEmpty(SearchEmployee.Email))
            {
                count++;
                var tmp = AllEmployees.Where(x => x.Email.ToLower() == SearchEmployee.Email.ToLower());
                list.Add(tmp);
             
            }
            if (!string.IsNullOrEmpty(SearchEmployee.Password))
            {
                count++;
                var tmp = AllEmployees.Where(x => x.Password.ToLower() == SearchEmployee.Password.ToLower());
                list.Add(tmp);
            }
            if (!string.IsNullOrEmpty(SearchEmployee.PhoneNumber))
            {
                count++;
                var tmp = AllEmployees.Where(x => x.PhoneNumber.ToLower() == SearchEmployee.PhoneNumber.ToLower());
                list.Add(tmp);
            }
            if (!string.IsNullOrEmpty(SearchEmployee.Surname))
            {
                count++;
                var tmp = AllEmployees.Where(x => x.Surname.ToLower() == SearchEmployee.Surname.ToLower());
                list.Add(tmp);
            }

            if (!string.IsNullOrEmpty(SearchEmployee.AccessLevel.Name)) 
            {
                count++;
                var tmp = AllEmployees.Where(x => x.AccessLevel.Id == SearchEmployee.AccessLevel.Id);
                list.Add(tmp);
            }

            IEnumerable<Employee> Result = new List<Employee>();
            //6
            switch (count)
            {
                case 6:
                    Result = from i in list[0]
                        from j in list[1]
                        from k in list[2]
                        from f in list[3]
                        from x in list[4]
                        from c in list[5]
                        where i == j && j == k && k==f && f==x && x==c
                        select i;
                    break;
                case 5:
                    Result = from i in list[0]
                        from j in list[1]
                        from k in list[2]
                        from f in list[3]
                        from x in list[4]
                        where i == j && j == k && k == f && f == x
                        select i;
                    break;
                case 4:
                    Result = from i in list[0]
                        from j in list[1]
                        from k in list[2]
                        from f in list[3]
                        where i == j && j == k && k == f
                        select i;
                    break;
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
                    Result = new ObservableCollection<Employee>();
                    break;
            }

            SelectedEmployees = new ObservableCollection<Employee>(Result);
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
            //FillCategories(SelectedDepartament);
            //FillAllProducts();
            //FillSelectedProducts();
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

        private bool CanAppSaveNewEmployeeCommandExecute(object arg) => true;
        private async void OnAppSaveNewEmployeeCommandExecute(object obj)
        { 
            await MainVM.StoreManagement.DataBaseControl.AddEmployeeAsync(NewEmployee.Login, NewEmployee.Password,
                    NewEmployee.AccessLevel, NewEmployee.Name, NewEmployee.Surname, NewEmployee.Email,
                    NewEmployee.Department, NewEmployee.PhoneNumber); 
            FillDepartaments(SelectedDepartament);
            FillAllEmployees();
            FillSelectedEmployees();
        }

        private bool CanAppOpenAddProductGridCommandExecute(object arg) => true;
        private void OnAppOpenAddProductGridCommandExecute(object obj)
        {
            OpenSearchProductGridHeight = 0;
            OpenAddProductGridHeight = OpenAddProductGridHeight == 40 ? 0d : 40;
        }

        private bool CanAppUploadNewEmployeePhotoCommandExecute(object arg) => true;
        private void OnAppUploadNewEmployeePhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName != null)
                NewEmployee.Image = File.ReadAllBytes(dialog.FileName);
        }

        private bool CanAppShowAllEmployeesCommandExecute(object arg) => true;
        private void OnAppShowAllEmployeesCommandExecute(object obj)
        {
            SelectedEmployees = AllEmployees;
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
        private void FillAccessLevels()
        {
            AccessLevels = new ObservableCollection<AccessLevel>(AllEmployees.Select(x => x.AccessLevel).Distinct().ToList());
        }

        #endregion
    }
}

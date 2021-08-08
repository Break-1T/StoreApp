using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;
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

            OpenAddNewDepartamentCommand = new RelayCommand(OnAppOpenAddNewDepartamentCommandExecute,
                CanAppOpenAddNewDepartamentCommandExecute);
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

            OpenAddEmployeeGridCommand = new RelayCommand(OnAppOpenAddEmployeeGridCommandExecute,
                CanAppOpenAddEmployeeGridCommandExecute);

            ShowAllEmployeesCommand = new RelayCommand(OnAppShowAllEmployeesCommandExecute,
                CanAppShowAllEmployeesCommandExecute);

            DeleteEmployeeCommand = new RelayCommand(OnAppDeleteEmployeeCommandExecute,
                CanAppDeleteEmployeeCommandExecute);
            OpenSearchGridCommand = new RelayCommand(OnAppOpenSearchGridCommandExecute,
                CanAppOpenSearchGridCommandExecute);

            SearchEmployeeCommand = new RelayCommand(OnAppSearchEmployeeCommandExecute,
                CanAppSearchEmployeeCommandExecute);

            OpenChangeEmployeeGridCommand = new RelayCommand(OnAppOpenChangeEmployeeGridCommandExecute,
                CanAppOpenChangeEmployeeGridCommandExecute);
            CloseChangeGridCommand = new RelayCommand(OnAppCloseChangeGridCommandExecute,
                CanAppCloseChangeGridCommandExecute);
            ChangeEmployeeCommand = new RelayCommand(OnAppChangeEmployeeCommandExecute,
                CanAppChangeEmployeeCommandExecute);
            ChangeEmployeePhotoCommand = new RelayCommand(OnAppChangeEmployeePhotoCommandExecute,
                CanAppChangeEmployeePhotoCommandExecute);

            #endregion
            
            //FillViewModel();
        }

        #region Commands

        public RelayCommand OpenAddNewDepartamentCommand { get; private set; }
        public RelayCommand UploadNewDepartamentPhotoCommand { get; private set; }
        public RelayCommand SaveNewDepartamentCommand { get; private set; }
        public RelayCommand DeleteDepartamentCommand { get; private set; }
        
        public RelayCommand SaveNewEmployeeCommand { get; private set; }
        public RelayCommand UploadNewEmployeePhotoCommand { get; private set; }
        
        public RelayCommand OpenAddEmployeeGridCommand { get; private set; }
        
        public RelayCommand ShowAllEmployeesCommand { get; private set; }
        
        public RelayCommand DeleteEmployeeCommand { get; private set; }
        public RelayCommand OpenSearchGridCommand { get; private set; }
        
        public RelayCommand SearchEmployeeCommand { get; private set; }
        
        public RelayCommand ChangeEmployeePhotoCommand { get; private set; }
        public RelayCommand ChangeEmployeeCommand { get; private set; }
        public RelayCommand OpenChangeEmployeeGridCommand { get; private set; }
        public RelayCommand CloseChangeGridCommand { get; private set; }

        #endregion

        #region Fields

        private Employee _selectedEmployee;
        private Employee _newEmployee;
        private Department _newDepartament;
        private Department _selectedDepartament;

        private ObservableCollection<Employee> _selectedEmployees;
        private ObservableCollection<Department> _departments;
        private ObservableCollection<Employee> _allEmployees;
        private ObservableCollection<AccessLevel> _accessLevels;
        
        private double _openAddEmployeeGridHeight;
        private double _openSearchEmployeeGridHeight;
        private double _openChangeEmployeeGridHeight;
        private double _expanderHeight;
        private SearchEmployee _searchEmployee;

        #endregion

        #region Properties

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
        public Store StoreManagement { get; set; }
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

        public SearchEmployee SearchEmployee
        {
            get => _searchEmployee;
            set
            {
                if (Equals(value, _searchEmployee)) return;
                _searchEmployee = value;
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
        public double OpenAddEmployeeGridHeight
        {
            get => _openAddEmployeeGridHeight;
            set
            {
                if (value.Equals(_openAddEmployeeGridHeight)) return;
                _openAddEmployeeGridHeight = value;
                OnPropertyChanged();
            }
        }
        public double OpenSearchEmployeeGridHeight
        {
            get => _openSearchEmployeeGridHeight;
            set
            {
                if (value.Equals(_openSearchEmployeeGridHeight)) return;
                _openSearchEmployeeGridHeight = value;
                OnPropertyChanged();
            }
        }
        public double OpenChangeEmployeeGridHeight
        {
            get => _openChangeEmployeeGridHeight;
            set
            {
                if (value.Equals(_openChangeEmployeeGridHeight)) return;
                _openChangeEmployeeGridHeight = value;
                OnPropertyChanged();
            }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (Equals(value, _selectedEmployee)) return;
                _selectedEmployee = value;
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

        private bool CanAppChangeEmployeePhotoCommandExecute(object arg) => true;
        private void OnAppChangeEmployeePhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
                SelectedEmployee.Image = File.ReadAllBytes(dialog.FileName);
        }

        private bool CanAppChangeEmployeeCommandExecute(object arg) => true;
        private async void OnAppChangeEmployeeCommandExecute(object obj)
        {
            if (await StoreManagement.DataBaseControl.ChangeEmployeeAsync(SelectedEmployee))
            {
                FillAllLists(SelectedDepartament);
                if (OpenChangeEmployeeGridHeight == 0d)
                    OpenChangeEmployeeGridHeight = 40d;
            }
        }


        private bool CanAppCloseChangeGridCommandExecute(object arg) => true;
        private void OnAppCloseChangeGridCommandExecute(object obj)
        {
            OpenChangeEmployeeGridHeight = OpenChangeEmployeeGridHeight == 40 ? 0d : 40d;
        }

        private bool CanAppOpenChangeEmployeeGridCommandExecute(object arg) => true;
        private void OnAppOpenChangeEmployeeGridCommandExecute(object obj)
        {
            if (OpenChangeEmployeeGridHeight == 0d)
                OpenChangeEmployeeGridHeight = 40d;
        }

        private bool CanAppSearchEmployeeCommandExecute(object arg) => true;
        private void OnAppSearchEmployeeCommandExecute(object obj)
        {
            int count = 0;

            var list = new List<IEnumerable<Employee>>();
            int id;
            if (!string.IsNullOrEmpty(SearchEmployee.Id))
            {
                if (int.TryParse(SearchEmployee.Id, out id))
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
            FillSelectedEmployees(new ObservableCollection<Employee>(Result));
        }

        private bool CanAppOpenSearchGridCommandExecute(object arg) => true;
        private void OnAppOpenSearchGridCommandExecute(object obj)
        {
            OpenAddEmployeeGridHeight = 0;
            OpenSearchEmployeeGridHeight = OpenSearchEmployeeGridHeight == 40 ? 0d : 40d;
        }

        private bool CanAppDeleteEmployeeCommandExecute(object arg) => true;
        private async void OnAppDeleteEmployeeCommandExecute(object obj)
        {
            string Name = SelectedEmployee.Name;
            if (await StoreManagement.DataBaseControl.RemoveEmployeeAsync(SelectedEmployee.Id))
            {
                FillAllLists(SelectedDepartament);
                MessageBox.Show($"Пользователь {Name} удалён");
            }
            else
            {
                MessageBox.Show($"Ошибка при удалении пользователя {Name}");
            }
        }

        private bool CanAppOpenAddNewDepartamentCommandExecute(object arg) => true;
        private void OnAppOpenAddNewDepartamentCommandExecute(object obj)
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
            try
            {
                if (NewDepartament.Image != null && NewDepartament.Image.Any())
                {
                    await StoreManagement.DataBaseControl.AddDepartamentAsync(NewDepartament.Name, NewDepartament.Image);
                    FillAllLists();
                    SelectedDepartament = Departments.FirstOrDefault(x => x.Name.ToLower() == NewDepartament.Name.ToLower());
                    FillSelectedEmployees();
                }
                else
                {
                    await StoreManagement.DataBaseControl.AddDepartamentAsync(NewDepartament.Name);
                    FillAllLists();
                    SelectedDepartament = Departments.FirstOrDefault(x => x.Name.ToLower() == NewDepartament.Name.ToLower());
                    FillSelectedEmployees();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool CanAppDeleteDepartamentCommandExecute(object arg) => true;
        private async void OnAppDeleteDepartamentCommandExecute(object obj)
        {
            if (await StoreManagement.DataBaseControl.RemoveDepartamentAsync(SelectedDepartament.Id, SelectedDepartament.Name))
            {
                FillAllLists();
                FillSelectedEmployees(AllEmployees);
            }
        }

        private bool CanAppSaveNewEmployeeCommandExecute(object arg) => true;
        private async void OnAppSaveNewEmployeeCommandExecute(object obj)
        {
            if (await StoreManagement.DataBaseControl.AddEmployeeAsync(NewEmployee.Login, NewEmployee.Password,
                NewEmployee.AccessLevel, NewEmployee.Name, NewEmployee.Surname, NewEmployee.Email,
                NewEmployee.Department, NewEmployee.PhoneNumber))
            {
                FillAllLists(SelectedDepartament);
            }
        }

        private bool CanAppOpenAddEmployeeGridCommandExecute(object arg) => true;
        private void OnAppOpenAddEmployeeGridCommandExecute(object obj)
        {
            OpenSearchEmployeeGridHeight = 0;
            OpenAddEmployeeGridHeight = OpenAddEmployeeGridHeight == 40 ? 0d : 40;
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
            FillSelectedEmployees(AllEmployees);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Заполняет список SelectedEmployees на основании выбранного SelectedDepartament
        /// </summary>
        private void FillSelectedEmployees()
        {
            if (SelectedDepartament!=null && AllEmployees!=null)
            {
                SelectedEmployees = new ObservableCollection<Employee>(AllEmployees.Where(x => (x.Department!=null) && x.Department.Id == SelectedDepartament.Id));
            }
            else
            {
                FillSelectedEmployees(AllEmployees);
            }
        }
        
        /// <summary>
        /// Заполняет список SelectedEmployees на основании списка Employees
        /// </summary>
        private void FillSelectedEmployees(ObservableCollection<Employee> Employees)
        {
            SelectedEmployees = Employees;
        }

        /// <summary>
        /// Заполняет списки Departaments, AllEmployees, AccessLevels
        /// </summary>
        private void FillAllLists()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Departments = new ObservableCollection<Department>(db.Departments);
                    AllEmployees = new ObservableCollection<Employee>(db.Employees.Include(x => x.Department));
                    AccessLevels = new ObservableCollection<AccessLevel>(db.AccessLevels);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Заполняет списки Departaments, AllEmployees, AccessLevels, SelectedEmployees.
        /// selectedDepartment - выбранный департамент
        /// </summary>
        private void FillAllLists(Department selectedDepartment)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Departments = new ObservableCollection<Department>(db.Departments.Include(x => x.Employees));
                    AllEmployees = new ObservableCollection<Employee>(db.Employees.Include(x => x.Department));
                    AccessLevels = new ObservableCollection<AccessLevel>(db.AccessLevels.Include(x => x.Employees));
                }

                if (selectedDepartment!=null)
                {
                    SelectedDepartament = Departments.FirstOrDefault(x => x.Id == selectedDepartment.Id);
                    FillSelectedEmployees();
                }
                else
                    FillSelectedEmployees(AllEmployees);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion

        public override void Dispose()
        {
            #region Properties

            ExpanderHeight = 0;
            OpenAddEmployeeGridHeight = 0;
            OpenSearchEmployeeGridHeight = 0;
            OpenChangeEmployeeGridHeight = 0;

            StoreManagement = null;
            
            SearchEmployee = null;
            NewEmployee = null;
            NewDepartament = null;
            SelectedDepartament = null;
            SelectedEmployee = null;
            
            SelectedEmployees = null;
            Departments = null;
            AccessLevels = null;
            AllEmployees = null;

            #endregion
            
            base.Dispose();
        }
        public override void FillViewModel()
        {
            #region Properties

            ExpanderHeight = 0;
            OpenAddEmployeeGridHeight = 0;
            OpenSearchEmployeeGridHeight = 0;
            OpenChangeEmployeeGridHeight = 0;

            StoreManagement = new Store();

            SearchEmployee = new SearchEmployee();
            NewEmployee = new Employee();
            NewDepartament = new Department();
            SelectedDepartament = new Department();

            SelectedEmployee = new Employee();

            SelectedEmployees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();

            FillAllLists();
            FillSelectedEmployees(AllEmployees);

            #endregion
            
            base.FillViewModel();
        }
    }
}

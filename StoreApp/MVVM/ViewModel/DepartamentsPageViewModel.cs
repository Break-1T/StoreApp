using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

            OpenAddNewDepartamentCommand = new RelayCommand(OnAppOpenAddNewDepartamentCommandExecute);
            UploadNewDepartamentPhotoCommand = new RelayCommand(OnAppUploadNewDepartamentPhotoCommandExecute);
            SaveNewDepartamentCommand = new RelayCommand(OnAppSaveNewDepartamentCommandExecute);
            DeleteDepartamentCommand = new RelayCommand(OnAppDeleteDepartamentCommandExecute);
            SaveNewEmployeeCommand = new RelayCommand(OnAppSaveNewEmployeeCommandExecute);
            UploadNewEmployeePhotoCommand = new RelayCommand(OnAppUploadNewEmployeePhotoCommandExecute);
            OpenAddEmployeeGridCommand = new RelayCommand(OnAppOpenAddEmployeeGridCommandExecute);
            ShowAllEmployeesCommand = new RelayCommand(OnAppShowAllEmployeesCommandExecute);
            DeleteEmployeeCommand = new RelayCommand(OnAppDeleteEmployeeCommandExecute);
            OpenSearchGridCommand = new RelayCommand(OnAppOpenSearchGridCommandExecute);
            SearchEmployeeCommand = new RelayCommand(OnAppSearchEmployeeCommandExecute);
            OpenChangeEmployeeGridCommand = new RelayCommand(OnAppOpenChangeEmployeeGridCommandExecute);
            CloseChangeGridCommand = new RelayCommand(OnAppCloseChangeGridCommandExecute);
            ChangeEmployeeCommand = new RelayCommand(OnAppChangeEmployeeCommandExecute);
            ChangeEmployeePhotoCommand = new RelayCommand(OnAppChangeEmployeePhotoCommandExecute);

            #endregion

            #region Properties

            ExpanderVisibility = Visibility.Collapsed;
            OpenAddEmployeeVisibility = Visibility.Collapsed;
            OpenSearchEmployeeVisibility = Visibility.Collapsed;
            OpenChangeEmployeeVisibility = Visibility.Collapsed;

            StoreManagement = new Store();

            SearchEmployee = new Employee(){AccessLevel = new AccessLevel()};
            NewEmployee = new Employee();
            NewDepartament = new Department();
            SelectedDepartament = new Department();

            SelectedEmployee = new Employee();

            SelectedEmployees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();

            Update();

            #endregion
        }

        #region Commands

        public RelayCommand OpenAddNewDepartamentCommand { get; }
        public RelayCommand UploadNewDepartamentPhotoCommand { get; }
        public RelayCommand SaveNewDepartamentCommand { get; }
        public RelayCommand DeleteDepartamentCommand { get; }
        
        public RelayCommand SaveNewEmployeeCommand { get; }
        public RelayCommand UploadNewEmployeePhotoCommand { get; }
        
        public RelayCommand OpenAddEmployeeGridCommand { get; }
        
        public RelayCommand ShowAllEmployeesCommand { get;}
        
        public RelayCommand DeleteEmployeeCommand { get; }
        public RelayCommand OpenSearchGridCommand { get; }
        
        public RelayCommand SearchEmployeeCommand { get; }
        
        public RelayCommand ChangeEmployeePhotoCommand { get; }
        public RelayCommand ChangeEmployeeCommand { get; }
        public RelayCommand OpenChangeEmployeeGridCommand { get;}
        public RelayCommand CloseChangeGridCommand { get; }

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
        
        private Visibility _openAddEmployeeVisibility;
        private Visibility _openSearchEmployeeVisibility;
        private Visibility _openChangeEmployeeVisibility;
        private Visibility _expanderVisibility;
        private Employee _searchEmployee;
        private int? _searchEmployeeId;

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

        public Employee SearchEmployee
        {
            get => _searchEmployee;
            set
            {
                if (Equals(value, _searchEmployee)) return;
                _searchEmployee = value;
                OnPropertyChanged();
            }
        }

        public Visibility ExpanderVisibility
        {
            get => _expanderVisibility;
            set
            {
                if (value.Equals(_expanderVisibility)) return;
                _expanderVisibility = value;
                OnPropertyChanged();
            }
        }
        public Visibility OpenAddEmployeeVisibility
        {
            get => _openAddEmployeeVisibility;
            set
            {
                if (value.Equals(_openAddEmployeeVisibility)) return;
                _openAddEmployeeVisibility = value;
                OnPropertyChanged();
            }
        }
        public Visibility OpenSearchEmployeeVisibility
        {
            get => _openSearchEmployeeVisibility;
            set
            {
                if (value.Equals(_openSearchEmployeeVisibility)) return;
                _openSearchEmployeeVisibility = value;
                OnPropertyChanged();
            }
        }
        public Visibility OpenChangeEmployeeVisibility
        {
            get => _openChangeEmployeeVisibility;
            set
            {
                if (value.Equals(_openChangeEmployeeVisibility)) return;
                _openChangeEmployeeVisibility = value;
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

        public int? SearchEmployeeId
        {
            get => _searchEmployeeId;
            set
            {
                if (value == _searchEmployeeId) return;
                _searchEmployeeId = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region CommandMethods

        private void OnAppChangeEmployeePhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
                SelectedEmployee.Image = File.ReadAllBytes(dialog.FileName);
        }

        private async void OnAppChangeEmployeeCommandExecute(object obj)
        {
            if (await StoreManagement.DataBaseControl.ChangeEmployeeAsync(SelectedEmployee))
            {
                FillAllLists(SelectedDepartament);
                if (OpenChangeEmployeeVisibility == Visibility.Collapsed)
                    OpenChangeEmployeeVisibility = Visibility.Visible;
                HasChanges = true;
            }
        }

        private void OnAppCloseChangeGridCommandExecute(object obj)
        {
            OpenChangeEmployeeVisibility = OpenChangeEmployeeVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnAppOpenChangeEmployeeGridCommandExecute(object obj)
        {
            if (OpenChangeEmployeeVisibility == Visibility.Collapsed)
                OpenChangeEmployeeVisibility = Visibility.Visible;
        }

        private void OnAppSearchEmployeeCommandExecute(object obj)
        {
            var Result = new ObservableCollection<Employee>(AllEmployees.Where(x=>
                StoreManagement.CanEqualsSearchQueryBeCompleted(x.Id,SearchEmployeeId)&&
                StoreManagement.CanEqualsSearchQueryBeCompleted(x.Email,SearchEmployee.Email) &&
                StoreManagement.CanEqualsSearchQueryBeCompleted(x.Login,SearchEmployee.Login)&&
                StoreManagement.CanEqualsSearchQueryBeCompleted(x.Password,SearchEmployee.Password)&&
                StoreManagement.CanEqualsSearchQueryBeCompleted(x.Name,SearchEmployee.Name)&&
                StoreManagement.CanEqualsSearchQueryBeCompleted(x.PhoneNumber,SearchEmployee.PhoneNumber) &&
                StoreManagement.CanEqualsSearchQueryBeCompleted(x.AccessLevel.Name,SearchEmployee.AccessLevel.Name)&&
                StoreManagement.CanEqualsSearchQueryBeCompleted(x.Surname,SearchEmployee.Surname))
            );

            SearchEmployee = new Employee() { AccessLevel = new AccessLevel() };
            SearchEmployeeId = null;

            FillSelectedEmployees(Result);
        }

        private void OnAppOpenSearchGridCommandExecute(object obj)
        {
            OpenAddEmployeeVisibility = Visibility.Collapsed;
            OpenSearchEmployeeVisibility = OpenSearchEmployeeVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private async void OnAppDeleteEmployeeCommandExecute(object obj)
        {
            string Name = SelectedEmployee.Name;
            if (await StoreManagement.DataBaseControl.RemoveEmployeeAsync(SelectedEmployee.Id))
            {
                FillAllLists(SelectedDepartament);
                MessageBox.Show($"Пользователь {Name} удалён");
                HasChanges = true;
            }
            else
            {
                MessageBox.Show($"Ошибка при удалении пользователя {Name}");
            }
        }

        private void OnAppOpenAddNewDepartamentCommandExecute(object obj)
        {
            ExpanderVisibility = ExpanderVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnAppUploadNewDepartamentPhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName != null)
                NewDepartament.Image = File.ReadAllBytes(dialog.FileName);
        }

        private async void OnAppSaveNewDepartamentCommandExecute(object obj)
        {
            try
            {
                if (NewDepartament.Image != null && NewDepartament.Image.Any())
                {
                    await StoreManagement.DataBaseControl.AddDepartamentAsync(NewDepartament.Name, NewDepartament.Image);
                    FillAllListsAsync();
                    SelectedDepartament = Departments.FirstOrDefault(x => x.Name.ToLower() == NewDepartament.Name.ToLower());
                    FillSelectedEmployees();
                    HasChanges = true;
                }
                else
                {
                    await StoreManagement.DataBaseControl.AddDepartamentAsync(NewDepartament.Name);
                    FillAllListsAsync();
                    SelectedDepartament = Departments.FirstOrDefault(x => x.Name.ToLower() == NewDepartament.Name.ToLower());
                    FillSelectedEmployees();
                    HasChanges = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void OnAppDeleteDepartamentCommandExecute(object obj)
        {
            if (await StoreManagement.DataBaseControl.RemoveDepartmentAsync(SelectedDepartament.Id, SelectedDepartament.Name))
            {
                FillAllListsAsync();
                FillSelectedEmployees(AllEmployees);
                HasChanges = true;
            }
        }

        private async void OnAppSaveNewEmployeeCommandExecute(object obj)
        {
            if (await StoreManagement.DataBaseControl.AddEmployeeAsync(NewEmployee.Login, NewEmployee.Password,
                NewEmployee.AccessLevel, NewEmployee.Name, NewEmployee.Surname, NewEmployee.Email,
                NewEmployee.Department, NewEmployee.PhoneNumber))
            {
                FillAllLists(SelectedDepartament);
                HasChanges = true;
            }
        }

        private void OnAppOpenAddEmployeeGridCommandExecute(object obj)
        {
            OpenSearchEmployeeVisibility = Visibility.Collapsed;
            OpenAddEmployeeVisibility = OpenAddEmployeeVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnAppUploadNewEmployeePhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName != null)
                NewEmployee.Image = File.ReadAllBytes(dialog.FileName);
        }

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
        /// Заполняет асинхронно списки Departaments, AllEmployees, AccessLevels
        /// </summary>
        private async void FillAllListsAsync() => await Task.Run(FillAllLists);

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

        public override void Update()
        {
            FillAllListsAsync();
            FillSelectedEmployees(AllEmployees);
        }

        public override void Dispose()
        {
            #region Properties

            ExpanderVisibility = 0;
            OpenAddEmployeeVisibility = 0;
            OpenSearchEmployeeVisibility = 0;
            OpenChangeEmployeeVisibility = 0;

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
        }
    }
}

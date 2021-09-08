using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StoreApp.Infrastructure.Commands;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.MVVM.Model;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class UsersPageViewModel:BaseViewModel
    {
        public UsersPageViewModel()
        {
            DeleteUserCommand = new RelayCommand(OnDeleteUserCommandExecute, CanDeleteUserCommandExecute);
            AddNewUserCommand = new RelayCommand(OnAddNewUserCommandExecute, CanAddNewUserCommandExecute);
            ChangeUserCommand = new RelayCommand(OnChangeUserCommandExecute);

            SelectedUser = new User();
            _store = new Store();
            NewUser = new User();

            AddUserGridVisibility = System.Windows.Visibility.Collapsed;
            SearchUserGridVisibility = System.Windows.Visibility.Collapsed;
            EditUserGridVisibility = System.Windows.Visibility.Collapsed;

            Update();
        }

        #region Fields

        private ObservableCollection<User> _users;
        private ObservableCollection<Order> _orders;
        
        private User _selectedUser;

        private Store _store;

        private System.Windows.Visibility _addUserGridVisibility;
        private System.Windows.Visibility _searchUserGridVisibility;
        private User _newUser;
        private ObservableCollection<AccessLevel> _accessLevels;
        private System.Windows.Visibility _editUserGridVisibility;

        #endregion

        #region Properties

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                if (Equals(value, _users)) return;
                _users = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                if (Equals(value, _orders)) return;
                _orders = value;
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

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (Equals(value, _selectedUser)) return;
                _selectedUser = value;
                OnPropertyChanged();
            }
        }
        public User NewUser
        {
            get => _newUser;
            set
            {
                if (Equals(value, _newUser)) return;
                _newUser = value;
                OnPropertyChanged();
            }
        }

        public System.Windows.Visibility AddUserGridVisibility
        {
            get => _addUserGridVisibility;
            set
            {
                if (value == _addUserGridVisibility) return;
                _addUserGridVisibility = value;
                OnPropertyChanged();
            }
        }
        public System.Windows.Visibility SearchUserGridVisibility
        {
            get => _searchUserGridVisibility;
            set
            {
                if (value == _searchUserGridVisibility) return;
                _searchUserGridVisibility = value;
                OnPropertyChanged();
            }
        }

        public System.Windows.Visibility EditUserGridVisibility
        {
            get => _editUserGridVisibility;
            set
            {
                if (value == _editUserGridVisibility) return;
                _editUserGridVisibility = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand DeleteUserCommand { get; }
        public RelayCommand AddNewUserCommand { get; }

        /// <summary>
        /// Комманда для открытия Grid содержащего элементы редактирования пользователя
        /// </summary>
        public RelayCommand OpenEditUserGridVisibilityCommand
        {
            get
            {
                return new RelayCommand((x) =>
                {
                    EditUserGridVisibility = System.Windows.Visibility.Visible;
                });
            }
        }
        /// <summary>
        /// Комманда для закрытия Grid содержащего элементы редактирования пользователя
        /// </summary>
        public RelayCommand CloseEditUserGridVisibilityCommand
        {
            get
            {
                return new RelayCommand((x) =>
                {
                    EditUserGridVisibility = System.Windows.Visibility.Collapsed;
                });
            }
        }


        /// <summary>
        /// Комманда для открытия Grid содержащего элементы добавления пользователя
        /// </summary>
        public RelayCommand OpenAddUserGridVisibilityCommand
        {
            get
            {
                return new RelayCommand((object x) =>
                {
                    AddUserGridVisibility = AddUserGridVisibility == System.Windows.Visibility.Visible? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
                    SearchUserGridVisibility = System.Windows.Visibility.Collapsed;
                }, (object x) => true);
            }
        }
        /// <summary>
        /// Комманда для открытия Grid содержащего элементы поиска пользователя
        /// </summary>
        public RelayCommand OpenSearchUserGridVisibilityCommand
        {
            get
            {
                return new RelayCommand((object x) =>
                {
                    AddUserGridVisibility = System.Windows.Visibility.Collapsed;
                    SearchUserGridVisibility = SearchUserGridVisibility == System.Windows.Visibility.Visible? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
                }, (object x) => true);
            }
        }
        /// <summary>
        /// Комманда для записи фото в нового юзера
        /// </summary>
        public RelayCommand AddNewUserPhoto
        {
            get => new ((x) =>
            {
                OpenFileDialog dialog = new ();
                dialog.ShowDialog();
                try
                {
                    NewUser.Image = File.ReadAllBytes(dialog.FileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            },(x)=>true);
        }
        public RelayCommand ChangeSelectedUserPhotoCommand
        {
            get => new ((x) =>
            {
                OpenFileDialog dialog = new ();
                dialog.ShowDialog();
                try
                {
                    SelectedUser.Image = !string.IsNullOrEmpty(dialog.FileName)? File.ReadAllBytes(dialog.FileName): SelectedUser.Image;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }
        public RelayCommand ChangeUserCommand { get; }

        #endregion

        #region Command methods

        private bool CanDeleteUserCommandExecute(object arg) => true;
        private async void OnDeleteUserCommandExecute(object obj)
        {
            if (await _store.DataBaseControl.RemoveUserAsync(SelectedUser.Id))
            {
                FillUsersAsync();
                FillOrdersAsync();
                HasChanges = true;
            }
        }

        private bool CanAddNewUserCommandExecute(object arg) => true;
        private async void OnAddNewUserCommandExecute(object obj)
        {
            try
            {
                if (await _store.DataBaseControl.AddUserAsync(NewUser.Login, NewUser.Password, NewUser.Name, NewUser.Surname,
                    NewUser.PhoneNumber, NewUser.Email, NewUser.Image, NewUser.AccessLevel))
                {
                    FillUsersAsync();
                    HasChanges = true;
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void OnChangeUserCommandExecute(object obj)
        {
            try
            {
                if (await _store.DataBaseControl.ChangeUserAsync(SelectedUser))
                {
                    FillUsersAsync();
                    HasChanges = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion

        #region Methods

        private void FillUsers()
        {
            try
            {
                using (ApplicationContext db = new ())
                {
                    Users = new( db.Users.Include(e => e.AccessLevel).Select(x => new User()
                    {
                        AccessLevel = x.AccessLevel != null ? new AccessLevel { Id = x.AccessLevel.Id, Name = x.AccessLevel.Name } : null,
                        Orders = null,
                        Image = x.Image,
                        Email = x.Email,
                        Id = x.Id,
                        Login = x.Login,
                        Name = x.Name,
                        Password = x.Password,
                        PhoneNumber = x.PhoneNumber,
                        Surname = x.Surname
                    }));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void FillOrders()
        {
            try
            {
                using (ApplicationContext db = new ())
                {
                    Orders = new ObservableCollection<Order>(db.Orders.Include(x => x.Product).Include(t=>t.User));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void FillAccessLevels()
        {
            try
            {
                using (ApplicationContext db = new ())
                {
                    AccessLevels = new (db.AccessLevels.Include(x=>x.Users).Select(level=>new AccessLevel()
                    {
                        Users = new (level.Users.Select(user=>new User()
                            {
                                AccessLevel = user.AccessLevel,
                                Email=user.Email,
                                Id=user.Id,
                                Image = null,
                                Login = user.Login,
                                Name = user.Name,
                                Orders = new ObservableCollection<Order>(),
                                Password = user.Password,
                                PhoneNumber = user.PhoneNumber,
                                Surname = user.Surname
                            })
                        ),
                        Employees = new(),
                        Id=level.Id,
                        Name = level.Name
                    }));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void FillUsersAsync() => await Task.Run(FillUsers);
        private async void FillOrdersAsync() => await Task.Run(FillOrders);
        private async void FillAccessLevelsAsync() => await Task.Run(FillAccessLevels);

        public override void Update()
        {
            FillUsersAsync();
            FillOrdersAsync();
            FillAccessLevelsAsync();
        }

        public override void Dispose()
        {
            Users = null;
            Orders = null;
            AccessLevels = null;

            SelectedUser = null;

            AddUserGridVisibility = System.Windows.Visibility.Collapsed;
            SearchUserGridVisibility = System.Windows.Visibility.Collapsed;
            EditUserGridVisibility = System.Windows.Visibility.Collapsed;

            _store = null;

            base.Dispose();
        }

        #endregion

    }
}

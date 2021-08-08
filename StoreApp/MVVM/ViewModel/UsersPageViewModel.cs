using System;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.EntityFrameworkCore;
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
            DeleteUser = new RelayCommand(OnDeleteUserExecute, CanDeleteUserExecute);
        }

        #region Fields

        private ObservableCollection<User> _users;
        private ObservableCollection<Order> _orders;
        
        private User _selectedUser;

        private Store _store;

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

        #endregion

        #region Commands

        public RelayCommand DeleteUser { get; }

        #endregion

        #region Command methods

        private bool CanDeleteUserExecute(object arg) => true;
        private async void OnDeleteUserExecute(object obj)
        {
            if (await _store.DataBaseControl.RemoveUserAsync(SelectedUser.Id))
            {
                FillUsers();
                FillOrders();
            }
        }

        #endregion

        #region Methods

        public void FillUsers()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Users = new ObservableCollection<User>(db.Users.Include(x=>x.Orders));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void FillOrders()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Orders = new ObservableCollection<Order>(db.Orders.Include(x => x.Product).Include(t=>t.User));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public override void Dispose()
        {
            Users = null;
            Orders = null;
            SelectedUser = null;

            _store = null;

            base.Dispose();
        }

        public override void FillViewModel()
        {
            Users = new ObservableCollection<User>();
            Orders = new ObservableCollection<Order>();

            SelectedUser = new User();
            _store = new Store();

            FillOrders();
            FillUsers();

            base.FillViewModel();
        }

        #endregion

    }
}

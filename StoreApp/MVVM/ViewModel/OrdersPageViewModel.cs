using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.MVVM.Model;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class OrdersPageViewModel:BaseViewModel
    {
        public OrdersPageViewModel()
        {
            FillOrders();
            FillOrders();
            FillProducts();
        }

        #region Fields

        private Order _selectedOrder;
        private Order _newOrder;
        private ObservableCollection<User> _users;
        private ObservableCollection<Product> _products;
        private ObservableCollection<Order> _orders;

        #endregion

        #region Properties

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
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                if (Equals(value, _products)) return;
                _products = value;
                OnPropertyChanged();
            }
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (Equals(value, _selectedOrder)) return;
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }
        public Order NewOrder
        {
            get => _newOrder;
            set
            {
                if (Equals(value, _newOrder)) return;
                _newOrder = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Заполняет список Orders
        /// </summary>
        private void FillOrders()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Orders = new ObservableCollection<Order>(db.Orders.Include(x=>x.User).Include(t=>t.Product));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Заполняет список Users
        /// </summary>
        private void FillUsers()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Users = new ObservableCollection<User>(db.Users);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// Заполняет список Products
        /// </summary>
        private void FillProducts()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Products = new ObservableCollection<Product>(db.Products);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion
    }
}

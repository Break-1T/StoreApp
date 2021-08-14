﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using StoreApp.Infrastructure.Commands;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class AccessLevelsViewModel : BaseViewModel
    {

        public AccessLevelsViewModel()
        {
            AddNewAccessLevelCommand = new RelayCommand(OnAddNewAccessLevelCommandExecute, CanAddNewAccessLevelCommandExecute);
            DeleteAcessLevelCommand =
                new RelayCommand(OnDeleteAcessLevelCommandExecute, CanDeleteAcessLevelCommandExecute);
            AddAccessLevelGridVisibilityCommand = new RelayCommand((x) =>
            {
                AddAccessLevelGridVisibility = AddAccessLevelGridVisibility == System.Windows.Visibility.Collapsed
                    ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }, (e) => true);

            ChangeAccessLevelCommand = new RelayCommand(OnChangeAccessLevelCommandExecute);

            EditAccessLevelGridVisibilityCommand = new RelayCommand((x) =>
            {
                EditAccessLevelGridVisibility = System.Windows.Visibility.Visible;
            });

            CloseEditGridCommand = new RelayCommand((x) =>
            {
                EditAccessLevelGridVisibility = System.Windows.Visibility.Collapsed;
            });

            FillViewModel();
        }

        #region Commands

        public RelayCommand AddNewAccessLevelCommand { get; }
        public RelayCommand AddAccessLevelGridVisibilityCommand { get; }
        public RelayCommand DeleteAcessLevelCommand { get; }
        public RelayCommand ChangeAccessLevelCommand { get; }
        public RelayCommand EditAccessLevelGridVisibilityCommand { get; }
        public RelayCommand CloseEditGridCommand { get; }

        #endregion

        #region Fields

        private ObservableCollection<AccessLevel> _accessLevels;
        private AccessLevel _selectedAccessLevel;
        private AccessLevel _newAccessLevel;

        private Store _store;
        private System.Windows.Visibility _addAccessLevelGridVisibility;
        private System.Windows.Visibility _editAccessLevelGridVisibility;

        #endregion

        #region Properties

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

        public AccessLevel SelectedAccessLevel
        {
            get => _selectedAccessLevel;
            set
            {
                if (Equals(value, _selectedAccessLevel)) return;
                _selectedAccessLevel = value;
                OnPropertyChanged();
            }
        }
        public AccessLevel NewAccessLevel
        {
            get => _newAccessLevel;
            set
            {
                if (Equals(value, _newAccessLevel)) return;
                _newAccessLevel = value;
                OnPropertyChanged();
            }
        }

        public System.Windows.Visibility AddAccessLevelGridVisibility
        {
            get => _addAccessLevelGridVisibility;
            set
            {
                if (value == _addAccessLevelGridVisibility) return;
                _addAccessLevelGridVisibility = value;
                OnPropertyChanged();
            }
        }
        public System.Windows.Visibility EditAccessLevelGridVisibility
        {
            get => _editAccessLevelGridVisibility;
            set
            {
                if (value == _editAccessLevelGridVisibility) return;
                _editAccessLevelGridVisibility = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Methods

        private async void OnChangeAccessLevelCommandExecute(object obj)
        {
            if (await _store.DataBaseControl.ChangeAccessLevelAsync(SelectedAccessLevel.Id,SelectedAccessLevel.Name))
            {
                FillAccessLevels();
            }
        }

        private bool CanDeleteAcessLevelCommandExecute(object arg) => true;
        private async void OnDeleteAcessLevelCommandExecute(object obj)
        {
            try
            {
                if (await _store.DataBaseControl.DeleteAccessLevelAsync(SelectedAccessLevel.Id))
                {
                    FillAccessLevels();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool CanAddNewAccessLevelCommandExecute(object arg) => true;
        private async void OnAddNewAccessLevelCommandExecute(object obj)
        {
            try
            {
                if (await _store.DataBaseControl.AddAccessLevelAsync(NewAccessLevel.Name))
                {
                    FillAccessLevels();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void FillAccessLevels()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                AccessLevels =
                    new ObservableCollection<AccessLevel>(db.AccessLevels.Include(x => x.Employees)
                        .Include(t => t.Users));
            }
        }

        public override void Dispose()
        {
            AccessLevels = null;

            SelectedAccessLevel = null;
            _store = null;
            NewAccessLevel = null;

            AddAccessLevelGridVisibility = System.Windows.Visibility.Collapsed;
            EditAccessLevelGridVisibility = System.Windows.Visibility.Collapsed;

            base.Dispose();
        }

        public override void FillViewModel()
        {
            AccessLevels = new ObservableCollection<AccessLevel>();
            SelectedAccessLevel = new AccessLevel();
            _store = new Store();
            NewAccessLevel = new AccessLevel();

            AddAccessLevelGridVisibility = System.Windows.Visibility.Collapsed;
            EditAccessLevelGridVisibility = System.Windows.Visibility.Collapsed;

            FillAccessLevels();

            base.FillViewModel();
        }

        #endregion

    }
}

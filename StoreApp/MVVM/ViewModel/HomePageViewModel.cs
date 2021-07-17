using StoreApp.MVVM.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Infrastructure.Commands;
using StoreApp.Resources;

namespace StoreApp.MVVM.ViewModel
{
    class HomePageViewModel:BaseViewModel
    {
        public HomePageViewModel()
        {
            EditCommand = new RelayCommand(OnAppEditCommandExecute, CanAppEditCommandExecute);
            SaveChangesCommand = new RelayCommand(OnAppSaveChangesCommandExecute, CanAppSaveChangesCommandExecute);
            DeclineChangesCommand = new RelayCommand(OnAppDeclineChangesCommandExecute, CanAppDeclineChangesCommandExecute);

            ButtonsVisibility = Visibility.Hidden.ToString();
        }

        #region Fields

        private string _buttonsVisibility;

        #endregion

        #region Properties

        public MainWindowViewModel MainWM { get; set; }

        public string ButtonsVisibility
        {
            get => _buttonsVisibility;
            set
            {
                if (value == _buttonsVisibility) return;
                _buttonsVisibility = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand EditCommand { get; }
        public RelayCommand SaveChangesCommand { get; }
        public RelayCommand DeclineChangesCommand { get; }

        #endregion

        private bool CanAppEditCommandExecute(object arg) => true;
        private void OnAppEditCommandExecute(object obj)
        {
            ButtonsVisibility = Visibility.Visible.ToString();
        }

        private bool CanAppDeclineChangesCommandExecute(object arg) => true;
        private void OnAppDeclineChangesCommandExecute(object obj)
        {
            ButtonsVisibility = Visibility.Hidden.ToString();
        }

        private bool CanAppSaveChangesCommandExecute(object arg) => true;
        private void OnAppSaveChangesCommandExecute(object obj)
        {
            ButtonsVisibility = Visibility.Hidden.ToString();
        }

    }

    public enum Visibility
    {
        Visible,
        Hidden
    }
}

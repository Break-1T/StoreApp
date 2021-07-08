using StoreApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreApp.MVVM.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            this.Loaded += AuthorizationPage_Loaded;
        }

        private void AuthorizationPage_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as StartViewModel).AuthorizationPasswordHandler = () =>Password.Password;
        }
    }
}

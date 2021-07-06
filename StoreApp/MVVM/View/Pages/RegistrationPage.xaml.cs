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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            this.Loaded += RegistrationPage_Loaded;
        }

        private void RegistrationPage_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as StartViewModel).Password1Handler = () => pass_1.Password;
            (this.DataContext as StartViewModel).Password2Handler = () => pass_2.Password;
        }
    }
}

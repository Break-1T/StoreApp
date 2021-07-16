using StoreApp.MVVM.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Resources;

namespace StoreApp.MVVM.ViewModel
{
    class HomePageViewModel:BaseViewModel
    {
        public HomePageViewModel()
        {
            
        }
        public MainWindowViewModel MainWV { get; set; }
    }
}

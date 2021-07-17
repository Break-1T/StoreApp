using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class DepartamentsPageViewModel:BaseViewModel
    {
        public DepartamentsPageViewModel()
        {
            
        }
        public MainWindowViewModel MainVM { get; set; }
    }
}

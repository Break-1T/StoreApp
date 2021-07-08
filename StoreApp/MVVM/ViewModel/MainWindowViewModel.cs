using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.MVVM.Model;
using StoreApp.MVVM.ViewModel.Base;

namespace StoreApp.MVVM.ViewModel
{
    class MainWindowViewModel:BaseViewModel
    {
        public Employee Employee { get; set; }
    }
}

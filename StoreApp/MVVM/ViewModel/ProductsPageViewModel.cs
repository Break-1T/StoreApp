using StoreApp.Infrastructure.Commands;
using StoreApp.MVVM.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.MVVM.Model;

namespace StoreApp.MVVM.ViewModel
{
    class ProductsPageViewModel:BaseViewModel
    {
        public ProductsPageViewModel()
        {
            AddCategoryCommand = new RelayCommand(OnAppAddCategoryCommandExecute,
                CanAppAddCategoryCommandExecute);
            UploadCategoryPhotoCommand = new RelayCommand(OnAppUploadCategoryPhotoCommandExecute,
                CanAppUploadCategoryPhotoCommandExecute);
            SaveCategoryCommand = new RelayCommand(OnAppSaveCategoryCommandExecute,
                CanAppSaveCategoryCommandExecute);

            ExpanderHeight = 0;
            
            Category = new Category();
            Categories = new ObservableCollection<Category>();
            
            FillCategories();
        }

        #region Commands

        public RelayCommand AddCategoryCommand { get; set; }
        public RelayCommand UploadCategoryPhotoCommand { get; set; }
        public RelayCommand SaveCategoryCommand { get; set; }

        #endregion

        #region Fields

        private Category _category;
        private double _expanderHeight;

        #endregion

        #region Properties

        public MainWindowViewModel MainVM { get; set; }
        public Category Category
        {
            get => _category;
            set
            {
                if (Equals(value, _category)) return;
                _category = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Category> Categories { get; set; }

        public double ExpanderHeight
        {
            get => _expanderHeight;
            set
            {
                if (value.Equals(_expanderHeight)) return;
                _expanderHeight = value;
                OnPropertyChanged();
            }
        }

        #endregion


        private bool CanAppAddCategoryCommandExecute(object arg) => true;
        private void OnAppAddCategoryCommandExecute(object obj)
        {
            ExpanderHeight = ExpanderHeight == 45d ? 0d : 45d;
        }

        private bool CanAppUploadCategoryPhotoCommandExecute(object arg) => true;
        private void OnAppUploadCategoryPhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            Category.Image = File.ReadAllBytes(dialog.FileName);
        }

        private bool CanAppSaveCategoryCommandExecute(object arg) => true;
        private void OnAppSaveCategoryCommandExecute(object obj)
        {
            if (Category.Image.Any())
                MainVM.StoreManagement.DataBaseControl.AddCategoryAsync(Category.Name, Category.Image);
            else
                MainVM.StoreManagement.DataBaseControl.AddCategoryAsync(Category.Name);
        }

        private void FillCategories()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var i in db.Categories)
                    {
                        Categories.Add(i);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

using StoreApp.Infrastructure.Commands;
using StoreApp.MVVM.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
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
            DeleteCategoryCommand = new RelayCommand(OnAppDeleteCategoryCommandExecute,
                CanAppDeleteCategoryCommandExecute);
            AddProductCommand = new RelayCommand(OnAppAddProductCommandExecute,
                CanAppAddProductCommandExecute);
            UploadNewProductPhotoCommand = new RelayCommand(OnAppUploadNewProductPhotoCommandExecute,
                CanAppUploadNewProductPhotoCommandExecute);
            OpenAddProductGridCommand = new RelayCommand(OnAppOpenAddProductGridCommandExecute,
                CanAppOpenAddProductGridCommandExecute);

            ExpanderHeight = 0;
            OpenAddProductGridHeight = 0;

            NewProduct = new Product();
            Category = new Category();
            SelectedCategory = new Category();
            
            Categories = new ObservableCollection<Category>();
            
            FillCategories();
        }


        #region Commands

        public RelayCommand AddCategoryCommand { get; set; }
        public RelayCommand UploadCategoryPhotoCommand { get; set; }
        public RelayCommand SaveCategoryCommand { get; set; }
        public RelayCommand DeleteCategoryCommand { get; set; }
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand UploadNewProductPhotoCommand { get; set; }
        public RelayCommand OpenAddProductGridCommand { get; set; }

        #endregion

        #region Fields

        private Category _category;
        private double _expanderHeight;
        private Category _selectedCategory;
        private ObservableCollection<Product> _selectedProducts;
        private Product _newProduct;
        private double _openAddProductGridHeight;

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
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (Equals(value, _selectedCategory)) return;
                _selectedCategory = value;
                OnPropertyChanged();
                FillSelectedProducts();
            }
        }
        public ObservableCollection<Product> SelectedProducts
        {
            get => _selectedProducts;
            set
            {
                if (Equals(value, _selectedProducts)) return;
                _selectedProducts = value;
                OnPropertyChanged();
            }
        }

        public Product NewProduct
        {
            get => _newProduct;
            set
            {
                if (Equals(value, _newProduct)) return;
                _newProduct = value;
                OnPropertyChanged();
            }
        }

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

        public double OpenAddProductGridHeight
        {
            get => _openAddProductGridHeight;
            set
            {
                if (value.Equals(_openAddProductGridHeight)) return;
                _openAddProductGridHeight = value;
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
            if (dialog.FileName!=null)
                Category.Image = File.ReadAllBytes(dialog.FileName);
        }

        private bool CanAppSaveCategoryCommandExecute(object arg) => true;
        private async void OnAppSaveCategoryCommandExecute(object obj)
        {
            if (Category.Image.Any())
            {
                await MainVM.StoreManagement.DataBaseControl.AddCategoryAsync(Category.Name, Category.Image);
                FillCategories();
            }
            else
            {
                await MainVM.StoreManagement.DataBaseControl.AddCategoryAsync(Category.Name);
                FillCategories();
            }
        }

        private bool CanAppDeleteCategoryCommandExecute(object arg) => true;
        private async void OnAppDeleteCategoryCommandExecute(object obj)
        {
            await MainVM.StoreManagement.DataBaseControl.RemoveCategoryAsync(SelectedCategory.Id, SelectedCategory.Name);
            FillCategories();
        }

        private bool CanAppAddProductCommandExecute(object arg) => true;
        private async void OnAppAddProductCommandExecute(object obj)
        {
            if (NewProduct.Image.Any())
            {
                await MainVM.StoreManagement.DataBaseControl.AddProductAsync(NewProduct.Name,NewProduct.Image, SelectedCategory);
            }
            else
            {
                await MainVM.StoreManagement.DataBaseControl.AddProductAsync(NewProduct.Name, SelectedCategory);
            }
            FillSelectedProducts();
        }


        private bool CanAppOpenAddProductGridCommandExecute(object arg) => true;
        private void OnAppOpenAddProductGridCommandExecute(object obj)
        {
            OpenAddProductGridHeight = OpenAddProductGridHeight == 30d ? 0d : 30d;
        }

        private bool CanAppUploadNewProductPhotoCommandExecute(object arg) => true;
        private void OnAppUploadNewProductPhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName != null)
                NewProduct.Image = File.ReadAllBytes(dialog.FileName);
        }

        private void FillCategories()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Categories.Clear();
                    foreach (var i in db.Categories.Include(x=>x.Products))
                    {
                        Categories.Add(i);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void FillSelectedProducts()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var categories = db.Categories.Include(x => x.Products).ToList();
                    SelectedProducts = SelectedCategory != null ? categories.FirstOrDefault(x => x.Id == SelectedCategory.Id)?.Products : null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}

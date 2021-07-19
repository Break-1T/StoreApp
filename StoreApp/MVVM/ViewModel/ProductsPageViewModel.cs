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

            #region Commands
            
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
            ShowAllProductsCommand = new RelayCommand(OnAppShowAllProductsCommandExecute,
                CanAppShowAllProductsCommandExecute);
            DeleteProductCommand = new RelayCommand(OnAppDeleteProductCommandExecute,
                CanAppDeleteProductCommandExecute);

            #endregion

            #region Properties

            ExpanderHeight = 0;
            OpenAddProductGridHeight = 0;

            NewProduct = new Product();
            Category = new Category();
            SelectedCategory = new Category();

            SelectedProducts = new ObservableCollection<Product>();
            Categories = new ObservableCollection<Category>();

            FillAllProducts();
            SelectedProducts = AllProducts;
            FillCategories();

            #endregion

        }


        #region Events


        #endregion

        #region Commands

        public RelayCommand AddCategoryCommand { get; set; }
        public RelayCommand UploadCategoryPhotoCommand { get; set; }
        public RelayCommand SaveCategoryCommand { get; set; }
        public RelayCommand DeleteCategoryCommand { get; set; }
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand UploadNewProductPhotoCommand { get; set; }
        public RelayCommand OpenAddProductGridCommand { get; set; }
        public RelayCommand ShowAllProductsCommand { get; set; }
        public RelayCommand DeleteProductCommand { get; set; }

        #endregion

        #region Fields

        private Category _category;
        private double _expanderHeight;
        private Category _selectedCategory;
        private ObservableCollection<Product> _selectedProducts;
        private Product _newProduct;
        private double _openAddProductGridHeight;
        private ObservableCollection<Product> _allProducts;
        private ObservableCollection<Category> _categories;
        private Product _selectedProduct;

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

        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                if (Equals(value, _categories)) return;
                _categories = value;
                OnPropertyChanged();
            }
        }

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
        public ObservableCollection<Product> AllProducts
        {
            get => _allProducts;
            set
            {
                if (Equals(value, _allProducts)) return;
                _allProducts = value;
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

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (Equals(value, _selectedProduct)) return;
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region CommandMethods

        private bool CanAppDeleteProductCommandExecute(object arg) => true;
        private async void OnAppDeleteProductCommandExecute(object obj)
        {
            await MainVM.StoreManagement.DataBaseControl.RemoveProductAsync(SelectedProduct.Name,SelectedProduct.Id);
            FillAllProducts();
            FillSelectedProducts();
        }

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
            if (dialog.FileName != null)
                Category.Image = File.ReadAllBytes(dialog.FileName);
        }

        private bool CanAppSaveCategoryCommandExecute(object arg) => true;
        private async void OnAppSaveCategoryCommandExecute(object obj)
        {
            if (Category.Image != null && Category.Image.Any())
            {
                await MainVM.StoreManagement.DataBaseControl.AddCategoryAsync(Category.Name, Category.Image);
                FillAllProducts();
                FillCategories();
                FillSelectedProducts();
            }
            else
            {
                await MainVM.StoreManagement.DataBaseControl.AddCategoryAsync(Category.Name);
                FillAllProducts();
                FillCategories();
                FillSelectedProducts();
            }
        }

        private bool CanAppDeleteCategoryCommandExecute(object arg) => true;
        private async void OnAppDeleteCategoryCommandExecute(object obj)
        {
            await MainVM.StoreManagement.DataBaseControl.RemoveCategoryAsync(SelectedCategory.Id, SelectedCategory.Name);
            FillAllProducts();
            FillCategories();
            FillSelectedProducts();
        }

        private bool CanAppAddProductCommandExecute(object arg) => true;
        private async void OnAppAddProductCommandExecute(object obj)
        {
            if (NewProduct.Image.Any())
            {
                await MainVM.StoreManagement.DataBaseControl.AddProductAsync(NewProduct.Name, NewProduct.Image, SelectedCategory);
            }
            else
            {
                await MainVM.StoreManagement.DataBaseControl.AddProductAsync(NewProduct.Name, SelectedCategory);
            }
            FillAllProducts();
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

        private bool CanAppShowAllProductsCommandExecute(object arg) => true;
        private void OnAppShowAllProductsCommandExecute(object obj)
        {
            SelectedProducts = AllProducts;
        }

        #endregion
        
        #region Methods

        private void FillCategories()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Categories = new ObservableCollection<Category>( db.Categories.ToList());
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
                SelectedProducts = SelectedCategory != null
                    ? new ObservableCollection<Product>(AllProducts.Where(x => x.Category.Id == SelectedCategory.Id).ToList())
                    : new ObservableCollection<Product>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void FillAllProducts()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    AllProducts = new ObservableCollection<Product>(db.Products.Include(x => x.Category).ToList());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }

        #endregion

    }
}

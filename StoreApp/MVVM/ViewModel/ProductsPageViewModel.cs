using StoreApp.Infrastructure.Commands;
using StoreApp.MVVM.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using StoreApp.Annotations;
using StoreApp.Infrastructure.DbManagement;
using StoreApp.Infrastructure.StoreManagement;
using StoreApp.MVVM.Model;

namespace StoreApp.MVVM.ViewModel
{
    class ProductsPageViewModel:BaseViewModel
    {
        public ProductsPageViewModel()
        {
            #region Commands

            AddCategoryCommand = new RelayCommand(OnAppAddCategoryCommandExecute);
            UploadCategoryPhotoCommand = new RelayCommand(OnAppUploadCategoryPhotoCommandExecute);
            SaveCategoryCommand = new RelayCommand(OnAppSaveCategoryCommandExecute);
            DeleteCategoryCommand = new RelayCommand(OnAppDeleteCategoryCommandExecute);
            AddProductCommand = new RelayCommand(OnAppAddProductCommandExecute);
            UploadNewProductPhotoCommand = new RelayCommand(OnAppUploadNewProductPhotoCommandExecute);
            OpenAddProductGridCommand = new RelayCommand(OnAppOpenAddProductGridCommandExecute);
            ShowAllProductsCommand = new RelayCommand(OnAppShowAllProductsCommandExecute);
            DeleteProductCommand = new RelayCommand(OnAppDeleteProductCommandExecute);
            OpenSearchGridCommand = new RelayCommand(OnAppOpenSearchGridCommandExecute);
            SearchProductCommand = new RelayCommand(OnAppSearchProductCommandExecute);
            OpenChangeGridCommand = new RelayCommand(OnAppOpenChangeGridCommandExecute);
            CloseChangeGridCommand = new RelayCommand(OnAppCloseChangeGridCommandExecute);
            ChangeProductCommand = new RelayCommand(OnAppChangeProductCommandExecute);
            ChangeProductPhotoCommand = new RelayCommand(OnAppChangeProductPhotoCommandExecute);

            #endregion

            #region Properties

            ExpanderHeight = 0;
            OpenAddProductGridHeight = 0;
            OpenSearchProductGridHeight = 0;
            OpenChangeProductGridHeight = 0;

            SearchId = "";
            SearchName = "";
            SearchPrice = "";

            StoreManagement = new Store();
            NewProduct = new Product();
            Category = new Category();
            SelectedCategory = new Category();

            SelectedProducts = new ObservableCollection<Product>();
            Categories = new ObservableCollection<Category>();

            Update();

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
        public RelayCommand OpenSearchGridCommand { get; set; }
        public RelayCommand SearchProductCommand { get; set; }
        public RelayCommand ChangeProductPhotoCommand { get; set; }
        public RelayCommand ChangeProductCommand { get; set; }
        public RelayCommand OpenChangeGridCommand { get; set; }
        public RelayCommand CloseChangeGridCommand { get; set; }

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
        private double _openSearchProductGridHeight;
        private string _searchId;
        private string _searchPrice;
        private string _searchName;
        private double _openChangeProductGridHeight;

        #endregion

        #region Properties

        public Store StoreManagement { get; set; }
        
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
        public double OpenSearchProductGridHeight
        {
            get => _openSearchProductGridHeight;
            set
            {
                if (value.Equals(_openSearchProductGridHeight)) return;
                _openSearchProductGridHeight = value;
                OnPropertyChanged();
            }
        }
        public double OpenChangeProductGridHeight
        {
            get => _openChangeProductGridHeight;
            set
            {
                if (value.Equals(_openChangeProductGridHeight)) return;
                _openChangeProductGridHeight = value;
                OnPropertyChanged();
            }
        }

        public string SearchId
        {
            get => _searchId;
            set
            {
                if (value == _searchId) return;
                _searchId = value;
                OnPropertyChanged();
            }
        }
        public string SearchName
        {
            get => _searchName;
            set
            {
                if (value == _searchName) return;
                _searchName = value;
                OnPropertyChanged();
            }
        }
        public string SearchPrice
        {
            get => _searchPrice;
            set
            {
                if (value == _searchPrice) return;
                _searchPrice = value;
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

        private void OnAppChangeProductPhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (!string.IsNullOrEmpty(dialog.FileName))
                SelectedProduct.Image = File.ReadAllBytes(dialog.FileName);
        }

        private async void OnAppChangeProductCommandExecute(object obj)
        {
            if (await StoreManagement.DataBaseControl.ChangeProductAsync(SelectedProduct))
            {
                FillCategories(SelectedCategory);
                FillAllProductsAsync();
                FillSelectedProducts();
                if (OpenChangeProductGridHeight == 0d)
                    OpenChangeProductGridHeight = 40d;
                HasChanges = true;
            }
        }

        private void OnAppCloseChangeGridCommandExecute(object obj)
        {
            OpenChangeProductGridHeight = OpenChangeProductGridHeight == 40 ? 0d : 40d;
        }

        private void OnAppOpenChangeGridCommandExecute(object obj)
        {
            if (OpenChangeProductGridHeight == 0d)
                OpenChangeProductGridHeight = 40d;
        }

        //В дальнейшем изменю этот метод так, как во вью модели Orders и Department
        private void OnAppSearchProductCommandExecute(object obj)
        {
            decimal Price;
            decimal Id;
            int count = 0;

            var list = new List<IEnumerable<Product>>();

            if (!string.IsNullOrEmpty(SearchPrice))
            {
                if (decimal.TryParse(SearchPrice.Replace(".", ","), out Price))
                {
                    count++;
                    var tmp = AllProducts.Where(x => x.Price == Price);
                    list.Add(tmp);
                }
                else
                {
                    count++;
                    list.Add(new List<Product>());
                }
            }

            if (!string.IsNullOrEmpty(SearchId))
            {
                if (decimal.TryParse(SearchId, out Id))
                {
                    count++;
                    var tmp = AllProducts.Where(x => x.Id == Id);
                    list.Add(tmp);
                }
                else
                {
                    count++;
                    list.Add(new List<Product>());
                }
            }

            if (!string.IsNullOrEmpty(SearchName))
            {
                count++;
                var tmp = AllProducts.Where(x => x.Name.ToLower() == SearchName.ToLower());
                list.Add(tmp);
            }

            IEnumerable<Product> Result = new List<Product>();
            
            switch (count)
            {
                case 3:
                    Result = from i in list[0]
                        from j in list[1]
                        from k in list[2]
                        where i == j && j == k
                        select i;
                    break;
                case 2:
                    Result = from i in list[0]
                        from j in list[1]
                        where i == j
                        select i;
                    break;
                case 1:
                    Result = from i in list[0]
                        select i;
                    break;
                case 0:
                    Result = new ObservableCollection<Product>();
                    break;
            }

            SelectedProducts = new ObservableCollection<Product>(Result);

        }

        private void OnAppOpenSearchGridCommandExecute(object obj)
        {
            OpenAddProductGridHeight = 0;
            OpenSearchProductGridHeight = OpenSearchProductGridHeight == 40 ? 0d : 40d;
        }

        private async void OnAppDeleteProductCommandExecute(object obj)
        {
            if (await StoreManagement.DataBaseControl.RemoveProductAsync(SelectedProduct.Name, SelectedProduct.Id))
            {
                FillCategories(SelectedCategory);
                FillAllProductsAsync();
                FillSelectedProducts();
                HasChanges = true;
            }
        }

        private void OnAppAddCategoryCommandExecute(object obj)
        {
            ExpanderHeight = ExpanderHeight == 55d ? 0d : 55d;
        }

        private void OnAppUploadCategoryPhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName != null)
                Category.Image = File.ReadAllBytes(dialog.FileName);
        }

        private async void OnAppSaveCategoryCommandExecute(object obj)
        {
            if (Category.Image != null && Category.Image.Any())
            {
                if (await StoreManagement.DataBaseControl.AddCategoryAsync(Category.Name, Category.Image))
                {
                    FillCategoriesAsync();
                    FillAllProductsAsync();
                    SelectedCategory = Categories.FirstOrDefault(x => x.Name == Category.Name);
                    FillSelectedProducts();
                    HasChanges = true;
                }
            }
            else
            {
                if (await StoreManagement.DataBaseControl.AddCategoryAsync(Category.Name))
                {
                    FillCategoriesAsync();
                    FillAllProductsAsync();
                    SelectedCategory = Categories.FirstOrDefault(x => x.Name == Category.Name);
                    FillSelectedProducts();
                    HasChanges = true;
                }
            }
        }

        private async void OnAppDeleteCategoryCommandExecute(object obj)
        {
            if (await StoreManagement.DataBaseControl.RemoveCategoryAsync(SelectedCategory.Id, SelectedCategory.Name))
            {
                FillCategoriesAsync();
                FillAllProductsAsync();
                FillSelectedProducts();
                SelectedProducts = AllProducts;
                HasChanges = true;
            }
        }

        private async void OnAppAddProductCommandExecute(object obj)
        {
            if (NewProduct.Image.Any())
            {
                if (await StoreManagement.DataBaseControl.AddProductAsync(NewProduct.Name, NewProduct.Price, NewProduct.Image, SelectedCategory))
                {
                    FillCategories(SelectedCategory);
                    FillAllProductsAsync();
                    FillSelectedProducts();
                    HasChanges = true;
                }

            }
            else
            {
                if (await StoreManagement.DataBaseControl.AddProductAsync(NewProduct.Name, NewProduct.Price, SelectedCategory))
                {
                    FillCategories(SelectedCategory);
                    FillAllProductsAsync();
                    FillSelectedProducts();
                    HasChanges = true;
                }
            }

        }

        private void OnAppOpenAddProductGridCommandExecute(object obj)
        {
            OpenSearchProductGridHeight = 0;
            OpenAddProductGridHeight = OpenAddProductGridHeight == 40 ? 0d : 40;
        }

        private void OnAppUploadNewProductPhotoCommandExecute(object obj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName != null)
                NewProduct.Image = File.ReadAllBytes(dialog.FileName);
        }

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
                    Categories = new ObservableCollection<Category>( db.Categories.Include(x=>x.Products).ToList());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void FillCategories(Category selectedCategory)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Categories = new ObservableCollection<Category>(db.Categories.Include(x => x.Products).ToList());
                }
                SelectedCategory = Categories.FirstOrDefault(x => x.Id == selectedCategory.Id);
                if (selectedCategory != null)
                {
                    SelectedCategory = Categories.FirstOrDefault(x => x.Id == selectedCategory.Id);
                    FillSelectedProducts();
                }
                else
                    FillSelectedProducts(AllProducts);
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
                if (SelectedCategory != null && AllProducts != null)
                {
                    SelectedProducts = new ObservableCollection<Product>(AllProducts.Where(x => (x.Category != null) && x.Category.Id == SelectedCategory.Id));
                }
                else
                {
                    FillSelectedProducts(AllProducts);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void FillSelectedProducts(ObservableCollection<Product> products)
        {
            try
            {
                SelectedProducts = products;
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
                    AllProducts = new ObservableCollection<Product>(db.Products.Include(x => x.Category).Select(t => new
                        {
                            CatId = t.Category.Id,
                            CatName = t.Category.Name,
                            PrName = t.Name,
                            PrId = t.Id,
                            PrImage = t.Image,
                            PrPrice = t.Price
                        }).AsEnumerable().Select(an => new Product()
                        {
                            Category = new Category() { Id = an.CatId, Name = an.CatName },
                            Id = an.PrId,
                            Image = an.PrImage,
                            Name = an.PrName,
                            Price = an.PrPrice
                        })
                    );
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void FillCategoriesAsync() => await Task.Run(FillCategories);
        private async void FillAllProductsAsync() => await Task.Run(FillAllProducts);

        public override void Update()
        {
            FillCategoriesAsync();
            FillAllProducts();
            SelectedProducts = AllProducts;
        }

        public override void Dispose()
        {
            #region Properties

            ExpanderHeight = 0;
            OpenAddProductGridHeight = 0;
            OpenSearchProductGridHeight = 0;
            OpenChangeProductGridHeight = 0;

            SearchId = null;
            SearchName = null;
            SearchPrice = null;

            StoreManagement = null;
            NewProduct = null;
            Category = null;
            SelectedCategory = null;

            SelectedProducts = null;
            Categories = null;
            AllProducts = null;

            #endregion
        }

        #endregion
    }
}

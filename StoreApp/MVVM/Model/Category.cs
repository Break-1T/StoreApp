using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using StoreApp.Annotations;
using StoreApp.Resources;

namespace StoreApp.MVVM.Model
{
    class Category:INotifyPropertyChanged
    {
        private string _name;

        public Category()
        {
            Image = Images.Empty_category_icon;
            Products = new ObservableCollection<Product>();
        }
        [Key]
        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        [CanBeNull]
        public byte[] Image { get; set; }
        
        public ObservableCollection<Product> Products { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using StoreApp.Annotations;

namespace StoreApp.Infrastructure.Interfaces
{
    class ClassWithImage
    {
        [CanBeNull]
        public byte[] Image { get; set; }

        /// <summary>
        /// Свойство для полечения объекта BitmapImage из byte[]
        /// </summary>
        public BitmapImage GetBitmapImage
        {
            get
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = new MemoryStream(Image);
                image.DecodePixelHeight = 200;
                image.DecodePixelWidth = 200;
                image.EndInit();
                return image;
            }
        }
    }
}

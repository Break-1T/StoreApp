using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StoreApp.Infrastructure.Interfaces
{
    interface IByteArrayToBitmapImageConverter
    {
        public byte[] Image { get; set; }

        [NotMapped]
        public BitmapImage GetBitmapImage
        {
            get
            {
                using (var ms = new MemoryStream(Image))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
        }
    }
}

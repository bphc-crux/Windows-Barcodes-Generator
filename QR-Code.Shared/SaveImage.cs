using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;


namespace QR_Code
{
    public class SaveImage
    {
        public static async Task<bool> Save(string filename,WriteableBitmap bmp)
        {
            var files = await KnownFolders.SavedPictures.CreateFileAsync(
                         filename + ".png", CreationCollisionOption.GenerateUniqueName);
            string errorMessage = null;
            try
            {
                using (var pixelStream = bmp.PixelBuffer.AsStream())
                using (var stream = await files.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                    byte[] pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)bmp.PixelWidth, (uint)bmp.PixelHeight, 96.0, 96.0, pixels);
                    await encoder.FlushAsync();
                    await stream.FlushAsync();
                }
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                
                return false;
            }
           
            return true;
        }          
      
    }
}

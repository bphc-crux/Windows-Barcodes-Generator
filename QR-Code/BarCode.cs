using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
using ZXing;

namespace QR_Code
{
    public class BarCode
    {
        public static WriteableBitmap Generate(string s,BarcodeFormat format,int h,int w,Color fore,Color back)
        {
            try
            {
                BarcodeWriter _writer = new BarcodeWriter();
            _writer.Renderer = new ZXing.Rendering.PixelDataRenderer()
            {
                Foreground = fore,
                Background = back
            };
            _writer.Format = format;
            _writer.Options.Height = h;
            _writer.Options.Width = w; 
            _writer.Options.Margin = 1;
            var ob = _writer.Write(s).ToBitmap();
            return (WriteableBitmap)ob;
            }
            catch (Exception e) {
               }
            return null;
        }
       
    }
}

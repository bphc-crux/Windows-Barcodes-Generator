
using QR_Code.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ZXing;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace QR_Code
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BasicPage1 : Page
    {
        public static WriteableBitmap bmp;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public static String url = "";
        public BasicPage1()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }
         
        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Select.SelectedIndex = MainPage.selection;
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ImageViewer.IsOpen = false;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await SaveImageAsync();
        }
        public static string s = ""; public static int qselect=0;
        private void Customize_Click(object sender, RoutedEventArgs e)
        {
             if (s .Equals("")) { MessageDialogHelper.Show("Enter a value to generate the QR-Code.", "Generate a QR-code before customizing it."); return; }
            qselect = Select.SelectedIndex;
            if (Select.SelectedIndex == 0){ s=(Freetext.Text);}
            else if (Select.SelectedIndex == 1) {s=(Freetext2.Text);}
            else if (Select.SelectedIndex == 2) {  s = ("BEGIN:VCARD\nVERSION:3.0\nN:+" + Name.Text + ";" + FirstName.Text + "\nORG:" + Organisation.Text + "\nEMAIL;TYPE=INTERNET:" + Email.Text + "\nURL:" + Website.Text + "\nTEL;TYPE=CELL:" + Cell.Text + "\nTEL:" + Phone.Text + "\nTEL;TYPE=FAX:" + Fax.Text + "\nADR:" + Street.Text + ";" + City.Text + ";" + Region.Text + ";" + PostCode.Text + ";" + Country.Text + "\nEND:VCARD"); }
            else if (Select.SelectedIndex == 3) { s = ("tel:" + Freetext4.Text); }
            else if (Select.SelectedIndex == 4) {  s = ("SMSTO:" + PhoneNumber.Text + ":" + Message.Text); }
            this.Frame.Navigate(typeof(Customize),s);
            MessageDialogHelper.Show(Select.SelectedIndex + " " + qselect, s);
            
        }
        private void generate()
        {
            if (Select.SelectedIndex == 0) GenerateQRCode(Freetext.Text);
            else if (Select.SelectedIndex == 1) GenerateQRCode(Freetext2.Text);
            else if (Select.SelectedIndex == 2) GenerateQRCode("BEGIN:VCARD\nVERSION:3.0\nN:+" + Name.Text + ";" + FirstName.Text + "\nORG:" + Organisation.Text + "\nEMAIL;TYPE=INTERNET:" + Email.Text + "\nURL:" + Website.Text + "\nTEL;TYPE=CELL:" + Cell.Text + "\nTEL:" + Phone.Text + "\nTEL;TYPE=FAX:" + Fax.Text + "\nADR:"+Street.Text+";" + City.Text + ";" + Region.Text + ";"+PostCode.Text+";" + Country.Text + "\nEND:VCARD");
            else if (Select.SelectedIndex == 3) GenerateQRCode("tel:" + Freetext4.Text);
            else if (Select.SelectedIndex == 4) GenerateQRCode("SMSTO:" + PhoneNumber.Text + ":" + Message.Text);
        }
        private void GenerateQRCode(string s)
        {
            BasicPage1.s = s;
            BarcodeWriter _writer = new BarcodeWriter();
            _writer.Renderer = new ZXing.Rendering.PixelDataRenderer()
            {
                Foreground = Color.FromArgb(255, 0, 0, 0),
                Background = Color.FromArgb(255, 255, 255, 255)
            };
            _writer.Format = BarcodeFormat.QR_CODE;
            _writer.Options.Height = 400;
            _writer.Options.Width = 400;
            _writer.Options.Margin = 1;
            var barcodeImage = _writer.Write(s);
            var ob = barcodeImage.ToBitmap();
            bmp = (WriteableBitmap)ob;
            Image.Source = bmp;
            BigImage.Source = bmp;
        }
        private void Freetext_TextChanged(object sender, TextChangedEventArgs e)
        {
            generate();
        }
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ImageViewer.IsOpen = true;
        }
        private async Task<bool> SaveImageAsync()
        {
            String filename = "QR_";
            if (Select.SelectedIndex == 0) filename += "FreeText";
            else if (Select.SelectedIndex == 1) filename += "URL";
            else if (Select.SelectedIndex == 2) filename += "Contact";
            else if (Select.SelectedIndex == 3) filename += "Phone";
            else if (Select.SelectedIndex == 4) filename += "SMS";
            filename += bmp.PixelHeight + "x" + bmp.PixelWidth;
            var files = await KnownFolders.SavedPictures.CreateFileAsync(
                         filename+".png", CreationCollisionOption.GenerateUniqueName);
            string errorMessage = null;
            try
            {
                using (var pixelStream = bmp.PixelBuffer.AsStream())
                using (var stream = await files.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                    byte[] pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint) bmp.PixelWidth, (uint) bmp.PixelHeight,96.0,96.0, pixels);
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
                var dialog = new MessageDialog(errorMessage);
                await dialog.ShowAsync();
                return false;
            }
            MessageDialogHelper.Show("Image Saved" + Environment.NewLine + filename + ".png", "QR-Code");
            return true;
        }
        
    }
}

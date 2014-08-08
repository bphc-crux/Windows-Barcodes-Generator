using QR_Code.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
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
    public sealed partial class BarCodes2 : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public BarCodes2()
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
        WriteableBitmap bmp = null;
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ImageViewer.IsOpen = false;
        }
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            switch (Select.SelectedIndex)
            {
                case 0:
                    str = "AZTEC";
                    break;
                case 1:
                    str = "DATA_MATRIX";
                    break;
            }
            str += bmp.PixelHeight + "x" + bmp.PixelWidth;
            await SaveImage.Save(str, bmp);

        }
        public static string str = ""; public static int qselect =-1;
        private void Customize_Click(object sender, RoutedEventArgs e)
        {
            if (str.Equals("")) { MessageDialogHelper.Show("Enter a value to generate the barcode.", "Generate a barcode before customizing it."); return; }
            qselect = Select.SelectedIndex;
            this.Frame.Navigate(typeof(Customize_Barcodes2));
        }
        private void GenerateQRCode()
        {
            BarcodeFormat format = BarcodeFormat.AZTEC;
            switch (Select.SelectedIndex)
            {
                case 0:
                    str = aztecText.Text;
                    format = BarcodeFormat.AZTEC;
                    break;
                case 1:
                    str = dataText.Text;
                    format = BarcodeFormat.DATA_MATRIX;
                    break;
            }
            try
            {
                bmp = BarCode.Generate(str, format, 300, 300, Windows.UI.Colors.Black, Windows.UI.Colors.White);
                if (bmp != (null))
                {
                    Image.Source = bmp;
                    BigImage.Source = bmp;
                }
                else
                {
                    str = str.Substring(0, str.Length - 1);
                    switch (Select.SelectedIndex)
                    {

                        case 0:
                            aztecText.Text = str;
                            break;
                        case 1:
                            dataText.Text = str;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                aztecText.Text = "";
                dataText.Text = "";
            }
        }
        private void Freetext_TextChanged(object sender, TextChangedEventArgs e)
        {
            GenerateQRCode();
        }
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ImageViewer.IsOpen = true;
        }
               
        
    }
}

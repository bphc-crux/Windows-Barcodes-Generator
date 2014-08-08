﻿
using QR_Code.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
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
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Rendering;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace QR_Code
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Customize : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private WriteableBitmap bmp = null;

        public Customize()
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
        Windows.UI.Color fore = Windows.UI.Colors.Black;
        Windows.UI.Color back = Windows.UI.Colors.White;
        int size = 300;
        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ColorPickerPage.foreground == null)
            {
                Fore.Fill = new SolidColorBrush(Windows.UI.Colors.Black); fore = Windows.UI.Colors.Black;
                foreText.Text = "Foreground:" + "Black";
            }
            else { Fore.Fill = new SolidColorBrush((Color)(ColorPickerPage.foreground)); fore = (ColorPickerPage.foreground);
            foreText.Text = "Foreground:" +  ColorPickerPage.foretext;
            }
            if (ColorPickerPage.background == null) {Back.Fill = new SolidColorBrush(Windows.UI.Colors.White);back=Windows.UI.Colors.White;
            backText.Text = "Background:" +  "White";
            }
            else { Back.Fill = new SolidColorBrush((Color)(ColorPickerPage.background)); back = ColorPickerPage.background;
            backText.Text = "Background:" +  ColorPickerPage.backtext;
            }
            GenerateQRCode();
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
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ImageViewer.IsOpen = true;
        }
        
        private void GenerateQRCode()
        {
            string str = BasicPage1.s;
            try
            {
                bmp = BarCode.Generate(str, BarcodeFormat.QR_CODE, size, size, fore, back);
                if (bmp != (null))
                {
                    MyImage.Source = bmp;
                    BigImage.Source = bmp;
                }
            }
            catch (Exception)
            {
               
            }
        }
        private void rcustom_Checked(object sender, RoutedEventArgs e)
        {
           Customtb.Visibility = Visibility.Visible;
            r300.IsChecked = false;
            r200.IsChecked = false;
            r100.IsChecked = false;
            r50.IsChecked = false;
        }
        private void rcustom_Unchecked(object sender, RoutedEventArgs e)
        {

            try { rcustom.IsChecked = false; Customtb.Visibility = Visibility.Collapsed; }
            catch (Exception) { }
        }
        private void ForeGround_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ColorPickerPage),"Foreground");
        }
        private void BackGround_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ColorPickerPage),"Background");
        }
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)r300.IsChecked) { size = 300; GenerateQRCode(); }
            if ((bool)r200.IsChecked) { size = 200; GenerateQRCode(); }
            if ((bool)r100.IsChecked) { size = 100; GenerateQRCode(); }
            if ((bool)r50.IsChecked) { size = 50; GenerateQRCode(); }
            if ((bool)rcustom.IsChecked)
            {
                if (Int32.TryParse(Customt.Text, out size)) GenerateQRCode();
                else MessageDialogHelper.Show("Enter a valid number", "Error");
            }
            String filename = "QR_";
            if (BasicPage1.qselect == 0) filename += "FreeText";
            else if (BasicPage1.qselect == 1) filename += "URL";
            else if (BasicPage1.qselect == 2) filename += "Contact";
            else if (BasicPage1.qselect == 3) filename += "Phone";
            else if (BasicPage1.qselect == 4) filename += "SMS";
            filename += bmp.PixelHeight + "x" + bmp.PixelWidth;
            await SaveImage.Save(filename, bmp);

        }
   }
}
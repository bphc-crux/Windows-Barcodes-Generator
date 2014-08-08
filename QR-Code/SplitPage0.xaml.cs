using QR_Code.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Split Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234234

namespace QR_Code
{
    /// <summary>
    /// A page that displays a group title, a list of items within the group, and details for
    /// the currently selected item.
    /// </summary>
    public sealed partial class SplitPage0 : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public SplitPage0()
        {
            this.InitializeComponent();

            // Setup the navigation helper
            this.navigationHelper = new NavigationHelper(this);
            read();
        }
        private async void read()
        {
            StorageFile image = UserInformation.GetAccountPicture(AccountPictureKind.SmallImage) as StorageFile;
            if (image != null)
            {
                try
                {
                    IRandomAccessStream imageStream = await image.OpenReadAsync();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(imageStream);
                    BITS.Source = bitmapImage;
                }
                catch (Exception) { }
            }
            String dn = "Guest";
            try { dn = await UserInformation.GetDisplayNameAsync(); }
            catch (Exception) { dn = "Guest"; }
            try { dn = await UserInformation.GetFirstNameAsync(); }
            catch (Exception) { dn = "Guest"; }
           Welcome.Text = "Hi " + dn+",";
        }
        public static int selection = 0;
        List<Items> list1 = new List<Items>();
        List<Items> list2 = new List<Items>();

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            ResourceMap resourceMap;
            ResourceContext rc = new ResourceContext();
            resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
            for (int i = 10; i < 12; i++)
            {
                Items ob = new Items();
                ob.Title = resourceMap.GetValue("Title" + i, rc).ValueAsString;
                try { ob.Image = new BitmapImage(new Uri("ms-appx:Assets/" + resourceMap.GetValue("Title" + i, rc).ValueAsString + ".png")); }
                catch (Exception ) {  }
                list2.Add(ob);
            }
            listBox2.ItemsSource = list2;
        }
       
        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = listBox2.SelectedIndex;
            this.Frame.Navigate(typeof(BarCodes2));
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = listBox.SelectedIndex;
            this.Frame.Navigate(typeof(SplitPage1));
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 0;
            this.Frame.Navigate(typeof(SplitPage1));
        }

        private void itemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(BarCodes));
        }

        private void g1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 1;
            this.Frame.Navigate(typeof(BarCodes));
        }
        private void g2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 2;
            this.Frame.Navigate(typeof(BarCodes));
        }
        private void g3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 3;
            this.Frame.Navigate(typeof(BarCodes));
        }
        private void g4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 4;
            this.Frame.Navigate(typeof(BarCodes));
        }
        private void g5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 5;
            this.Frame.Navigate(typeof(BarCodes));
        }
        private void g6_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 6;
            this.Frame.Navigate(typeof(BarCodes));
        }
        private void g7_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 7;
            this.Frame.Navigate(typeof(BarCodes));
        }
        private void g8_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 8;
            this.Frame.Navigate(typeof(BarCodes));
        }
        private void g9_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 9;
            this.Frame.Navigate(typeof(BarCodes));
        }
    }
}

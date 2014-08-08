using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace QR_Code
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            read();
            this.SizeChanged += WindowingPage_SizeChanged;
        }
        void WindowingPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ApplicationView currentAppView = ApplicationView.GetForCurrentView();
            if (currentAppView.IsFullScreen)
            {
               
               
            }
            else if (currentAppView.AdjacentToLeftDisplayEdge)
            {
               
            }
            else if (currentAppView.AdjacentToRightDisplayEdge)
            {
                
            }
            else
            {
               
            }
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
            Welcome.Text = "Welcome, " + dn;
        }
        public static int selection = 0;
        private void Select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = (int)(Select.SelectedIndex);
            if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(SplitPage1));
                }
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Resources.Core;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace QR_Code
{
    public sealed partial class AppbarContent : UserControl
    {
        Frame Frame = Window.Current.Content as Frame;
        public AppbarContent()
        {
            this.InitializeComponent();
           // ChangeLanguage();
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(FreeText));
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(URL));
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Contact));
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Phone));
            }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Sms));
            }

        }
        
        
       
    }
}

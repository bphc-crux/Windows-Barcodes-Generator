using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

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
            this.NavigationCacheMode = NavigationCacheMode.Required;
            
        }
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

             for (int i = 1; i < 10; i++)
             {
                 Items ob = new Items();
                 ob.Title = resourceMap.GetValue("Title"+i, rc).ValueAsString;
                 try { ob.Image = new BitmapImage(new Uri("ms-appx:Assets/"+resourceMap.GetValue("Title" + i, rc).ValueAsString + ".png")); }
                 catch (Exception e1) { MessageDialogHelper.Show(e1.Message, ""); }
                 list1.Add(ob);
             }
             listBox1.ItemsSource = list1;
             for (int i = 10; i < 12; i++)
             {
                 Items ob = new Items();
                 ob.Title = resourceMap.GetValue("Title" + i, rc).ValueAsString;
                 try { ob.Image = new BitmapImage(new Uri("ms-appx:Assets/" + resourceMap.GetValue("Title" + i, rc).ValueAsString + ".png")); }
                 catch (Exception e1) { MessageDialogHelper.Show(e1.Message, ""); }
                 list2.Add(ob);
             }
             listBox2.ItemsSource = list2;
        }
        public static int selection = 0;

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = listBox1.SelectedIndex;
            this.Frame.Navigate(typeof(BarCodes));
        }
        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = listBox2.SelectedIndex;
            this.Frame.Navigate(typeof(BarCodes2));
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = listBox.SelectedIndex;
            this.Frame.Navigate(typeof(BasicPage1));
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selection = 0;
            this.Frame.Navigate(typeof(BasicPage1));
        }

        private async void feedback_Click(object sender, RoutedEventArgs e)
        {

            EmailRecipient sendTo = new EmailRecipient()
            {
                Address ="srujanjha.jha@gmail.com"
            };

            //generate mail object
            EmailMessage mail = new EmailMessage();
            mail.Subject = "Feedback for BarCodes Generator";
            mail.Body = "Write your feedback here...";

            //add recipients to the mail object
            mail.To.Add(sendTo);
            //mail.Bcc.Add(sendTo);
            //mail.CC.Add(sendTo);

            //open the share contract with Mail only:
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }
        private void about_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(About));
        }
        
    }
}

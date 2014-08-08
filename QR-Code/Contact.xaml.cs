using QR_Code.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using NotificationsExtensions.TileContent;
using Windows.UI.Notifications;
using Windows.Networking.Connectivity;
using Windows.Data.Xml.Dom;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace QR_Code
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Contact : Page
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


        public Contact()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            if (!IsInternet()) ShowPopupAnimationClicked("Oops! You are not connected to network service. The functionality of the app is decreased.");
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
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
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        String ImageUrl = "";
        void UpdateTile(TileTemplateType templateType)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(templateType);
            XmlNodeList textElements = tileXml.GetElementsByTagName("text");

            string tileText = "Contact";
            textElements.Item((uint)0).AppendChild(tileXml.CreateTextNode(tileText));


            XmlNodeList imageElements = tileXml.GetElementsByTagName("image");
            XmlElement imageElement = (XmlElement)imageElements.Item((uint)0);
            string imageSource = ImageUrl;
            imageElement.SetAttribute("src", imageSource);
            TileNotification tile = new TileNotification(tileXml);
            string tag = "Peek";
            tile.Tag = tag;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tile);
        }
        void UpdateImage()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            ITileSquare310x310Image tileContent = TileContentFactory.CreateTileSquare310x310Image();
            tileContent.AddImageQuery = true;
            tileContent.Image.Src = ImageUrl;
            tileContent.Image.Alt = "Web Image";
            ITileWide310x150ImageAndText01 wide310x150Content = TileContentFactory.CreateTileWide310x150ImageAndText01();
            wide310x150Content.TextCaptionWrap.Text = "Contact";
            wide310x150Content.Image.Src = ImageUrl;
            wide310x150Content.Image.Alt = "Web image";
            ITileSquare150x150Image square150x150Content = TileContentFactory.CreateTileSquare150x150Image();
            square150x150Content.Image.Src = ImageUrl;
            square150x150Content.Image.Alt = "Web image";
            wide310x150Content.Square150x150Content = square150x150Content;
            tileContent.Wide310x150Content = wide310x150Content;
            TileNotification tileNotification = tileContent.CreateNotification();
            string tag = "Image";
            tileNotification.Tag = tag;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
        private void ShowPopupAnimationClicked(String s)
        {
            if (!LightDismissAnimatedPopup.IsOpen)
            {
                errortext.Text = s;
                LightDismissAnimatedPopup.IsOpen = true;
            }
        }
        private void CloseAnimatedPopupClicked(object sender, RoutedEventArgs e)
        {
            if (LightDismissAnimatedPopup.IsOpen) { LightDismissAnimatedPopup.IsOpen = false; }
        }
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Equals("")) { ShowPopupAnimationClicked("Enter a valid Name to continue."); return; }
            
            String url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=BEGIN%3AVCARD%0AVERSION%3A3.0%0AN%3A+" + Name.Text + "%3B" + FirstName.Text + "%0AORG%3A" + Organisation.Text + "%0AEMAIL%3BTYPE%3DINTERNET%3A" + Email.Text + "%0AURL%3A" + Website.Text + "%0ATEL%3BTYPE%3DCELL%3A" + Cell.Text + "%0ATEL%3A" + Phone.Text + "%0ATEL%3BTYPE%3DFAX%3A" + Fax.Text + "%0AADR%3A%3B%3BA-203%3B" + City.Text + "%3B" + Region.Text + "%3B500078%3B" + Country.Text + "%0AEND%3AVCARD";
            ImageUrl = url;
            UpdateImage();
            UpdateTile((TileTemplateType)31);
            this.Frame.Navigate(typeof(Result), url);
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            String url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=BEGIN%3AVCARD%0AVERSION%3A3.0%0AN%3A+" + Name.Text + "%3B" + FirstName.Text + "%0AORG%3A" + Organisation.Text + "%0AEMAIL%3BTYPE%3DINTERNET%3A" + Email.Text + "%0AURL%3A" + Website.Text + "%0ATEL%3BTYPE%3DCELL%3A" + Cell.Text + "%0ATEL%3A" + Phone.Text + "%0ATEL%3BTYPE%3DFAX%3A" + Fax.Text + "%0AADR%3A%3B%3BA-203%3B" + City.Text + "%3B" + Region.Text + "%3B500078%3B" + Country.Text + "%0AEND%3AVCARD";
            Image.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
        }
        
    }
}

using NotificationsExtensions.TileContent;
using QR_Code.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Notifications;
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

// The Split Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234234

namespace QR_Code
{
    /// <summary>
    /// A page that displays a group title, a list of items within the group, and details for
    /// the currently selected item.
    /// </summary>
    public sealed partial class SplitPage1 : Page
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
        public SplitPage1()
        {
            this.InitializeComponent();
            
            if (SplitPage0.selection == 0)
            {
                itemListView.SelectedIndex = 0;
                User1.Visibility = Visibility.Visible;
                User2.Visibility = Visibility.Collapsed;
                User3.Visibility = Visibility.Collapsed;
                User4.Visibility = Visibility.Collapsed;
                User5.Visibility = Visibility.Collapsed;
                itemTitle.Text = "Freetext";
                itemSubtitle.Text = "QR-Code containing a simple text";
            }
            if (SplitPage0.selection == 1)
            {
                itemListView.SelectedIndex = 1;
                User1.Visibility = Visibility.Collapsed;
                User2.Visibility = Visibility.Visible;
                User3.Visibility = Visibility.Collapsed;
                User4.Visibility = Visibility.Collapsed;
                User5.Visibility = Visibility.Collapsed; 
                itemTitle.Text = "URL";
                itemSubtitle.Text = "QR-Code containing a URL of a website";
            }
            if (SplitPage0.selection == 2)
            {
                itemListView.SelectedIndex =2;
                User1.Visibility = Visibility.Collapsed;
                User2.Visibility = Visibility.Collapsed;
                User3.Visibility = Visibility.Visible;
                User4.Visibility = Visibility.Collapsed;
                User5.Visibility = Visibility.Collapsed; 
                itemTitle.Text = "Contact";
                itemSubtitle.Text = "QR-Code containing complete contact-info of a person";
            }
            if (SplitPage0.selection == 3)
            {
                itemListView.SelectedIndex = 3;
                User1.Visibility = Visibility.Collapsed;
                User2.Visibility = Visibility.Collapsed;
                User3.Visibility = Visibility.Collapsed;
                User4.Visibility = Visibility.Visible;
                User5.Visibility = Visibility.Collapsed; 
                itemTitle.Text = "Phone";
                itemSubtitle.Text = "QR-Code containing a phone number";
            }
            if (SplitPage0.selection == 4)
            {
                itemListView.SelectedIndex = 4;
                User1.Visibility = Visibility.Collapsed;
                User2.Visibility = Visibility.Collapsed;
                User3.Visibility = Visibility.Collapsed;
                User4.Visibility = Visibility.Collapsed;
                User5.Visibility = Visibility.Visible;
                itemTitle.Text = "SMS:";
                itemSubtitle.Text = "QR-Code containing a message along with a phone number";
            }
            // Setup the navigation helper
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            // Setup the logical page navigation components that allow
            // the page to only show one pane at a time.
            this.navigationHelper.GoBackCommand = new QR_Code.Common.RelayCommand(() => this.GoBack(), () => this.CanGoBack());
            this.itemListView.SelectionChanged += itemListView_SelectionChanged;

            // Start listening for Window size changes 
            // to change from showing two panes to showing a single pane
            Window.Current.SizeChanged += Window_SizeChanged;
            this.InvalidateVisualState();
            
        }
        void itemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (this.UsingLogicalPageNavigation())
            {
                this.navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
            }*/
            if (itemListView.SelectedIndex == 0)
            {
                User1.Visibility = Visibility.Visible;
                User2.Visibility = Visibility.Collapsed;
                User3.Visibility = Visibility.Collapsed;
                User4.Visibility = Visibility.Collapsed;
                User5.Visibility = Visibility.Collapsed;
                itemTitle.Text = "Freetext";
                itemSubtitle.Text = "QR-Code containing a simple text";
            }
            if (itemListView.SelectedIndex == 1)
            {
                User1.Visibility = Visibility.Collapsed;
                User2.Visibility = Visibility.Visible;
                User3.Visibility = Visibility.Collapsed;
                User4.Visibility = Visibility.Collapsed;
                User5.Visibility = Visibility.Collapsed;
                itemTitle.Text = "URL";
                itemSubtitle.Text = "QR-Code containing a URL of a website";
            }
            if (itemListView.SelectedIndex == 2)
            {
                User1.Visibility = Visibility.Collapsed;
                User2.Visibility = Visibility.Collapsed;
                User3.Visibility = Visibility.Visible;
                User4.Visibility = Visibility.Collapsed;
                User5.Visibility = Visibility.Collapsed;
                itemTitle.Text = "Contact";
                itemSubtitle.Text = "QR-Code containing complete contact-info of a person";
            }
            if (itemListView.SelectedIndex == 3)
            {
                User1.Visibility = Visibility.Collapsed;
                User2.Visibility = Visibility.Collapsed;
                User3.Visibility = Visibility.Collapsed;
                User4.Visibility = Visibility.Visible;
                User5.Visibility = Visibility.Collapsed;
                itemTitle.Text = "Phone";
                itemSubtitle.Text = "QR-Code containing a phone number";
            }
            if (itemListView.SelectedIndex == 4)
            {
                User1.Visibility = Visibility.Collapsed;
                User2.Visibility = Visibility.Collapsed;
                User3.Visibility = Visibility.Collapsed;
                User4.Visibility = Visibility.Collapsed;
                User5.Visibility = Visibility.Visible;
                itemTitle.Text = "SMS:";
                itemSubtitle.Text = "QR-Code containing a message along with a phone number";
            }
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
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Assign a bindable group to Me.DefaultViewModel("Group")
            // TODO: Assign a collection of bindable items to Me.DefaultViewModel("Items")

            if (e.PageState == null)
            {
                // When this is a new page, select the first item automatically unless logical page
                // navigation is being used (see the logical page navigation #region below.)
                if (!this.UsingLogicalPageNavigation() && this.itemsViewSource.View != null)
                {
                    this.itemsViewSource.View.MoveCurrentToFirst();
                }
            }
            else
            {
                // Restore the previously saved state associated with this page
                if (e.PageState.ContainsKey("SelectedItem") && this.itemsViewSource.View != null)
                {
                    // TODO: Invoke Me.itemsViewSource.View.MoveCurrentTo() with the selected
                    //       item as specified by the value of pageState("SelectedItem")

                }
            }
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
            if (this.itemsViewSource.View != null)
            {
                // TODO: Derive a serializable navigation parameter and assign it to
                //pageState("SelectedItem");

            }
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
        /*
        
        private void Submit1_Click(object sender, RoutedEventArgs e)
        {
            if (Freetext.Text.Equals("")) { ShowPopupAnimationClicked("Enter some text to continue."); return; }
            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=" + Freetext.Text;
            Change();
            UpdateImage("FreeText");
            UpdateTile((TileTemplateType)31, "FreeText");
            this.Frame.Navigate(typeof(SplitPage2), url);
        }
        private void Submit2_Click(object sender, RoutedEventArgs e)
        {
            if (Freetext2.Text.Equals("")) { ShowPopupAnimationClicked("Enter a valid URL to continue."); return; }
            if (!Uri.IsWellFormedUriString(Freetext2.Text, UriKind.RelativeOrAbsolute))
            {
                ShowPopupAnimationClicked("Enter a valid URL to continue."); return;
            }
            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=http%3A%2F%2F" + Freetext2.Text;
            Change();
            UpdateImage("URL/Website");
            UpdateTile((TileTemplateType)31, "URL/Website");
            this.Frame.Navigate(typeof(SplitPage2), url);
        }
        private void Submit3_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Equals("")) { ShowPopupAnimationClicked("Enter a valid Name to continue."); return; }

            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=BEGIN%3AVCARD%0AVERSION%3A3.0%0AN%3A+" + Name.Text + "%3B" + FirstName.Text + "%0AORG%3A" + Organisation.Text + "%0AEMAIL%3BTYPE%3DINTERNET%3A" + Email.Text + "%0AURL%3A" + Website.Text + "%0ATEL%3BTYPE%3DCELL%3A" + Cell.Text + "%0ATEL%3A" + Phone.Text + "%0ATEL%3BTYPE%3DFAX%3A" + Fax.Text + "%0AADR%3A%3B%3BA-203%3B" + City.Text + "%3B" + Region.Text + "%3B500078%3B" + Country.Text + "%0AEND%3AVCARD";
            Change();
            UpdateImage("Contact Info");
            UpdateTile((TileTemplateType)31, "Contact Info");
            this.Frame.Navigate(typeof(SplitPage2), url);
        }
        private void Submit4_Click(object sender, RoutedEventArgs e)
        {
            try { long n = Convert.ToInt64(Freetext4.Text); }
            catch (Exception) { ShowPopupAnimationClicked("Enter a valid Phone-Number to continue."); return; }
            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=tel%3A" + Freetext4.Text;
            Change();
            UpdateImage("Phone Number");
            UpdateTile((TileTemplateType)31, "Phone Number");
            this.Frame.Navigate(typeof(SplitPage2), url);
        }
        private void Submit5_Click(object sender, RoutedEventArgs e)
        {
            if (Message.Text.Equals("")) { ShowPopupAnimationClicked("Enter a valid Message to continue."); return; }
            try { long n = Convert.ToInt64(PhoneNumber.Text); }
            catch (Exception) { ShowPopupAnimationClicked("Enter a valid Phone-Number to continue."); return; }
            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=SMSTO%3A" + PhoneNumber.Text + "%3A" + Message.Text;
            Change();
            UpdateImage("SMS");
            UpdateTile((TileTemplateType)31, "SMS");
            this.Frame.Navigate(typeof(SplitPage2), url);
        }
        private void Freetext_TextChanged(object sender, TextChangedEventArgs e)
        {
            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=" + Freetext.Text;
            Change();
        }
        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=tel%3A" + Freetext4.Text;
            Change();
        }
        private void URL_TextChanged(object sender, TextChangedEventArgs e)
        {
            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=http%3A%2F%2F" + Freetext2.Text;
            Change();
        }
        private void Message_TextChanged(object sender, TextChangedEventArgs e)
        {
            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=SMSTO%3A" + PhoneNumber.Text + "%3A" + Message.Text;
            Change();
        }
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            url = "http://chart.apis.google.com/chart?chs=300x300&cht=qr&chld=|1&chl=BEGIN%3AVCARD%0AVERSION%3A3.0%0AN%3A+" + Name.Text + "%3B" + FirstName.Text + "%0AORG%3A" + Organisation.Text + "%0AEMAIL%3BTYPE%3DINTERNET%3A" + Email.Text + "%0AURL%3A" + Website.Text + "%0ATEL%3BTYPE%3DCELL%3A" + Cell.Text + "%0ATEL%3A" + Phone.Text + "%0ATEL%3BTYPE%3DFAX%3A" + Fax.Text + "%0AADR%3A%3B%3BA-203%3B" + City.Text + "%3B" + Region.Text + "%3B500078%3B" + Country.Text + "%0AEND%3AVCARD";
            Change();
        }
         * */
        #region Logical page navigation

        // The split page isdesigned so that when the Window does have enough space to show
        // both the list and the dteails, only one pane will be shown at at time.
        //
        // This is all implemented with a single physical page that can represent two logical
        // pages.  The code below achieves this goal without making the user aware of the
        // distinction.

        private const int MinimumWidthForSupportingTwoPanes = 768;

        /// <summary>
        /// Invoked to determine whether the page should act as one logical page or two.
        /// </summary>
        /// <returns>True if the window should show act as one logical page, false
        /// otherwise.</returns>
        private bool UsingLogicalPageNavigation()
        {
            return Window.Current.Bounds.Width < MinimumWidthForSupportingTwoPanes;
        }

        /// <summary>
        /// Invoked with the Window changes size
        /// </summary>
        /// <param name="sender">The current Window</param>
        /// <param name="e">Event data that describes the new size of the Window</param>
        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.InvalidateVisualState();
            var rect = Window.Current.Bounds;
            if (rect.Width < 640) 
            {
                st1.Width = rect.Width - 240; Freetext.Width = rect.Width - 260;
                st2.Width = rect.Width - 240; Freetext2.Width = rect.Width - 260;
                st3.Width = rect.Width - 240; st32.Width = rect.Width - 357;
                st4.Width = rect.Width - 240; Freetext4.Width = rect.Width - 260;
                st5.Width = rect.Width - 240; Message.Width = rect.Width - 260; PhoneNumber.Width = rect.Width - 260;
                
            }
            else
            { 
                st1.Width = 400; Freetext.Width = 380;
                st2.Width = 470; Freetext2.Width = 450;
                st3.Width = 421; st32.Width = 304;
                st4.Width = 400; Freetext4.Width = 380;
                st5.Width = 407; Message.Width = 387; PhoneNumber.Width = 387;
                
            }
            if (rect.Width < 843)
            {
                VisualStateManager.GoToState(this, "SinglePane_Detail", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "PrimaryView", false);
            }
        }

        /// <summary>
        /// Invoked when an item within the list is selected.
        /// </summary>
        /// <param name="sender">The GridView displaying the selected item.</param>
        /// <param name="e">Event data that describes how the selection was changed.</param>
        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Invalidate the view state when logical page navigation is in effect, as a change
            // in selection may cause a corresponding change in the current logical page.  When
            // an item is selected this has the effect of changing from displaying the item list
            // to showing the selected item's details.  When the selection is cleared this has the
            // opposite effect.
            if (this.UsingLogicalPageNavigation()) this.InvalidateVisualState();
        }

        private bool CanGoBack()
        {
            if (this.UsingLogicalPageNavigation() && this.itemListView.SelectedItem != null)
            {
                return true;
            }
            else
            {
                return this.navigationHelper.CanGoBack();
            }
        }
        private void GoBack()
        {
            if (this.UsingLogicalPageNavigation() && this.itemListView.SelectedItem != null)
            {
                // When logical page navigation is in effect and there's a selected item that
                // item's details are currently displayed.  Clearing the selection will return to
                // the item list.  From the user's point of view this is a logical backward
                // navigation.
                this.itemListView.SelectedItem = null;
            }
            else
            {
                this.navigationHelper.GoBack();
            }
        }

        private void InvalidateVisualState()
        {
            var visualState = DetermineVisualState();
            VisualStateManager.GoToState(this, visualState, false);
            this.navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Invoked to determine the name of the visual state that corresponds to an application
        /// view state.
        /// </summary>
        /// <returns>The name of the desired visual state.  This is the same as the name of the
        /// view state except when there is a selected item in portrait and snapped views where
        /// this additional logical page is represented by adding a suffix of _Detail.</returns>
        private string DetermineVisualState()
        {
            if (!UsingLogicalPageNavigation())
                return "PrimaryView";

            // Update the back button's enabled state when the view state changes
            var logicalPageBack = this.UsingLogicalPageNavigation() && this.itemListView.SelectedItem != null;

            return logicalPageBack ? "SinglePane_Detail" : "SinglePane";
        }

        #endregion

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
            Save.IsEnabled = false; Customize.IsEnabled = false;
            itemListView.SelectedIndex = SplitPage0.selection;
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await SaveImageAsync();
        }
        public static string s = ""; public static int qselect = 0;
        private void Customize_Click(object sender, RoutedEventArgs e)
        {
            if (s.Equals("")) { ShowPopupAnimationClicked("Enter a value to generate the QR-Code."+Environment.NewLine+"Generate a QR-code before customizing it."); return; }
            if (itemListView.SelectedIndex == 0) { qselect = 0; s = (Freetext.Text); }
            else if (itemListView.SelectedIndex == 1) { qselect = 1; s = (Freetext2.Text); }
            else if (itemListView.SelectedIndex == 2) { qselect = 2; s = ("BEGIN:VCARD\nVERSION:3.0\nN:+" + Name.Text + ";" + FirstName.Text + "\nORG:" + Organisation.Text + "\nEMAIL;TYPE=INTERNET:" + Email.Text + "\nURL:" + Website.Text + "\nTEL;TYPE=CELL:" + Cell.Text + "\nTEL:" + Phone.Text + "\nTEL;TYPE=FAX:" + Fax.Text + "\nADR:" + Street.Text + ";" + City.Text + ";" + Region.Text + ";" + PostCode.Text + ";" + Country.Text + "\nEND:VCARD"); }
            else if (itemListView.SelectedIndex == 3) { qselect = 3; s = ("tel:" + Freetext4.Text); }
            else if (itemListView.SelectedIndex == 4) { qselect = 4; s = ("SMSTO:" + PhoneNumber.Text + ":" + Message.Text); }
            this.Frame.Navigate(typeof(Customize), s);
        }
        private void generate()
        {
            if (itemListView.SelectedIndex == 0) GenerateQRCode(Freetext.Text);
            else if (itemListView.SelectedIndex == 1) GenerateQRCode(Freetext2.Text);
            else if (itemListView.SelectedIndex == 2) GenerateQRCode("BEGIN:VCARD\nVERSION:3.0\nN:+" + Name.Text + ";" + FirstName.Text + "\nORG:" + Organisation.Text + "\nEMAIL;TYPE=INTERNET:" + Email.Text + "\nURL:" + Website.Text + "\nTEL;TYPE=CELL:" + Cell.Text + "\nTEL:" + Phone.Text + "\nTEL;TYPE=FAX:" + Fax.Text + "\nADR:" + Street.Text + ";" + City.Text + ";" + Region.Text + ";" + PostCode.Text + ";" + Country.Text + "\nEND:VCARD");
            else if (itemListView.SelectedIndex == 3) GenerateQRCode("tel:" + Freetext4.Text);
            else if (itemListView.SelectedIndex == 4) GenerateQRCode("SMSTO:" + PhoneNumber.Text + ":" + Message.Text);
        }
        WriteableBitmap bmp;
        private void GenerateQRCode(string s)
        {
            SplitPage1.s = s;
            if (s.Equals("")) { Save.IsEnabled = false; Customize.IsEnabled = false; return; }
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
        }
        private void Freetext_TextChanged(object sender, TextChangedEventArgs e)
        {
            Save.IsEnabled = true; Customize.IsEnabled = true;
            generate();
        }
        
        private async Task<bool> SaveImageAsync()
        {
            String filename = "QR_";
            if (itemListView.SelectedIndex == 0) filename += "FreeText";
            else if (itemListView.SelectedIndex == 1) filename += "URL";
            else if (itemListView.SelectedIndex == 2) filename += "Contact";
            else if (itemListView.SelectedIndex == 3) filename += "Phone";
            else if (itemListView.SelectedIndex == 4) filename += "SMS";
            filename += bmp.PixelHeight + "x" + bmp.PixelWidth;
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
            ShowPopupAnimationClicked("Image Saved" + Environment.NewLine + "Pictures//" + filename + ".png");
            return true;
        }
        
    }
}

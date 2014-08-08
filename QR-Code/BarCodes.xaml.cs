using QR_Code.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class BarCodes : Page
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
        WriteableBitmap bmp = null;
        
        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public BarCodes()
        {
            this.InitializeComponent();

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
            switch (itemListView.SelectedIndex)
            {

                case 0:
                    itemTitle.Text = "CODABAR";
                    FreeTitle.Text = "CODABAR";
                    cbarText.Visibility = Visibility.Visible;
                    c128Text.Visibility = Visibility.Collapsed;
                    c39Text.Visibility = Visibility.Collapsed;
                    ean13Text.Visibility = Visibility.Collapsed;
                    ean8Text.Visibility = Visibility.Collapsed;
                    itfText.Visibility = Visibility.Collapsed;
                    msiText.Visibility = Visibility.Collapsed;
                    upcaText.Visibility = Visibility.Collapsed;
                    plsText.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    itemTitle.Text = "CODE_128";
                    FreeTitle.Text = "CODE_128";
                    c128Text.Visibility = Visibility.Visible;
                    cbarText.Visibility = Visibility.Collapsed;
                    c39Text.Visibility = Visibility.Collapsed;
                    ean13Text.Visibility = Visibility.Collapsed;
                    ean8Text.Visibility = Visibility.Collapsed;
                    itfText.Visibility = Visibility.Collapsed;
                    msiText.Visibility = Visibility.Collapsed;
                    upcaText.Visibility = Visibility.Collapsed;
                    plsText.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    itemTitle.Text = "CODE_39";
                    FreeTitle.Text = "CODE_39";
                    c39Text.Visibility = Visibility.Visible;
                    c128Text.Visibility = Visibility.Collapsed;
                    cbarText.Visibility = Visibility.Collapsed;
                    ean13Text.Visibility = Visibility.Collapsed;
                    ean8Text.Visibility = Visibility.Collapsed;
                    itfText.Visibility = Visibility.Collapsed;
                    msiText.Visibility = Visibility.Collapsed;
                    upcaText.Visibility = Visibility.Collapsed;
                    plsText.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    itemTitle.Text = "EAN_13";
                    FreeTitle.Text = "EAN_13";
                    ean13Text.Visibility = Visibility.Visible;
                    cbarText.Visibility = Visibility.Collapsed;
                    c128Text.Visibility = Visibility.Collapsed;
                    c39Text.Visibility = Visibility.Collapsed;
                    ean8Text.Visibility = Visibility.Collapsed;
                    itfText.Visibility = Visibility.Collapsed;
                    msiText.Visibility = Visibility.Collapsed;
                    upcaText.Visibility = Visibility.Collapsed;
                    plsText.Visibility = Visibility.Collapsed;
                    break;
                case 4:
                    itemTitle.Text = "EAN_8";
                    FreeTitle.Text = "EAN_8";
                    ean8Text.Visibility = Visibility.Visible;
                    cbarText.Visibility = Visibility.Collapsed;
                    c128Text.Visibility = Visibility.Collapsed;
                    c39Text.Visibility = Visibility.Collapsed;
                    ean13Text.Visibility = Visibility.Collapsed;
                    itfText.Visibility = Visibility.Collapsed;
                    msiText.Visibility = Visibility.Collapsed;
                    upcaText.Visibility = Visibility.Collapsed;
                    plsText.Visibility = Visibility.Collapsed;
                    break;
                case 5:
                    itemTitle.Text = "ITF";
                    FreeTitle.Text = "ITF";
                    itfText.Visibility = Visibility.Visible;
                    cbarText.Visibility = Visibility.Collapsed;
                    c128Text.Visibility = Visibility.Collapsed;
                    c39Text.Visibility = Visibility.Collapsed;
                    ean13Text.Visibility = Visibility.Collapsed;
                    ean8Text.Visibility = Visibility.Collapsed;
                    msiText.Visibility = Visibility.Collapsed;
                    upcaText.Visibility = Visibility.Collapsed;
                    plsText.Visibility = Visibility.Collapsed;
                    break;
                case 6:
                    itemTitle.Text = "MSI";
                    FreeTitle.Text = "MSI";
                    msiText.Visibility = Visibility.Visible;
                    cbarText.Visibility = Visibility.Collapsed;
                    c128Text.Visibility = Visibility.Collapsed;
                    c39Text.Visibility = Visibility.Collapsed;
                    ean13Text.Visibility = Visibility.Collapsed;
                    ean8Text.Visibility = Visibility.Collapsed;
                    itfText.Visibility = Visibility.Collapsed;
                    upcaText.Visibility = Visibility.Collapsed;
                    plsText.Visibility = Visibility.Collapsed;break;
                case 7:
                    itemTitle.Text = "UPC_A";
                    FreeTitle.Text = "UPC_A";
                    upcaText.Visibility = Visibility.Visible;
                    cbarText.Visibility = Visibility.Collapsed;
                    c128Text.Visibility = Visibility.Collapsed;
                    c39Text.Visibility = Visibility.Collapsed;
                    ean13Text.Visibility = Visibility.Collapsed;
                    ean8Text.Visibility = Visibility.Collapsed;
                    itfText.Visibility = Visibility.Collapsed;
                    msiText.Visibility = Visibility.Collapsed;
                   plsText.Visibility = Visibility.Collapsed;
                    break;
                case 8:
                    itemTitle.Text = "PLESSEY";
                    FreeTitle.Text = "PLESSEY";
                    plsText.Visibility = Visibility.Visible;
                    cbarText.Visibility = Visibility.Collapsed;
                    c128Text.Visibility = Visibility.Collapsed;
                    c39Text.Visibility = Visibility.Collapsed;
                    ean13Text.Visibility = Visibility.Collapsed;
                    ean8Text.Visibility = Visibility.Collapsed;
                    itfText.Visibility = Visibility.Collapsed;
                    msiText.Visibility = Visibility.Collapsed;
                    upcaText.Visibility = Visibility.Collapsed;
                    break;
            }
            if (this.UsingLogicalPageNavigation())
            {
                this.navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
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
                //       pageState("SelectedItem")

            }
        }
        
       
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
            Save.IsEnabled = false; Customize.IsEnabled = false;
            itemListView.SelectedIndex = SplitPage0.selection - 1;
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (BarCodes.str.Equals("")) { Save.IsEnabled = false; Customize.IsEnabled = false; return; }
            string str = "";
            switch (itemListView.SelectedIndex)
            {
                case 0:
                    str = "CODABAR";
                    break;
                case 1:
                    str = "CODE_128";
                    break;
                case 2:
                    str = "CODE_39";
                    break;
                case 3:
                    str = "EAN_13";
                    break;
                case 4:
                    str = "EAN_8";
                    break;
                case 5:
                    str = "ITF";
                    break;
                case 6:
                    str = "MSI";
                    break;
                case 7:
                    str = "UPC_A";
                    break;
                case 8:
                    str = "PLESSEY";
                    break;
            }
            str += bmp.PixelHeight + "x" + bmp.PixelWidth;
            await SaveImage.Save(str, bmp);
            ShowPopupAnimationClicked("Image Saved" + Environment.NewLine + "Pictures//" + str + ".png");

        }
        public static string str = ""; public static int qselect = 0;
        private void Customize_Click(object sender, RoutedEventArgs e)
        {
            if (str.Equals("")) { ShowPopupAnimationClicked("Enter a value to generate the barcode."+Environment.NewLine+ "Generate a barcode before customizing it."); return; }
            qselect = itemListView.SelectedIndex;
            if (qselect == 5 && str.Length % 2 == 1) str = str.Substring(0, str.Length - 1);
            this.Frame.Navigate(typeof(Customize_Barcodes));
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
       
        private void GenerateQRCode()
        {
            BarcodeFormat format = BarcodeFormat.EAN_13;
            switch (itemListView.SelectedIndex)
            {
                case 0:
                    str = "*" + cbarText.Text + "*";
                    format = BarcodeFormat.CODABAR;
                    break;
                case 1:
                    str = c128Text.Text;
                    format = BarcodeFormat.CODE_128;
                    break;
                case 2:
                    str = c39Text.Text.ToUpper();
                    format = BarcodeFormat.CODE_39;
                    break;
                case 3:
                    str = ean13Text.Text;
                    if (str.Length < 12) return;
                    format = BarcodeFormat.EAN_13;
                    break;
                case 4:
                    str = ean8Text.Text;
                    if (str.Length < 7) return;
                    format = BarcodeFormat.EAN_8;
                    break;
                case 5:
                    str = itfText.Text;
                    if (str.Length % 2 == 1) return;
                    format = BarcodeFormat.ITF;
                    break;
                case 6:
                    str = msiText.Text;
                    format = BarcodeFormat.MSI;
                    break;
                case 7:
                    str = upcaText.Text;
                    if (str.Length < 11) return;
                    format = BarcodeFormat.UPC_A;
                    break;
                case 8:
                    str = plsText.Text;
                    format = BarcodeFormat.PLESSEY;
                    break;
            }
            try
            {
                bmp = BarCode.Generate(str, format, 300, 400, Windows.UI.Colors.Black, Windows.UI.Colors.White);
                if (bmp != (null))
                {
                    Image.Source = bmp;
                }
                else
                {
                    str = str.Substring(0, str.Length - 1);
                    switch (itemListView.SelectedIndex)
                    {

                        case 0:
                            cbarText.Text = str;
                            break;
                        case 1:
                            c128Text.Text = str;
                            break;
                        case 2:
                            c39Text.Text = str;
                            break;
                        case 3:
                            ean13Text.Text = str;
                            break;
                        case 4:
                            ean8Text.Text = str;
                            break;
                        case 5:
                            itfText.Text = str;
                            break;
                        case 6:
                            msiText.Text = str;
                            break;
                        case 7:
                            upcaText.Text = str;
                            break;
                        case 8:
                            plsText.Text = str;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                cbarText.Text = "";
                c39Text.Text = "";
                c128Text.Text = "";
                upcaText.Text = "";
                msiText.Text = "";
                itfText.Text = "";
                ean13Text.Text = "";
                ean8Text.Text = "";
                plsText.Text = "";
            }
        }
        private void Freetext_TextChanged(object sender, TextChangedEventArgs e)
        {
            Save.IsEnabled = true; Customize.IsEnabled = true;
            GenerateQRCode();
        }
        
    }
}

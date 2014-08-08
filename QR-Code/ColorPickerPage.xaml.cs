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
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace QR_Code
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class ColorPickerPage : Page
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


        public ColorPickerPage()
        {
            this.InitializeComponent();
        }
        static string[] colorNames =
{
	"AliceBlue","AntiqueWhite","Aqua","Aquamarine","Azure",
    "Beige","Bisque","Black","BlanchedAlmond","Blue","BlueViolet","Brown","BurlyWood",
    "CadetBlue","Chartreuse","Chocolate","Coral","CornflowerBlue","Cornsilk","Crimson","Cyan",
     "DarkBlue","DarkCyan","DarkGoldenrod","DarkGray","DarkGreen","DarkKhaki","DarkMagenta",
        "DarkOliveGreen","DarkOrange","DarkOrchid","DarkRed","DarkSalmon","DarkSeaGreen","DarkSlateBlue",
        "DarkSlateGray","DarkTurquoise","DarkViolet","DeepPink","DeepSkyBlue","DimGray","DodgerBlue",
        "Firebrick","FloralWhite","ForestGreen","Fuchsia","Gainsboro","GhostWhite","Gold","Goldenrod","Gray",
        "Green","GreenYellow","Honeydew","HotPink","IndianRed","Indigo","Ivory","Khaki","Lavender","LavenderBlush",
        "LawnGreen","LemonChiffon","LightBlue","LightCoral","LightCyan","LightGoldenrodYellow","LightGray","LightGreen",
        "LightPink","LightSalmon","LightSeaGreen","LightSkyBlue","LightSlateGray","LightSteelBlue","LightYellow","Lime",
        "LimeGreen","Linen","Magenta","Maroon","MediumAquamarine","MediumBlue","MediumOrchid","MediumPurple","MediumSeaGreen",
        "MediumSlateBlue","MediumSpringGreen","MediumTurquoise","MediumVioletRed","MidnightBlue","MintCream","MistyRose",
        "Moccasin","NavajoWhite","Navy","OldLace","Olive","OliveDrab","Orange","OrangeRed","Orchid","PaleGoldenrod","PaleGreen", 
        "PaleTurquoise",  "PaleVioletRed", "PapayaWhip", "PeachPuff",  "Peru", "Pink", "Plum", "PowderBlue","Purple","Red", 
        "RosyBrown", "RoyalBlue","SaddleBrown", "Salmon",  "SandyBrown", "SeaGreen", "SeaShell",  "Sienna",  "Silver", 
        "SkyBlue",  "SlateBlue",  "SlateGray", "Snow","SpringGreen","SteelBlue", "Tan", "Teal", "Thistle", "Tomato", 
        "Transparent","Turquoise","Violet","Wheat","White","WhiteSmoke", "Yellow","YellowGreen"
};
        public static Windows.UI.Color foreground = Windows.UI.Colors.Black, background = Windows.UI.Colors.White;
        public static string foretext = "Black", backtext = "White";
        string select = "";
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            select = e.Parameter.ToString();
            List<Colors> list = new List<Colors>();
            for (int i = 0; i < colorNames.Length; i++)
            {
                Colors ob = new Colors();
                ob.Color = colorNames[i];
                list.Add(ob);
            }
            listBox.ItemsSource = list;

        }
        private Windows.UI.Color ColorName(string s)
        {
            switch (s)
            {
                case "AliceBlue": return Windows.UI.Colors.AliceBlue;
                case "AntiqueWhite": return Windows.UI.Colors.AntiqueWhite;
                case "Aqua": return Windows.UI.Colors.Aqua;
                case "Aquamarine": return Windows.UI.Colors.Aquamarine;
                case "Azure": return Windows.UI.Colors.Azure;
                case "Beige": return Windows.UI.Colors.Beige;
                case "Bisque": return Windows.UI.Colors.Bisque;
                case "Black": return Windows.UI.Colors.Black;
                case "BlanchedAlmond": return Windows.UI.Colors.BlanchedAlmond;
                case "Blue": return Windows.UI.Colors.Blue;
                case "BlueViolet": return Windows.UI.Colors.BlueViolet;
                case "Brown": return Windows.UI.Colors.Brown;
                case "BurlyWood": return Windows.UI.Colors.BurlyWood;
                case "CadetBlue": return Windows.UI.Colors.CadetBlue;
                case "Chartreuse": return Windows.UI.Colors.Chartreuse;
                case "Chocolate": return Windows.UI.Colors.Chocolate;
                case "Coral": return Windows.UI.Colors.Coral;
                case "CornflowerBlue": return Windows.UI.Colors.CornflowerBlue;
                case "Cornsilk": return Windows.UI.Colors.Cornsilk;
                case "Crimson": return Windows.UI.Colors.Crimson;
                case "Cyan": return Windows.UI.Colors.Cyan;
                case "DarkBlue": return Windows.UI.Colors.DarkBlue;
                case "DarkCyan": return Windows.UI.Colors.DarkCyan;
                case "DarkGoldenrod": return Windows.UI.Colors.DarkGoldenrod;
                case "DarkGray": return Windows.UI.Colors.DarkGray;
                case "DarkGreen": return Windows.UI.Colors.DarkGreen;
                case "DarkKhaki": return Windows.UI.Colors.DarkKhaki;
                case "DarkMagenta": return Windows.UI.Colors.DarkMagenta;
                case "DarkOliveGreen": return Windows.UI.Colors.DarkOliveGreen;
                case "DarkOrange": return Windows.UI.Colors.DarkOrange;
                case "DarkOrchid": return Windows.UI.Colors.DarkOrchid;
                case "DarkRed": return Windows.UI.Colors.DarkRed;
                case "DarkSalmon": return Windows.UI.Colors.DarkSalmon;
                case "DarkSeaGreen": return Windows.UI.Colors.DarkSeaGreen;
                case "DarkSlateBlue": return Windows.UI.Colors.DarkSlateBlue;
                case "DarkSlateGray": return Windows.UI.Colors.DarkSlateGray;
                case "DarkTurquoise": return Windows.UI.Colors.DarkTurquoise;
                case "DarkViolet": return Windows.UI.Colors.DarkViolet;
                case "DeepPink": return Windows.UI.Colors.DeepPink;
                case "DeepSkyBlue": return Windows.UI.Colors.DeepSkyBlue;
                case "DimGray": return Windows.UI.Colors.DimGray;
                case "DodgerBlue": return Windows.UI.Colors.DodgerBlue;
                case "Firebrick": return Windows.UI.Colors.Firebrick;
                case "FloralWhite": return Windows.UI.Colors.FloralWhite;
                case "ForestGreen": return Windows.UI.Colors.ForestGreen;
                case "Fuchsia": return Windows.UI.Colors.Fuchsia;
                case "Gainsboro": return Windows.UI.Colors.Gainsboro;
                case "GhostWhite": return Windows.UI.Colors.GhostWhite;
                case "Gold": return Windows.UI.Colors.Gold;
                case "Goldenrod": return Windows.UI.Colors.Goldenrod;
                case "Gray": return Windows.UI.Colors.Gray;
                case "Green": return Windows.UI.Colors.Green;
                case "GreenYellow": return Windows.UI.Colors.GreenYellow;
                case "Honeydew": return Windows.UI.Colors.Honeydew;
                case "HotPink": return Windows.UI.Colors.HotPink;
                case "IndianRed": return Windows.UI.Colors.IndianRed;
                case "Indigo": return Windows.UI.Colors.Indigo;
                case "Ivory": return Windows.UI.Colors.Ivory;
                case "Khaki": return Windows.UI.Colors.Khaki;
                case "Lavender": return Windows.UI.Colors.Lavender;
                case "LavenderBlush": return Windows.UI.Colors.LavenderBlush;
                case "LawnGreen": return Windows.UI.Colors.LawnGreen;
                case "LemonChiffon": return Windows.UI.Colors.LemonChiffon;
                case "LightBlue": return Windows.UI.Colors.LightBlue;
                case "LightCoral": return Windows.UI.Colors.LightCoral;
                case "LightCyan": return Windows.UI.Colors.LightCyan;
                case "LightGoldenrodYellow": return Windows.UI.Colors.LightGoldenrodYellow;
                case "LightGray": return Windows.UI.Colors.LightGray;
                case "LightGreen": return Windows.UI.Colors.LightGreen;
                case "LightPink": return Windows.UI.Colors.LightPink;
                case "LightSalmon": return Windows.UI.Colors.LightSalmon;
                case "LightSeaGreen": return Windows.UI.Colors.LightSeaGreen;
                case "LightSkyBlue": return Windows.UI.Colors.LightSkyBlue;
                case "LightSlateGray": return Windows.UI.Colors.LightSlateGray;
                case "LightSteelBlue": return Windows.UI.Colors.LightSteelBlue;
                case "LightYellow": return Windows.UI.Colors.LightYellow;
                case "Lime": return Windows.UI.Colors.Lime;
                case "LimeGreen": return Windows.UI.Colors.LimeGreen;
                case "Linen": return Windows.UI.Colors.Linen;
                case "Magenta": return Windows.UI.Colors.Magenta;
                case "Maroon": return Windows.UI.Colors.Maroon;
                case "MediumAquamarine": return Windows.UI.Colors.MediumAquamarine;
                case "MediumBlue": return Windows.UI.Colors.MediumBlue;
                case "MediumOrchid": return Windows.UI.Colors.MediumOrchid;
                case "MediumPurple": return Windows.UI.Colors.MediumPurple;
                case "MediumSeaGreen": return Windows.UI.Colors.MediumSeaGreen;
                case "MediumSlateBlue": return Windows.UI.Colors.MediumSlateBlue;
                case "MediumSpringGreen": return Windows.UI.Colors.MediumSpringGreen;
                case "MediumTurquoise": return Windows.UI.Colors.MediumTurquoise;
                case "MediumVioletRed": return Windows.UI.Colors.MediumVioletRed;
                case "MidnightBlue": return Windows.UI.Colors.MidnightBlue;
                case "MintCream": return Windows.UI.Colors.MintCream;
                case "MistyRose": return Windows.UI.Colors.MistyRose;
                case "Moccasin": return Windows.UI.Colors.Moccasin;
                case "NavajoWhite": return Windows.UI.Colors.NavajoWhite;
                case "Navy": return Windows.UI.Colors.Navy;
                case "OldLace": return Windows.UI.Colors.OldLace;
                case "Olive": return Windows.UI.Colors.Olive;
                case "OliveDrab": return Windows.UI.Colors.OliveDrab;
                case "Orange": return Windows.UI.Colors.Orange;
                case "OrangeRed": return Windows.UI.Colors.OrangeRed;
                case "Orchid": return Windows.UI.Colors.Orchid;
                case "PaleGoldenrod": return Windows.UI.Colors.PaleGoldenrod;
                case "PaleGreen": return Windows.UI.Colors.PaleGreen;
                case "PaleTurquoise": return Windows.UI.Colors.PaleTurquoise;
                case "PaleVioletRed": return Windows.UI.Colors.PaleVioletRed;
                case "PapayaWhip": return Windows.UI.Colors.PapayaWhip;
                case "PeachPuff": return Windows.UI.Colors.PeachPuff;
                case "Peru": return Windows.UI.Colors.Peru;
                case "Pink": return Windows.UI.Colors.Pink;
                case "Plum": return Windows.UI.Colors.Plum;
                case "PowderBlue": return Windows.UI.Colors.PowderBlue;
                case "Purple": return Windows.UI.Colors.Purple;
                case "Red": return Windows.UI.Colors.Red;
                case "RosyBrown": return Windows.UI.Colors.RosyBrown;
                case "RoyalBlue": return Windows.UI.Colors.RoyalBlue;
                case "SaddleBrown": return Windows.UI.Colors.SaddleBrown;
                case "Salmon": return Windows.UI.Colors.Salmon;
                case "SandyBrown": return Windows.UI.Colors.SandyBrown;
                case "SeaGreen": return Windows.UI.Colors.SeaGreen;
                case "SeaShell": return Windows.UI.Colors.SeaShell;
                case "Sienna": return Windows.UI.Colors.Sienna;
                case "Silver": return Windows.UI.Colors.Silver;
                case "SkyBlue": return Windows.UI.Colors.SkyBlue;
                case "SlateBlue": return Windows.UI.Colors.SlateBlue;
                case "SlateGray": return Windows.UI.Colors.SlateGray;
                case "Snow": return Windows.UI.Colors.Snow;
                case "SpringGreen": return Windows.UI.Colors.SpringGreen;
                case "SteelBlue": return Windows.UI.Colors.SteelBlue;
                case "Tan": return Windows.UI.Colors.Tan;
                case "Teal": return Windows.UI.Colors.Teal;
                case "Thistle": return Windows.UI.Colors.Thistle;
                case "Tomato": return Windows.UI.Colors.Tomato;
                case "Transparent": return Windows.UI.Colors.Transparent;
                case "Turquoise": return Windows.UI.Colors.Turquoise;
                case "Violet": return Windows.UI.Colors.Violet;
                case "Wheat": return Windows.UI.Colors.Wheat;
                case "White": return Windows.UI.Colors.White;
                case "WhiteSmoke": return Windows.UI.Colors.WhiteSmoke;
                case "Yellow": return Windows.UI.Colors.Yellow;
                case "YellowGreen": return Windows.UI.Colors.YellowGreen;
            }
            return Windows.UI.Colors.Black;
        }
        private void lstColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // MessageDialogHelper.Show(colorNames[listBox.SelectedIndex], "Color");
            if (select.Equals("Foreground")) { foreground = ColorName(colorNames[listBox.SelectedIndex]); foretext = colorNames[listBox.SelectedIndex]; }
            else { background = ColorName(colorNames[listBox.SelectedIndex]); backtext = colorNames[listBox.SelectedIndex]; }
            this.Frame.GoBack();
        }
    }
}
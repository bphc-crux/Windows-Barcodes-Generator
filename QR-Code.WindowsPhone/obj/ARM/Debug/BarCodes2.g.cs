﻿

#pragma checksum "C:\Users\Srujan Jha\Documents\Visual Studio 2013\Projects\Windows8\QR-Code\QR-Code.WindowsPhone\BarCodes2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C5C82E24ED3AF177F431F34834D56550"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QR_Code
{
    partial class BarCodes2 : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 32 "..\..\..\BarCodes2.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Image_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 37 "..\..\..\BarCodes2.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarButton_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 29 "..\..\..\BarCodes2.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.Freetext_TextChanged;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 26 "..\..\..\BarCodes2.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.Freetext_TextChanged;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 46 "..\..\..\BarCodes2.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Save_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 47 "..\..\..\BarCodes2.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Customize_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



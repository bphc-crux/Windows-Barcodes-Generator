﻿

#pragma checksum "C:\Users\Srujan Jha\Documents\Visual Studio 2013\Projects\Windows8\QR-Code\QR-Code.WindowsPhone\BasicPage2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AFD0A9B9C35612DC1FF0BAFC25686E2E"
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
    partial class BasicPage2 : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 56 "..\..\..\BasicPage2.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Checked += this.rcustom_Checked;
                 #line default
                 #line hidden
                #line 56 "..\..\..\BasicPage2.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Unchecked += this.rcustom_Unchecked;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 61 "..\..\..\BasicPage2.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Save_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}



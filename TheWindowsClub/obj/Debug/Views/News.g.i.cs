﻿#pragma checksum "G:\Users\Manish\Documents\Visual Studio 2012\Projects\TheWindowsClub\TheWindowsClub\Views\News.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "133AB0D973C8EDCC4188039A3AF5D4D9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace TheWindowsClub {
    
    
    public partial class News : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Controls.PhoneApplicationPage usercontro;
        
        internal System.Windows.Media.Animation.Storyboard Str_NewsScreenLoads;
        
        internal System.Windows.Media.Animation.Storyboard LoadedStart;
        
        internal System.Windows.Media.PlaneProjection UsrControlPlaneProjection;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.Pivot pivot;
        
        internal Coding4Fun.Toolkit.Controls.TileNotification tileNotification;
        
        internal System.Windows.Controls.Button On_Web;
        
        internal System.Windows.Controls.Button Share;
        
        internal System.Windows.Controls.Button mailBtn;
        
        internal System.Windows.Controls.Button MsgBtn;
        
        internal System.Windows.Controls.Button logoBtn;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/TheWindowsClub;component/Views/News.xaml", System.UriKind.Relative));
            this.usercontro = ((Microsoft.Phone.Controls.PhoneApplicationPage)(this.FindName("usercontro")));
            this.Str_NewsScreenLoads = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Str_NewsScreenLoads")));
            this.LoadedStart = ((System.Windows.Media.Animation.Storyboard)(this.FindName("LoadedStart")));
            this.UsrControlPlaneProjection = ((System.Windows.Media.PlaneProjection)(this.FindName("UsrControlPlaneProjection")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.pivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("pivot")));
            this.tileNotification = ((Coding4Fun.Toolkit.Controls.TileNotification)(this.FindName("tileNotification")));
            this.On_Web = ((System.Windows.Controls.Button)(this.FindName("On_Web")));
            this.Share = ((System.Windows.Controls.Button)(this.FindName("Share")));
            this.mailBtn = ((System.Windows.Controls.Button)(this.FindName("mailBtn")));
            this.MsgBtn = ((System.Windows.Controls.Button)(this.FindName("MsgBtn")));
            this.logoBtn = ((System.Windows.Controls.Button)(this.FindName("logoBtn")));
        }
    }
}


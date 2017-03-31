using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace TheWindowsClub
{
    public partial class AboutTWC : UserControl
    {
        public AboutTWC()
        {
            InitializeComponent();
        }

        public event EventHandler AboutPage;
        public void RaiseAboutPage()
        {
            if (AboutPage != null)
                AboutPage(this, new EventArgs());
        }

        private void WcImg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            WebBrowserTask wb = new WebBrowserTask() 
            {
                URL = "http://www.thewindowsclub.com/"
            };
            wb.Show();
        }

        private void CompImg_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            RaiseAboutPage();
        }

        private void Btn_FB_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WebBrowserTask wb = new WebBrowserTask() 
            {
                URL = "https://www.facebook.com/TheWindowsClub"
            };
            wb.Show();
        }

        private void Btn_Twt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WebBrowserTask wb = new WebBrowserTask()
            {
                URL = "https://twitter.com/TheWindowsClub"
            };
            wb.Show();
        }

        private void Btn_GP_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WebBrowserTask wb = new WebBrowserTask()
            {
                URL = "https://plus.google.com/103148237334381859790"
            };
            wb.Show();
        }

        private void Btn_In_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WebBrowserTask wb = new WebBrowserTask()
            {
                URL = "http://www.linkedin.com/groups/Windows-Club-1946790"
            };
            wb.Show();
        }
    }
}

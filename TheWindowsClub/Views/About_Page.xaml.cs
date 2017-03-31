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
    public partial class About_Page : PhoneApplicationPage
    {
        WebBrowserTask Task = null;
        public About_Page()
        {
            InitializeComponent();
            Btn_FB.Click += Btn_FB_Click;
            Btn_GP.Click += Btn_GP_Click;
            Btn_Wp.Click += Btn_Wp_Click;
        }

        void Btn_Wp_Click(object sender, RoutedEventArgs e)
        {
            Task = new WebBrowserTask() 
            {
                URL = "http://www.windowsphone.com/en-US/store/publishers?publisherId=Googlert&appId=a302a617-8af4-4189-a673-9a0e7344e4bf"
            };
            Task.Show();
        }

        void Btn_GP_Click(object sender, RoutedEventArgs e)
        {
            Task = new WebBrowserTask() {
                URL = "https://plus.google.com/u/0/b/111013302558351195393/111013302558351195393"
            };
            Task.Show();

        }

        void Btn_FB_Click(object sender, RoutedEventArgs e)
        {
            Task = new WebBrowserTask()
            {
                URL = "https://www.facebook.com/Googlert"
            };
            Task.Show();
        }
    }
}
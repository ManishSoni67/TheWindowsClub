﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TheWindowsClub
{
    public partial class SecurityFeedNews : UserControl
    {
        public SecurityFeedNews()
        {
            InitializeComponent();
            foreach (var tile in ContentPanel.Children)
            {
                if (tile is HubTile)
                {
                    (tile as HubTile).Tap += SecurityFeedNews_Tap;
                }
            }
        }
        public SecurityFeedNewsVM Model
        {
            get
            {
                return (SecurityFeedNewsVM)this.DataContext;
            }
        }
        public event EventHandler<RssContentEventArgs> ShowNews;
       
        void SecurityFeedNews_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var tile = sender as HubTile;

            var Data = Model.AllRssData.SingleOrDefault(x => x.Title.CompareTo(tile.Title) == 0);
            if (Data != null)
            {
                string loc = "/Views/News.xaml?Id=" + Data.ID + "&intID=" + Data.IntID + "&type=" + type;
                ShowNews(this, new RssContentEventArgs(Data.ID, Data.IntID, type));
            }
        }

        public int type { get; set; }
    }
}

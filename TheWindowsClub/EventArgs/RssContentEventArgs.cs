using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace TheWindowsClub
{
   public class RssContentEventArgs:RoutedEventArgs
    {
        public string Title { get; set; }

        public int ID { get; set; }

        public int Type { get; set; }

        public RssContentEventArgs(string TiTle, int ID, int type)
        {
            this.Title = TiTle;
            this.ID = ID;
            this.Type = type;
        }
    }
}

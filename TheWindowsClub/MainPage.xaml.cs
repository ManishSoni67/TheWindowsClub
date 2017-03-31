using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;

namespace TheWindowsClub
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPageVM Model
        {
            get
            {
                return (MainPageVM)this.DataContext;

            }
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Model.LoadRss();
            LoadAllViews();
            LoadtoAllContainers();
            Model.NewsLoaded += Model_NewsLoaded;
            foreach (var tile in ContentPanel.Children)
            {
                if (tile is HubTile)
                {
                    (tile as HubTile).Tap += MainPage_Tap;
                }
            }
            Panorama.SelectionChanged += Panorama_SelectionChanged;

            this.Loaded += MainPage_Loaded;
        }

        void Model_NewsLoaded(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(this, "Hide", true);
        }

        private void LoadtoAllContainers()
        {
            WContainer.Child = Windowscontainer;
            SecContent.Child = SecurityNews;
            Downlcontainer.Child = DownLoadNews;
          //  LiveContainer.Child = LiveNews;
            IEContainer.Child = IENews;
            AboutCOntainer.Child = AboutWC;
        }

        private void LoadAllViews()
        {
            Windowscontainer = new WindowsFeedNews();
            Windowscontainer.ShowNews += Windowscontainer_ShowNews;
            Windowscontainer.type = 1;

            SecurityNews = new SecurityFeedNews();
            SecurityNews.ShowNews += SecurityNews_ShowNews;
            SecurityNews.type = 2;

            DownLoadNews = new DownLoadsFeedNews();
            DownLoadNews.ShowNews += DownLoadNews_ShowNews;
            DownLoadNews.type = 3;

            LiveNews = new LiveFeedNews();
            LiveNews.ShowNews += LiveNews_ShowNews;
            LiveNews.type = 4;

            IENews = new IEFeedNews();
            IENews.ShowNews += IENews_ShowNews;
            IENews.type = 5;

            AboutWC = new AboutTWC();
            AboutWC.AboutPage += AboutWC_AboutPage;
          
        }

        void AboutWC_AboutPage(object sender, EventArgs e)
        {
            string loc = "/Views/About_Page.xaml";
            NavigationService.Navigate(new Uri(loc, UriKind.Relative));
        }

        void IENews_ShowNews(object sender, RssContentEventArgs e)
        {
            string loc = "/Views/News.xaml?Id=" + e.Title + "&intID=" + e.ID + "&type=" + e.Type;
            NavigationService.Navigate(new Uri(loc, UriKind.Relative));
        }

        void LiveNews_ShowNews(object sender, RssContentEventArgs e)
        {
            string loc = "/Views/News.xaml?Id=" + e.Title + "&intID=" + e.ID + "&type=" + e.Type;
            NavigationService.Navigate(new Uri(loc, UriKind.Relative));
        }

        void DownLoadNews_ShowNews(object sender, RssContentEventArgs e)
        {
            string loc = "/Views/News.xaml?Id=" + e.Title + "&intID=" + e.ID + "&type=" + e.Type;
            NavigationService.Navigate(new Uri(loc, UriKind.Relative));
        }

        void SecurityNews_ShowNews(object sender, RssContentEventArgs e)
        {
            string loc = "/Views/News.xaml?Id=" + e.Title + "&intID=" + e.ID + "&type=" + e.Type;
            NavigationService.Navigate(new Uri(loc, UriKind.Relative));
        }

        void Windowscontainer_ShowNews(object sender, RssContentEventArgs e)
        {
            string loc = "/Views/News.xaml?Id=" + e.Title + "&intID=" + e.ID + "&type=" + e.Type;
            NavigationService.Navigate(new Uri(loc, UriKind.Relative));
        }

        void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Panorama.SelectedIndex == 0)
            {
                Model.LoadRss();
            }
            else if (Panorama.SelectedIndex == 1)
            {
                  Windowscontainer.Model.LoadRss();
            }
            else if (Panorama.SelectedIndex == 2)
            {
                SecurityNews.Model.LoadRss();
            }
            else if (Panorama.SelectedIndex == 3)
            {
                DownLoadNews.Model.LoadRss();
            }
            else if (Panorama.SelectedIndex == 4)
            {
                IENews.Model.LoadRss();
            }
           

        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Model.LoadRss();

        }

        void MainPage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (Model.Isbusy != true)
            {
                var tile = sender as HubTile;
                var Data = Model.AllRssData.SingleOrDefault(x => x.Title.CompareTo(tile.Title) == 0);
                if (Data != null)
                {
                    string loc = "/Views/News.xaml?Id=" + Data.ID + "&intID=" + Data.IntID + "&type=" + Panorama.SelectedIndex;
                    NavigationService.Navigate(new Uri(loc, UriKind.Relative));

                }
            }
        }

        private void ApplicationBarIconButton_Click_4(object sender, System.EventArgs e)
        {
            Model.LoadRss();
        }

        public AboutTWC AboutWC { get; set; }
        public WindowsFeedNews Windowscontainer { get; set; }
        public LiveFeedNews LiveNews { get; set; }
        public DownLoadsFeedNews DownLoadNews { get; set; }
        public SecurityFeedNews SecurityNews { get; set; }
        public IEFeedNews IENews { get; set; }

      
        private void ApplicationBarMenuItem_Click_1(object sender, System.EventArgs e)
        {
            string loc = "/Views/About_Page.xaml";
            NavigationService.Navigate(new Uri(loc, UriKind.Relative));
        }
    }
}
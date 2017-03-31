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
using System.IO;
using System.IO.IsolatedStorage;
using WindowsClub.Models;
using System.Windows.Media.Imaging;

namespace TheWindowsClub
{
    public partial class News : PhoneApplicationPage
    {
        public NewsVM Model
        {
            get
            {
                return (NewsVM)this.DataContext;
            }
        }
        public News()
        {
            InitializeComponent();
            Share.Click += Share_Click;
            Model.RssLoaded += Model_RssLoaded;
            Model.NewsLoaded += Model_NewsLoaded;
            LoadedStart.Completed += LoadedStart_Completed;
            this.Loaded += News_Loaded;
            Str_NewsScreenLoads.Begin();
        }

        void News_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        void LoadedStart_Completed(object sender, EventArgs e)
        {
           // Str_NewsScreenLoads.Begin();
        }

        void Model_NewsLoaded(object sender, EventArgs e)
        {
            //foreach (var line in Model.AllRssModel)
            //{
            //    AddPivotItem(line);
            //}
            //selectPivotItem(Model.RssContent);

        }

        void Model_RssLoaded(object sender, EventArgs e)
        {
            
            int i = 1;
            foreach (var pivot in this.pivot.Items.OfType<PivotItem>())
            {
                var item = Model.AllRssModel.SingleOrDefault(x => x.IntID == i);
                if (item != null)
                {
                    pivot.Name = item.Title;
                    //pivot.Header = item.Title;
                    DataTemplate dtemp = usercontro.Resources["AllNewsItemTemp"] as DataTemplate;
                    pivot.ContentTemplate = dtemp;
                    Style st = usercontro.Resources["PivotItemStyle1"] as Style;
                    pivot.Style = st;
                    pivot.Content = item;
                }
                i++;
            }
            selectPivotItem(Model.ID);
            Model.Isbusy = false;
            LoadedStart.Begin();
        }
        void AddPivotItem(RssModel item)
        {
            try
            {
                PivotItem pivot = new PivotItem();
                pivot.Name = item.Title;
                
                //pivot.Header = item.Title;
                DataTemplate dtemp = usercontro.Resources["AllNewsItemTemp"] as DataTemplate;
                pivot.ContentTemplate = dtemp;
                Style st = usercontro.Resources["PivotItemStyle1"] as Style;
                pivot.Style = st;
                pivot.Content = item;
                this.pivot.Items.Add(pivot);
            }
            catch
            {

            }
        }

        void selectPivotItem(string ID)
        {
            var item= Model.AllRssModel.SingleOrDefault(x=>x.ID==ID);

            var data = this.pivot.Items.OfType<PivotItem>().SingleOrDefault(x => x.Name == item.Title);
            if (data != null)
            {
                this.pivot.SelectedItem = data;
            }
        }
        void On_Web_Click(object sender, RoutedEventArgs e)
        {

            if (Model.RssContent != null)
            {
                WebBrowserTask wb = new WebBrowserTask()
                {
                    URL = Model.RssContent.ID,
                };
                wb.Show();
            }
        }

        void Share_Click(object sender, RoutedEventArgs e)
        {

            ShareLinkTask shareLinkTask = new ShareLinkTask();
            shareLinkTask.Title = Model.RssContent.Title;
            shareLinkTask.LinkUri = new Uri(Model.RssContent.ID, UriKind.Absolute);
            shareLinkTask.Message = Model.RssContent.PlainSummary.Substring(0, 100);
            shareLinkTask.Show();// TODO: Add event handler implementation here.  
        }

        void MsgBtn_Click(object sender, RoutedEventArgs e)
        {

            PhoneNumberChooserTask phoneNo = new PhoneNumberChooserTask();
            phoneNo.Completed += phoneNo_Completed;
            phoneNo.Show();// TODO: Add event handler implementation here.  
        }

        void phoneNo_Completed(object sender, PhoneNumberResult e)
        {

            if (e.PhoneNumber != null)
            {
                SmsComposeTask smsTask = new SmsComposeTask();
                smsTask.To = e.PhoneNumber;
                smsTask.Body = Model.RssContent.Title + "\n" + Model.RssContent.PlainSummary;
                smsTask.Show();
            }


        }

        void mailBtn_Click(object sender, RoutedEventArgs e)
        {

            EmailAddressChooserTask Email = new EmailAddressChooserTask();
            Email.Completed += Email_Completed;
            Email.Show();
        }

        void Email_Completed(object sender, EmailResult e)
        {
            if (e.Email != null)
            {
                EmailComposeTask Compose = new EmailComposeTask();
                Compose.To = e.Email;
                Compose.Subject = "The Windows Club: " + Model.RssContent.Title;
                Compose.Body = Model.RssContent.PlainSummary;
                Compose.Show();
            }

        }

        private void PhoneApplicationPage_Loaded_1(object sender, System.Windows.RoutedEventArgs e)
        {
            var ID = NavigationContext.QueryString["Id"];
            var intID = int.Parse(NavigationContext.QueryString["intID"]);
            var type = int.Parse(NavigationContext.QueryString["type"]);

            Model.ID = ID;
            Model.intID = intID;
            Model.TypeID = type;
            Model.Isbusy = true;
            Model.EvaluateHeader();
            Model.LoadRssContent(ID);
           

        }

        public int TypeID { get; set; }

        private void logoBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WebBrowserTask wb = new WebBrowserTask()
            {
                URL = "http://www.thewindowsclub.com/"
            };
            wb.Show();
        }

        private void pivot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var data = (PivotItem)pivot.SelectedItem;
            if (data != null)
            {
                var Item = (RssModel)data.Content;
                if (Item != null)
                {
                    Model.intID = Item.IntID;
                    Model.RssContent = Item;

                }
            }
        }

        private void HyperlinkButton_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Model.RssContent != null)
            {
                WebBrowserTask wb = new WebBrowserTask()
                {
                    URL = Model.RssContent.ID,
                };
                wb.Show();
            }
        	
        }



    }
}
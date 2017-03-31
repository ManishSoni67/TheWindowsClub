using System;
using System.Collections.Generic;
using System.Text;
using WindowsClub.Models;
using System.IO;
using System.Xml;
using System.IO.IsolatedStorage;
using System.ServiceModel.Syndication;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Collections.ObjectModel;

namespace TheWindowsClub
{
    public class NewsVM : BaseModel
    {
        public NewsVM()
        {
            Onint();
        }

        private void Onint()
        {
            AttchEvent();
        }

        public event EventHandler RssLoaded;
        public void RaiseRssLoaded()
        {
            if (RssLoaded != null)
                RssLoaded(this, new EventArgs());
        }

        public event EventHandler NewsLoaded;
        public void RaiseNewsLoaded()
        {
            if (NewsLoaded != null)
                NewsLoaded(this, new EventArgs());
        }
        RssModel _RssContent = new RssModel();
        public RssModel RssContent
        {
            get { return _RssContent; }
            set
            {
                _RssContent = value;

                Notify("RssContent");
            }
        }

        private void AttchEvent()
        {
            foreach (var line in AllRssModel)
            {
                line.NullCase += line_NullCase;
                line.ImaegLoaded += line_ImaegLoaded;
            }
        }

        void line_ImaegLoaded(object sender, EventArgs e)
        {
            var line = sender as RssModel;
            if (TypeID == 0)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultUri);

                }
            }
            else if (TypeID == 1)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            }
            else if (TypeID == 2)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultSecurity);
                }
            }
            else if (TypeID == 3)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultdownLoadUri);
                }
            }
            else if (TypeID == 4)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultLiveUri);
                }
            }
            else if (TypeID == 5)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultIEUri);
                }
            }
        }

        void line_NullCase(object sender, EventArgs e)
        {
            var line = sender as RssModel;
            if (TypeID == 0)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultUri);

                }
            }
            else if (TypeID == 1)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            }
            else if (TypeID == 2)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultSecurity);
                }
            }
            else if (TypeID == 3)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultdownLoadUri);
                }
            }
            else if (TypeID == 4)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultLiveUri);
                }
            }
            else if (TypeID == 5)
            {
                if (line.IsLoaded == false)
                {
                    line.Image = new BitmapImage(App.DefaultIEUri);
                }
            }
        }


        void RssContent_ImaegLoaded(object sender, EventArgs e)
        {
            //Isbusy = false;
            if (RssContent.IsLoaded == false)
            {
                RssContent.Image = new BitmapImage(App.DefaultUri);
            }
        }

        string _ID;
        public string ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                Notify("ID");
            }
        }

        int _intID;
        public int intID
        {
            get { return _intID; }
            set
            {
                _intID = value;
                Notify("intID");
            }
        }

        string _Status;
        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                Notify("Status");
            }
        }
        string _Label;
        public string Label
        {
            get { return _Label; }
            set
            {
                _Label = value;
                Notify("Label");
            }
        }
        double _Progress;
        public double Progress
        {
            get { return _Progress; }
            set
            {
                _Progress = value;
                Notify("Progress");
            }
        }
        public void LoadRssContent(string ID)
        {
         
            Status = "Loading...";
            using (IsolatedStorageFile fs = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var file = fs.OpenFile(XMLEvaluator, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlReader reader = XmlReader.Create(file);
                    SyndicationFeed feed = SyndicationFeed.Load(reader);
                    int i = 0;
                    AllRssModel = new ObservableCollection<RssModel>(feed.Items.Select(x => new RssModel()
                    {
                        ID = x.Id,
                        Header = this.Header,
                        IntID = ++i,
                        Title = x.Title.Text,
                        //PageUri = x.Links.FirstOrDefault().BaseUri,
                        Summary = x.Summary.Text,
                        //Content = x.Content.ToString(),
                        PublishedDate = x.PublishDate
                    }));
                    LoadAllImaegs();
                    LoadData(intID);
                    Notify("AllRssModel");
                    Status = null;
                }
            }
        }

        public void LoadAllImaegs()
        {
            foreach (var line in AllRssModel)
            {
                try
                {
                    //using (IsolatedStorageFile fs = IsolatedStorageFile.GetUserStoreForApplication())
                    //{
                    //    if (fs.FileExists(line.ImageName))
                    //    {
                    //        using (var ImageFile = fs.OpenFile(line.ImageName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    //        {
                    //            line.Image = new BitmapImage();
                    //            line.Image.SetSource(ImageFile);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (TypeID == 0)
                    //        {
                    //            Uri ur = new Uri("/Images/1369001253_news.png", UriKind.Relative);
                    //            line.Image = new BitmapImage(ur);
                    //        }
                    //        else
                    //        {
                    //            line.Image = new BitmapImage(HeaderImage.UriSource);
                    //        }
                    //    }
                    //}
                }
                catch
                {
 
                }
                 if (TypeID == 0)
                 {
                     Uri ur = new Uri("/Images/1369001253_news.png", UriKind.Relative);
                     line.Image = new BitmapImage(ur);
                 }
                 else
                 {
                     line.Image = new BitmapImage(HeaderImage.UriSource);
                 }
                 
                
            }
        }

        private void LoadData(int ID)
        {
            var data = AllRssModel.SingleOrDefault(x => x.IntID == ID);
            if (data != null)
            {
                RssContent = new RssModel()
                {
                    ID = data.ID,
                    IntID = intID,
                    Header = Header,
                    Title = data.Title,
                    Summary = data.Summary,
                    PublishedDate = data.PublishedDate,
                    Image=data.Image,
                };
            }
            RaiseRssLoaded();
        }

        public void EvaluateHeader()
        {
            if (TypeID == 0)
            {
                XMLEvaluator = App.XMLDoc;
                HeaderImage = new BitmapImage(App.DefaltNewsUri);
                Label = "What's New";
            }
            else if (TypeID == 1)
            {
                XMLEvaluator = App.XMLWindowsDoc;
                Header = "Windows";
                HeaderImage = new BitmapImage(App.DefaultWindowsUri);
                Label = "Windows";
            }
            if (TypeID == 2)
            {
                XMLEvaluator = App.XMLSecurityDoc;
                Header = "Security";
                HeaderImage = new BitmapImage(App.DefaultSecurity);
                Label = "Security";
            }
            else if (TypeID == 5)
            {
                XMLEvaluator = App.XMLIEDoc;
                Header = "IE";
                HeaderImage = new BitmapImage(App.DefaultIEUri);
                Label = "Internet Explorer";
            }
            if (TypeID == 3)
            {
                XMLEvaluator = App.XMLDownDoc;
                Header = "Down";
                HeaderImage = new BitmapImage(App.DefaultdownLoadUri);
                Label = "Downloads";
            }
            else if (TypeID == 4)
            {
                XMLEvaluator = App.XMLLiveDoc;
                Header = "Live";
                HeaderImage = new BitmapImage(App.DefaultLiveUri);
                Label = "Live!";
            }
        }


        string _Header;
        public string Header
        {
            get { return _Header; }
            set
            {
                _Header = value;
                Notify("Header");
            }
        }
        
        int _TypeID;
        public int TypeID
        {
            get { return _TypeID; }
            set
            {
                _TypeID = value;
                Notify("TypeID");
            }
        }
        public string XMLEvaluator { get; set; }

        BitmapImage _HeaderImage = new BitmapImage();
        public BitmapImage HeaderImage
        {
            get { return _HeaderImage; }
            set
            {
                _HeaderImage = value;
                Notify("HeaderImage");
            }
        }

        ObservableCollection<RssModel> _AllRssModel = new ObservableCollection<RssModel>();
        public ObservableCollection<RssModel> AllRssModel
        {
            get { return _AllRssModel; }
            set
            {
                _AllRssModel = value;
                AttchEvent();
                Notify("AllRssModel");
            }
        }

    }
}

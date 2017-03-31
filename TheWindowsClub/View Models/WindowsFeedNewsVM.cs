using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;
using System.Net;
using System.Collections.ObjectModel;
using WindowsClub.Models;
using System.Xml;
using System.IO;
using System.Windows.Media.Imaging;

namespace TheWindowsClub
{
    public class WindowsFeedNewsVM : BaseModel
    {
       public WindowsFeedNewsVM()
        {
            Oninit();
        }
        private void Oninit()
        {
           // AttachEvent();
        }

        private void AttachEvent()
        {
            FirstTile.ImaegLoaded += (s, e) =>
            {

                if (FirstTile.IsLoaded == false)
                {
                    FirstTile.Image = new BitmapImage(App.DefaultWindowsUri);
                }

            };
            SeconTile.ImaegLoaded += (s, e) =>
            {

                if (SeconTile.IsLoaded == false)
                {
                    SeconTile.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            };
            ThirdTile.ImaegLoaded += (s, e) =>
            {

                if (ThirdTile.IsLoaded == false)
                {
                    ThirdTile.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            };
            ForthTile.ImaegLoaded += (s, e) =>
            {

                if (ForthTile.IsLoaded == false)
                {
                    ForthTile.Image = new BitmapImage(App.DefaultWindowsUri);
                }

            };
            FifthTile.ImaegLoaded += (s, e) =>
            {

                if (FifthTile.IsLoaded == false)
                {
                    FifthTile.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            };
            SixthTile.ImaegLoaded += (s, e) =>
            {
                if (SixthTile.IsLoaded == false)
                {
                    SixthTile.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            };
            SeventhTile.ImaegLoaded += (s, e) =>
            {
                if (SeventhTile.IsLoaded == false)
                {
                    SeventhTile.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            };
            EightthTile.ImaegLoaded += (s, e) =>
            {
                if (EightthTile.IsLoaded == false)
                {
                    EightthTile.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            };
            NinthTile.ImaegLoaded += (s, e) =>
            {
                if (NinthTile.IsLoaded == false)
                {
                    NinthTile.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            };
            TenthModel.ImaegLoaded += (s, e) =>
            {
                Isbusy = false;
                if (TenthModel.IsLoaded == false)
                {
                    TenthModel.Image = new BitmapImage(App.DefaultWindowsUri);
                }
              
            };

        }
        public void LoadRss()
        {
            Uri ur = new Uri(App.WindowsFeedSource, UriKind.Absolute);
            WebClient wb = new WebClient();
            wb.OpenReadCompleted += (s, e) =>
            {
                Status = null;
                Isbusy = false;
                if (e.Error != null)
                {

                    IsolatedStorageFile fs = IsolatedStorageFile.GetUserStoreForApplication();
                    if (fs.FileExists(App.XMLWindowsDoc))
                    {
                        using (IsolatedStorageFileStream file = fs.OpenFile(App.XMLWindowsDoc, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {


                            XmlReader reader = XmlReader.Create(file);
                            //XElement xdoc = XElement.Load(reader);
                            // var Items = xdoc.Elements();
                            SyndicationFeed feed = SyndicationFeed.Load(reader);
                            int i = 0;

                            AllRssData = new ObservableCollection<RssModel>(feed.Items.OrderByDescending(x => x.PublishDate).Take(10).Select(x => new RssModel()
                            {
                                ID = x.Id,
                                Header = "Windows",
                                IntID = ++i,
                                Title = x.Title.Text,
                               
                                //PageUri = x.Links.FirstOrDefault().BaseUri,
                                Summary = x.Summary.Text,
                                //Content = x.Content.ToString(),
                                PublishedDate = x.PublishDate
                            }));
                            i = 0;
                            FillAllData();
                            
                 

                        }
                    }
                }
                else
                {
                    Status = null;
                    IsolatedStorageFile fs = IsolatedStorageFile.GetUserStoreForApplication();
                    if (fs.FileExists(App.XMLWindowsDoc))
                    {
                        fs.DeleteFile(App.XMLWindowsDoc);
                    }
                    using (IsolatedStorageFileStream file = fs.CreateFile(App.XMLWindowsDoc))
                    {
                        e.Result.CopyTo(file);
                    }
                    using (IsolatedStorageFileStream file = fs.OpenFile(App.XMLWindowsDoc, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        XmlReader reader = XmlReader.Create(file);
                        SyndicationFeed feed = SyndicationFeed.Load(reader);
                        int i = 0;
                        AllRssData = new ObservableCollection<RssModel>(feed.Items.OrderByDescending(x => x.PublishDate).Take(10).Select(x => new RssModel()
                        {
                            ID = x.Id,
                            Header = "Windows",
                            IntID = ++i,
                            Title = x.Title.Text,
                            PageUri = x.Links.FirstOrDefault().BaseUri,
                            Summary = x.Summary.Text,
                            PublishedDate = x.PublishDate
                        }));
                        FillAllData();
                        
                    }
                }
                Isbusy = false;
            };
            Isbusy = true;
            Status = "Loading...";
            wb.OpenReadAsync(ur);
        }

     

        
        private void FillAllData()
        {
            FirstTile = AllRssData.SingleOrDefault(x => x.IntID == 1);
            SeconTile = AllRssData.SingleOrDefault(x => x.IntID == 2);
            ThirdTile = AllRssData.SingleOrDefault(x => x.IntID == 3);
            ForthTile = AllRssData.SingleOrDefault(x => x.IntID == 4);
            FifthTile = AllRssData.SingleOrDefault(x => x.IntID == 5);
            SixthTile = AllRssData.SingleOrDefault(x => x.IntID == 6);
            SeventhTile = AllRssData.SingleOrDefault(x => x.IntID == 7);
            EightthTile = AllRssData.SingleOrDefault(x => x.IntID == 8);
            NinthTile = AllRssData.SingleOrDefault(x => x.IntID == 9);
            TenthModel = AllRssData.SingleOrDefault(x => x.IntID == 10);
            CheckForallImages();
        }

        ObservableCollection<RssModel> _AllRssData = new ObservableCollection<RssModel>();
        public ObservableCollection<RssModel> AllRssData
        {
            get { return _AllRssData; }
            set
            {
                _AllRssData = value;
                AttachAllEvents();
                Notify("AllRssData");
            }
        }

        private void AttachAllEvents()
        {
            
        }


        private void CheckForallImages()
        {
            var AllData = AllRssData.Where(x => x.ImageUri == null);
            if (AllData != null)
            {
                foreach (var data in AllData)
                {
                    data.Image = new BitmapImage(App.DefaultWindowsUri);
                }
            }


        }

        RssModel _FirstTile = new RssModel();
        public RssModel FirstTile
        {
            get { return _FirstTile; }
            set
            {
                _FirstTile = value;
              //  AttachEvent();
                Notify("FirstTile");
            }
        }

        RssModel _SeconTile = new RssModel();
        public RssModel SeconTile
        {
            get { return _SeconTile; }
            set
            {
                _SeconTile = value;
                //AttachEvent();
                Notify("SeconTile");
            }
        }

        RssModel _ThirdTile = new RssModel();
        public RssModel ThirdTile
        {
            get { return _ThirdTile; }
            set
            {
                _ThirdTile = value;
               // AttachEvent();

                Notify("ThirdTile");
            }
        }
        RssModel _ForthTile = new RssModel();
        public RssModel ForthTile
        {
            get { return _ForthTile; }
            set
            {
                _ForthTile = value;
                //AttachEvent();

                Notify("ForthTile");
            }
        }
        RssModel _FifthTile = new RssModel();
        public RssModel FifthTile
        {
            get { return _FifthTile; }
            set
            {
                _FifthTile = value;
                //AttachEvent();

                Notify("FifthTile");
            }
        }

        RssModel _SixthTile = new RssModel();
        public RssModel SixthTile
        {
            get { return _SixthTile; }
            set
            {
                _SixthTile = value;
                //AttachEvent();

                Notify("SixthTile");
            }
        }
        RssModel _SeventhTile = new RssModel();
        public RssModel SeventhTile
        {
            get { return _SeventhTile; }
            set
            {
                _SeventhTile = value;
                //AttachEvent();

                Notify("SeventhTile");
            }
        }

        RssModel _EightthTile = new RssModel();
        public RssModel EightthTile
        {
            get { return _EightthTile; }
            set
            {
                _EightthTile = value;
                //AttachEvent();

                Notify("EightthTile");
            }
        }

        RssModel _NinthTile = new RssModel();
        public RssModel NinthTile
        {
            get { return _NinthTile; }
            set
            {
                _NinthTile = value;
//                AttachEvent();

                Notify("NinthTile");
            }
        }

        RssModel _TenthModel = new RssModel();
        public RssModel TenthModel
        {
            get { return _TenthModel; }
            set
            {
                _TenthModel = value;
                AttachEvent();

                Notify("TenthModel");
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsClub.Models;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.ServiceModel.Syndication;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;
using System.Windows;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Notification;

namespace TheWindowsClub
{
    public class MainPageVM : BaseModel
    {
        public MainPageVM()
        {
            Oninit();
        }

        private void Oninit()
        {
            AttachEvent();
        }
        public event EventHandler NewsLoaded;
        public void RaiseNewsLoaded()
        {
            if (NewsLoaded != null)
                NewsLoaded(this, new EventArgs());
        }

        private void AttachEvent()
        {
            FirstTile.ImaegLoaded += (s, e) =>
            {
                if (FirstTile.IsLoaded == false)
                {
                    FirstTile.Image = new BitmapImage(App.DefaultUri);
                }
            };

            SeconTile.ImaegLoaded += (s, e) =>
            {
                if (SeconTile.IsLoaded == false)
                {
                    SeconTile.Image = new BitmapImage(App.DefaultUri);
                }
            };
            ThirdTile.ImaegLoaded += (s, e) =>
            {

                if (ThirdTile.IsLoaded == false)
                {
                    ThirdTile.Image = new BitmapImage(App.DefaultUri);
                }
            };
            ForthTile.ImaegLoaded += (s, e) =>
            {

                if (ForthTile.IsLoaded == false)
                {
                    ForthTile.Image = new BitmapImage(App.DefaultUri);
                }

            };
            FifthTile.ImaegLoaded += (s, e) =>
            {

                if (FifthTile.IsLoaded == false)
                {
                    FifthTile.Image = new BitmapImage(App.DefaultUri);
                }
            };
            SixthTile.ImaegLoaded += (s, e) =>
            {
                if (SixthTile.IsLoaded == false)
                {
                    SixthTile.Image = new BitmapImage(App.DefaultUri);
                }
            };
            SeventhTile.ImaegLoaded += (s, e) =>
            {
                if (SeventhTile.IsLoaded == false)
                {
                    SeventhTile.Image = new BitmapImage(App.DefaultUri);
                }
            };
            EightthTile.ImaegLoaded += (s, e) =>
            {
                if (EightthTile.IsLoaded == false)
                {
                    EightthTile.Image = new BitmapImage(App.DefaultUri);
                }
            };
            NinthTile.ImaegLoaded += (s, e) =>
            {
                if (NinthTile.IsLoaded == false)
                {
                    NinthTile.Image = new BitmapImage(App.DefaultUri);
                }
            };
            TenthModel.ImaegLoaded += (s, e) =>
            {
                Isbusy = false;
                if (TenthModel.IsLoaded == false)
                {
                    TenthModel.Image = new BitmapImage(App.DefaultUri);

                }
                UpdateTile();
                UpdateToastNotification();
            };





        }

        private void UpdateToastNotification()
        {
            //HttpNotificationChannel channel = new HttpNotificationChannel("MyChannel");
            //var data = HttpNotificationChannel.Find("MyChannel");
            //if (data == null)
            //{
            //    data = new HttpNotificationChannel("MyChannel", App.FeedSource);
            //    data.Open();

            //    ShellToast Toast = new ShellToast()
            //    {
            //        Title = FirstTile.Title,
            //        Content = FirstTile.PlainSummary,
            //        NavigationUri = null

            //    };
            //    Toast.Show();
            //    data.BindToShellToast();

            //}
            //else
            //{
            //    //data.Open();
            //    ShellToast Toast = new ShellToast()
            //    {
            //        Title = FirstTile.Title,
            //        Content = FirstTile.PlainSummary,
            //        NavigationUri = null

            //    };
            //    Toast.Show();
            //}
        }





        public void LoadRss()
        {
            Uri ur = new Uri(App.FeedSource, UriKind.Absolute);
            WebClient wb = new WebClient();
            wb.OpenReadCompleted += (s, e) =>
                {
                    Status = null;
                    Isbusy = false;
                    IsVisualBusy = false;
                    RaiseNewsLoaded();
                    if (e.Error != null)
                    {

                        IsolatedStorageFile fs = IsolatedStorageFile.GetUserStoreForApplication();
                        if (fs.FileExists(App.XMLDoc))
                        {
                            using (IsolatedStorageFileStream file = fs.OpenFile(App.XMLDoc, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {


                                XmlReader reader = XmlReader.Create(file);
                                //XElement xdoc = XElement.Load(reader);
                                // var Items = xdoc.Elements();
                                SyndicationFeed feed = SyndicationFeed.Load(reader);
                                int i = 0;

                                AllRssData = new ObservableCollection<RssModel>(feed.Items.OrderByDescending(x => x.PublishDate).Take(10).Select(x => new RssModel()
                                {
                                    ID = x.Id,
                                    Header = "What's New",
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
                        else
                        {
                            MessageBox.Show("Internet Problem :( Try Again");
                        }
                    }
                    else
                    {
                        Status = null;
                        IsolatedStorageFile fs = IsolatedStorageFile.GetUserStoreForApplication();
                        if (fs.FileExists(App.XMLDoc))
                        {
                            fs.DeleteFile(App.XMLDoc);
                        }
                        using (IsolatedStorageFileStream file = fs.CreateFile(App.XMLDoc))
                        {
                            e.Result.CopyTo(file);
                        }
                        using (IsolatedStorageFileStream file = fs.OpenFile(App.XMLDoc, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            XmlReader reader = XmlReader.Create(file);
                            SyndicationFeed feed = SyndicationFeed.Load(reader);
                            int i = 0;
                            AllRssData = new ObservableCollection<RssModel>(feed.Items.OrderByDescending(x => x.PublishDate).Take(10).Select(x => new RssModel()
                            {
                                ID = x.Id,
                                Header = "What's New",
                                IntID = ++i,
                                Title = x.Title.Text,
                                PageUri = x.Links.FirstOrDefault().BaseUri,
                                Summary = x.Summary.Text,
                                PublishedDate = x.PublishDate
                            }));
                            FillAllData();
                            Isbusy = false;
                            IsVisualBusy = false;
                        }
                    }
                };
            Isbusy = true;
            IsVisualBusy = true;
            Status = "Loading...";
            wb.OpenReadAsync(ur);
        }

        private void UpdateTile()
        {
            ShellTile currentTiles = ShellTile.ActiveTiles.FirstOrDefault();
            var UpdatedData = new StandardTileData()
            {
                Title = "WC Got News :)",
                Count = AllRssData.Where(x => (x.PublishedDate.Date.CompareTo(DateTimeOffset.Now.Date) == 0) || (x.PublishedDate.Date.CompareTo(DateTimeOffset.Now.Date) == 1)).Count(),
                BackgroundImage = new Uri("/Images/The Widows Club.jpg", UriKind.Relative),
            };

            //var Data=AllRssData.Where(x => (x.PublishedDate.Date.CompareTo(DateTimeOffset.Now.Date) == 0) || (x.PublishedDate.Date.CompareTo(DateTimeOffset.Now.Date) == 1));

            foreach (var line in AllRssData)
            {
                if (line.IsLoaded == true)
                {
                    if (!App.IsTargetedVersion)
                    {
                        UpdatedData.BackBackgroundImage = line.ImageUri;
                        UpdatedData.BackTitle = null;
                        UpdatedData.BackContent = null;
                    }
                    else
                    {
                        App.UpdateFlipTile("The Windows Club", line.Title, line.PlainSummary, line.PlainSummary, UpdatedData.Count.Value, new Uri("/", UriKind.Relative), new Uri("/Images/The Widows Club.jpg", UriKind.Relative), line.ImageUri, new Uri("/Images/The Widows Club.jpg", UriKind.Relative), line.ImageUri, new Uri("/Images/The Widows Club.jpg", UriKind.Relative));
                    }
                    // UpdatedData.BackContent = line.PlainSummary.Substring(0, 140);
                    break;
                }
                else
                {
                    if (!App.IsTargetedVersion)
                    {
                        UpdatedData.BackBackgroundImage = App.DeafultBackUri;
                        UpdatedData.BackTitle = line.Title;
                        UpdatedData.BackContent = line.PlainSummary.Substring(0, 140);
                    }
                    else
                    {
                        App.UpdateFlipTile("The Windows Club", line.Title, line.PlainSummary, line.PlainSummary, UpdatedData.Count.Value, new Uri("/", UriKind.Relative), new Uri("/Images/The Widows Club.jpg", UriKind.Relative), line.ImageUri, new Uri("/Images/The Widows Club.jpg", UriKind.Relative), App.DeafultBackUri, new Uri("/Images/The Widows Club.jpg", UriKind.Relative));
                    }
                }
            }

            //UpdatedData.BackBackgroundImage = SeconTile.ImageUri;
            //UpdatedData.BackContent = SeconTile.PlainSummary.Substring(0, 140);
            //UpdatedData.BackTitle = SeconTile.Title;

            currentTiles.Update(UpdatedData);

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
            foreach (var line in AllRssData)
            {

            }
        }


        private void CheckForallImages()
        {
            var AllData = AllRssData.Where(x => x.ImageUri == null);
            if (AllData != null)
            {
                foreach (var data in AllData)
                {
                    data.Image = new BitmapImage(App.DefaultUri);
                }
            }


        }

        bool _IsVisualBusy;
        public bool IsVisualBusy
        {
            get { return _IsVisualBusy; }
            set
            {
                _IsVisualBusy = value;
                Notify("IsVisualBusy");
            }
        }

        RssModel _FirstTile = new RssModel();
        public RssModel FirstTile
        {
            get { return _FirstTile; }
            set
            {
                _FirstTile = value;
                //AttachEvent();
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
                //AttachEvent();

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
                //AttachEvent();

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

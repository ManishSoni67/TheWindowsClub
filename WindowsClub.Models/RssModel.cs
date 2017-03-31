using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows;

namespace WindowsClub.Models
{
    public class RssModel : BaseModel
    {
        public RssModel()
        {
            this.ImageUriNull += RssModel_ImageUriNull;

        }

        void RssModel_ImageUriNull(object sender, EventArgs e)
        {
            IsLoaded = false;
            RaiseNullCase();
        }

        public event EventHandler NullCase;
        public void RaiseNullCase()
        {
            if (NullCase != null)
                NullCase(this, new EventArgs());
        }
        public event EventHandler ImaegLoading;
        public void RaiseImaegLoading()
        {
            if (ImaegLoading != null)
                ImaegLoading(this, new EventArgs());
        }

        public event EventHandler ImaegLoaded;
        public void RaiseImaegLoaded()
        {
            if (ImaegLoaded != null)
                ImaegLoaded(this, new EventArgs());
        }
        public event EventHandler ImageUriNull;
        public void RaiseImageUriNull()
        {
            if (ImageUriNull != null)
                ImageUriNull(this, new EventArgs());
        }

        int _Type;
        public int Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                Notify("Type");
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
        string _Header;
        public string Header
        {
            get { return _Header; }
            set
            {
                _Header = value;
                EvaluateName();
                Notify("Header");
            }
        }



        int _IntID;
        public int IntID
        {
            get { return _IntID; }
            set
            {
                _IntID = value;
                EvaluateName();
                Notify("IntID");
            }
        }

        private void EvaluateName()
        {
            ImageName = IntID + "image" + Header;
        }
        string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                Notify("Title");
            }
        }

        string _Summary;
        public string Summary
        {
            get { return _Summary; }
            set
            {
                _Summary = value;
                EvaluateSummary();
                Notify("Summary");
            }
        }


        bool _IsLoaded = false;
        public bool IsLoaded
        {
            get { return _IsLoaded; }
            set
            {
                _IsLoaded = value;
                Notify("IsLoaded");
            }
        }
        private void EvaluateSummary()
        {
            PlainSummary = HttpUtility.HtmlDecode(Regex.Replace(Summary, "<[^>]+?>", ""));
            var reg = new Regex(" src=\"([^\"]*)\"", RegexOptions.IgnoreCase & RegexOptions.IgnorePatternWhitespace);
            var Format = PlainSummary.Split('[');
            PlainSummary = Format.FirstOrDefault();
            var match = reg.Match(Summary);
            if (match.Success)
            {
                String Uri = match.Value;
                var AllUri = Uri.Split('"');
                if (string.IsNullOrWhiteSpace(AllUri[1]))
                {

                    RaiseNullCase();
                }
                else
                {
                    ImageUri = new Uri(AllUri[1], UriKind.Absolute);
                }
            }
            else
            {
             reg = new Regex("src=(?:\"|\')?(?<imgSrc>[^>]*[^/].(?:jpg|bmp|gif|png))(?:\"|\')?");
             match = reg.Match(Summary);
             if (match.Success)
             {
                 String Uri = match.Value;
                 var AllUri = Uri.Split('=');
                 var ImgUri = AllUri[1].Trim('\'');

                 if (string.IsNullOrWhiteSpace(ImgUri))
                 {

                     RaiseNullCase();
                 }
                 else
                 {
                     ImageUri = new Uri(ImgUri, UriKind.Absolute);
                 }
             }
             else
             {
                 RaiseNullCase();
             }
           
            }
        }

        DateTimeOffset _PublishedDate;
        public DateTimeOffset PublishedDate
        {
            get { return _PublishedDate; }
            set
            {
                _PublishedDate = value;
                Notify("PublishedDate");
            }
        }

        string _PlainSummary;
        public string PlainSummary
        {
            get { return _PlainSummary; }
            set
            {
                _PlainSummary = value;
                Notify("PlainSummary");
            }
        }

        Uri _ImageUri;
        public Uri ImageUri
        {
            get { return _ImageUri; }
            set
            {
                _ImageUri = value;
                EvaluateImage();
                Notify("ImageUri");
            }
        }

        private void EvaluateImage()
        {
            WebClient wb = new WebClient();
            wb.OpenReadCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    using (IsolatedStorageFile fs = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        try
                        {
                            if (fs.FileExists(ImageName))
                            {

                                IsolatedStorageFileStream file = fs.OpenFile(ImageName, FileMode.Open, FileAccess.Read, FileShare.Read);
                                Image.SetSource(file);
                                file.Close();
                                IsLoaded = true;
                            }
                            else
                            {
                                IsLoaded = false;
                            }
                        }
                        catch
                        {
                            IsLoaded = false;
                        }
                    }

                }
                else
                {
                    using (IsolatedStorageFile fs = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        try
                        {
                            if (fs.FileExists(ImageName))
                            {
                                fs.DeleteFile(ImageName);
                            }
                            using (IsolatedStorageFileStream file = fs.CreateFile(ImageName))
                            {
                                e.Result.Seek(0, SeekOrigin.Begin);
                                //e.Result.CopyTo(file);
                                Image = new BitmapImage();
                                Byte[] PhotoBytes = new byte[e.Result.Length];
                                e.Result.Read(PhotoBytes, 0, PhotoBytes.Length);
                                file.Write(PhotoBytes, 0, PhotoBytes.Length);
                                file.Seek(0, SeekOrigin.Begin);
                                //Convert the bytes back to a bitmap
                                MemoryStream stream = new MemoryStream(PhotoBytes);
                                //   BitmapImage image = new BitmapImage();

                                Image.SetSource(file);
                                //   file.Close();
                                IsLoaded = true;
                            }
                        }
                        catch (Exception ee)
                        {
                            // var data = ee;
                            IsLoaded = false;
                            //Image = new BitmapImage(new Uri(ImageUri.AbsoluteUri, UriKind.Absolute));
                        }
                        RaiseImaegLoaded();
                    }

                }


            };
           
            wb.OpenReadAsync(ImageUri);
            RaiseImaegLoading();
           

        }

        Uri _PageUri;
        public Uri PageUri
        {
            get { return _PageUri; }
            set
            {
                _PageUri = value;
                Notify("PageUri");
            }
        }
        string _Content;
        public string Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                Notify("Content");
            }
        }

        BitmapImage _Image;
        public BitmapImage Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                Notify("Image");
            }
        }

        string _ImageName;
        public string ImageName
        {
            get { return _ImageName; }
            set
            {
                _ImageName = value;
                Notify("ImageName");
            }
        }

    }
}

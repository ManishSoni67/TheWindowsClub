using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TheWindowsClub
{
    public partial class App : Application
    {
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        private static Version TargetedVersion = new Version(7, 10, 8858);
        public static bool IsTargetedVersion { get { return Environment.OSVersion.Version >= TargetedVersion; } }

        public static void UpdateFlipTile(
            string title,
            string backTitle,
            string backContent,
            string wideBackContent,
            int count,
            Uri tileId,
            Uri smallBackgroundImage,
            Uri backgroundImage,
            Uri backBackgroundImage,
            Uri wideBackgroundImage,
            Uri wideBackBackgroundImage)
        {
            if (IsTargetedVersion)
            {
                // Get the new FlipTileData type.
                Type flipTileDataType = Type.GetType("Microsoft.Phone.Shell.FlipTileData, Microsoft.Phone");

                // Get the ShellTile type so we can call the new version of "Update" that takes the new Tile templates.
                Type shellTileType = Type.GetType("Microsoft.Phone.Shell.ShellTile, Microsoft.Phone");

                // Loop through any existing Tiles that are pinned to Start.
                foreach (var tileToUpdate in ShellTile.ActiveTiles)
                {
                    // Look for a match based on the Tile's NavigationUri (tileId).
                    if (tileToUpdate.NavigationUri.ToString() == tileId.ToString())
                    {
                        // Get the constructor for the new FlipTileData class and assign it to our variable to hold the Tile properties.
                        var UpdateTileData = flipTileDataType.GetConstructor(new Type[] { }).Invoke(null);

                        // Set the properties. 
                        SetProperty(UpdateTileData, "Title", title);
                        SetProperty(UpdateTileData, "Count", count);
                        SetProperty(UpdateTileData, "BackTitle", backTitle);
                        SetProperty(UpdateTileData, "BackContent", backContent);
                        SetProperty(UpdateTileData, "SmallBackgroundImage", smallBackgroundImage);
                        SetProperty(UpdateTileData, "BackgroundImage", backgroundImage);
                        SetProperty(UpdateTileData, "BackBackgroundImage", backBackgroundImage);
                        SetProperty(UpdateTileData, "WideBackgroundImage", wideBackgroundImage);
                        SetProperty(UpdateTileData, "WideBackBackgroundImage", wideBackBackgroundImage);
                        SetProperty(UpdateTileData, "WideBackContent", wideBackContent);

                        // Invoke the new version of ShellTile.Update.
                        shellTileType.GetMethod("Update").Invoke(tileToUpdate, new Object[] { UpdateTileData });
                        break;
                    }
                }
            }

        }

        private static void SetProperty(object instance, string name, object value)
        {
            var setMethod = instance.GetType().GetProperty(name).GetSetMethod();
            setMethod.Invoke(instance, new object[] { value });
        }




        public PhoneApplicationFrame RootFrame { get; private set; }

        public static String XMLDoc = "NewsFeed.xml";

        public static String XMLWindowsDoc = "WindowsNewsFeed.xml";

        public static String XMLSecurityDoc = "SecurityNewsFeed.xml";

        public static String XMLLiveDoc = "LiveNewsFeed.xml";

        public static String XMLDownDoc = "DownNewsFeed.xml";

        public static String XMLIEDoc = "IENewsFeed.xml";

        public static Uri DefaultUri = new Uri("/Images/1369001253_news.png", UriKind.Relative);
        public static Uri DefaltNewsUri = new Uri("/Images/1369431249_News_source.png", UriKind.Relative);

        public static Uri DeafultBackUri = new Uri("/Images/1369434101_rss_square_gray.png", UriKind.Relative);

        public static Uri DefaultWindowsUri= new Uri("/Images/1369580839_32-windows8.png",UriKind.Relative);

        public static Uri DefaultSecurity = new Uri("/Images/1369580799_security-high.png", UriKind.Relative);
        public static Uri DefaultIEUri = new Uri("/Images/1369581314_IE.png", UriKind.Relative);
        public static Uri DefaultdownLoadUri = new Uri("/Images/1369582696_Folder-Downloads.png", UriKind.Relative);
        public static Uri DefaultLiveUri = new Uri("/Images/1369581462_Live_Messenger.png", UriKind.Relative);

        public static String FeedSource = "http://news.thewindowsclub.com/feed/";

        public static string WindowsFeedSource = "http://www.thewindowsclub.com/category/windows/feed";

        public static String DownloadsFeedSource = "http://www.thewindowsclub.com/category/downloads/feed";

        public static string SecurityFeedSource = "http://www.thewindowsclub.com/category/security/feed";

        public static string IEFeedSource = "http://www.thewindowsclub.com/category/ie/feed";

        public static string LiveFeedSource = "http://www.thewindowsclub.com/category/live/feed";
        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}
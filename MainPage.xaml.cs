using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.ApplicationModel.Core;
using Microsoft.Toolkit.Uwp.Connectivity;
using Windows.System;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI.Xaml.Media.Animation;
using System.Collections.ObjectModel;
using System.Net.Http;
using Microsoft.Toolkit.Parsers.Rss;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Core;
using Windows.Storage.Streams;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.Toolkit.Uwp.UI.Helpers;
// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MANAGER_NETWORK_DETECTOR
{
    public class InterestPoint
    {
        public Uri ImageSourceUri { get; set; }
        public Geopoint Location { get; set; }
        public RotateTransform Rotate { get; set; }
        public TranslateTransform Translate { get; set; }
        public System.Drawing.Point CenterPoint { get; set; }
    }
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public ObservableCollection<RssSchema> RSSFeed { get; } = new ObservableCollection<RssSchema>();
        public MainPage()
        {
            this.InitializeComponent();
            CustomizeTitleBar();
            // Set theme for window root
            FrameworkElement root = (FrameworkElement)Window.Current.Content;
            root.RequestedTheme = AppSettings.Theme;
            ParseRSS();
            TXT_DASHBORD_VERSION_APP.Text = "VERSION : " + $"{SystemInformation.ApplicationVersion.Major}.{SystemInformation.ApplicationVersion.Minor}.{SystemInformation.ApplicationVersion.Build}.{SystemInformation.ApplicationVersion.Revision}" + " - MANAGER NETWORK DETECTOR (2020)";

        }

        // Customisation de la barre de titre
        private void CustomizeTitleBar()
        {
            // Customisation de la bar de titre
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(customTitleBar);
        }

        public string Url { get; set; } = "https://sidl-corporation.fr/feed/";
        public static MainPage Current { get; internal set; }

        public async void ParseRSS()
        {
            string feed = null;
            RSSFeed.Clear();

            using (var client = new HttpClient())
            {
                try
                {
                    feed = await client.GetStringAsync(UrlBox.Text);
                }
                catch
                {
                }
            }

            if (feed != null)
            {
                var parser = new RssParser();
                var rss = parser.Parse(feed);

                foreach (var element in rss)
                {
                    RSSFeed.Add(element);
                }
            }
        }

        internal void NotifyUser(string v, object errorMessage)
        {
            throw new NotImplementedException();
        }

        private async void RSSList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RSSList.SelectedItem is RssSchema rssItem)
            {
                try
                {
                    await Launcher.LaunchUriAsync(new Uri(rssItem.FeedUrl));
                }
                catch
                {
                }
            }

            RSSList.SelectedItem = null;
        }

        private void BUTTON_SETTINGS_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsAppsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            MAP.MapServiceToken = "wcklKBaJekuIwGC7dAYw~wZIOwg3KY8QZNxtc-ZVpgw~AnvptMcmzRusLY4o19HWQ1RjTNTp_HJV1DiOXVDFdHjjGjVMFGYjBs14UykKG_1I";
        }

        private void MAP_Loaded(object sender, RoutedEventArgs e)
        {
            SetMapStyle();
            SetMapProjection();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached. The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            StopTrackingButton.IsEnabled = false;
        }


        private void TrafficFlowVisible_Checked(object sender, RoutedEventArgs e)
        {
            MAP.TrafficFlowVisible = true;
        }

        private void trafficFlowVisibleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MAP.TrafficFlowVisible = false;
        }

        private void styleCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Protect against events that are raised before we are fully initialized.
            if (MAP != null)
            {
                SetMapStyle();
            }
        }

        private void SetMapStyle()
        {
            switch (styleCombobox.SelectedIndex)
            {
                case 0:
                    MAP.Style = MapStyle.Road;
                    break;
                case 1:
                    MAP.Style = MapStyle.Aerial;
                    break;
                case 2:
                    MAP.Style = MapStyle.Aerial3D;
                    break;
                case 3:
                    MAP.Style = MapStyle.Aerial3DWithRoads;
                    break;
                case 4:
                    MAP.Style = MapStyle.AerialWithRoads;
                    break;
                case 5:
                    MAP.Style = MapStyle.Terrain;
                    break;
            }
        }

        private void projectionCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Protect against events that are raised before we are fully initialized.
            if (MAP != null)
            {
                SetMapProjection();
            }
        }

        private void SetMapProjection()
        {
            switch (projectionCombobox.SelectedIndex)
            {
                case 0:
                    MAP.MapProjection = MapProjection.WebMercator;
                    break;
                case 1:
                    MAP.MapProjection = MapProjection.Globe;
                    break;
            }
        }

        private void BTN_SWITCH_PAGE_NETWORK_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NetworkAppsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void colorshemaCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Protect against events that are raised before we are fully initialized.
            if (MAP != null)
            {
                SetMapColorShema();
            }
        }

        private void SetMapColorShema()
        {
            switch (colorshemaCombobox.SelectedIndex)
            {
                case 0:
                    MAP.ColorScheme = MapColorScheme.Light;
                    break;
                case 1:
                    MAP.ColorScheme = MapColorScheme.Dark;
                    break;
            }
        }
        private void SSL_Click(object sender, RoutedEventArgs e)
        {
            var SSL = new HyperlinkButton();
            SSL.Content = "SITE SUPPORT (LOGICIEL)";
            SSL.NavigateUri = new Uri("https://mnd-for-desktop.sidl-corporation.fr/");
        }

        private void SSA_Click(object sender, RoutedEventArgs e)
        {
            var SSAe = new HyperlinkButton();
            SSA.Content = "SITE SUPPORT (APPLICATION)";
            SSA.NavigateUri = new Uri("https://mnd-uwp.sidl-corporation.fr/");
        }

        private void M_ViewNetwork_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NetworkAppsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void M_ViewSetting_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsAppsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void M_ViewThemeAndColors_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ThemeAppsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void M_ViewSonds_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AudioAppsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}

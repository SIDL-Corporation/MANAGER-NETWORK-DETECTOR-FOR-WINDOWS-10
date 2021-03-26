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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MANAGER_NETWORK_DETECTOR_
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<RssSchema> RSSFeed { get; } = new ObservableCollection<RssSchema>();
        public MainPage()
        {
            this.InitializeComponent();
            // Set theme for window root
            FrameworkElement root = (FrameworkElement)Window.Current.Content;
            root.RequestedTheme = AppSettings.Theme;
            CustomizeTitleBar();
            ParseRSS();

            PI_NEWS.Header = "📢 Actualités";
        }

        public string Url { get; set; } = "https://sidl-corporation.tk/feed/";

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

        private void CustomizeTitleBar()
        {
            // Customisation de la bar de titre
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(customTitleBar);
        }

        private void BUTTON_SETTINGS_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomeSettings), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }    
}

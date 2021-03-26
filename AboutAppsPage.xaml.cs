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
using Microsoft.Toolkit.Uwp.Connectivity;
using Microsoft.Toolkit.Parsers.Rss;
using MANAGER_NETWORK_DETECTOR;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Globalization;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI;
using Windows.UI.Text;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Security.Authentication.Web.Core;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.Data.Json;
using Windows.Web.Http;
using Windows.Security.Credentials;
using Windows.Storage;
using Microsoft.Toolkit.Uwp.Helpers;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace MANAGER_NETWORK_DETECTOR
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AboutAppsPage : Page
    {
        private Color currentColor = Colors.Green;
        public AboutAppsPage()
        {
            this.InitializeComponent();
            CustomizeTitleBar();
            // Set theme for window root
            FrameworkElement root = (FrameworkElement)Window.Current.Content;
            root.RequestedTheme = AppSettings.Theme;
        }
        private void CustomizeTitleBar()
        {
            // Customisation de la bar de titre
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(customTitleBar);
        }

        private void BUTTON_BACK_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsAppsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            NAME_APPS.Text = "NOM : " + SystemInformation.ApplicationName;
            BUILD_APPS.Text = "VERSION : " + $"{SystemInformation.ApplicationVersion.Major}.{SystemInformation.ApplicationVersion.Minor}.{SystemInformation.ApplicationVersion.Build}.{SystemInformation.ApplicationVersion.Revision}";
            APPS_EDITOR.Text = "EDITEUR : SIDL CORPORATION";
        }
    }
}

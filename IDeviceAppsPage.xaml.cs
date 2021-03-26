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
using Microsoft.Toolkit.Parsers.Rss;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Globalization;
using Windows.UI.Xaml.Media.Animation;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Security.Authentication.Web.Core;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.Data.Json;
using Windows.Web.Http;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Runtime.Serialization;
using MANAGER_NETWORK_DETECTOR;
using Microsoft.Toolkit.Uwp.Helpers;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace MANAGER_NETWORK_DETECTOR
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class IDeviceAppsPage : Page
    {
        public IDeviceAppsPage()
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
            TXT_MARQUE.Text = "Fabricant de l'appareil / Marque : " + SystemInformation.DeviceManufacturer;
            TXT_CULTURE.Text = "Langue du système : " + SystemInformation.Culture;
            TXT_ARCHITECTURE.Text = "Architecture du système : " + SystemInformation.OperatingSystemArchitecture;
            TXT_MODEL.Text = "Modèle de l'appareil : " + SystemInformation.DeviceModel;
            TXT_UPDATE_APP.Text = SystemInformation.IsAppUpdated ? "L'application est-elle mise à jour : Oui, sous la version " + $"{SystemInformation.ApplicationVersion.Major}.{SystemInformation.ApplicationVersion.Minor}.{SystemInformation.ApplicationVersion.Build}.{SystemInformation.ApplicationVersion.Revision}" : "L'application est-elle mise à jour : Non...";
        }

        private async void ShowInfoMarqueDialogButton_Click(object sender, RoutedEventArgs e)
        {
            InfoMarqueContentDialog.Visibility = Visibility.Visible;
            ContentDialogResult result = await InfoMarqueContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                // Terms of use were accepted.
            }
            else
            {
                // User pressed Cancel, ESC, or the back arrow.
                // Terms of use were not accepted.
            }
        }

        private void InfoMarqueContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            // Ensure that the check box is unchecked each time the dialog opens.
        }
    }
}
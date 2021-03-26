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
using Microsoft.Toolkit.Uwp.Connectivity;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace MANAGER_NETWORK_DETECTOR
{
    public sealed partial class ThemeAppsPage : Page
    {
        private Color currentColor = Colors.Green;
        public ThemeAppsPage()
        {
            this.InitializeComponent();
            CustomizeTitleBar();
            // Set theme for window root
            FrameworkElement root = (FrameworkElement)Window.Current.Content;
            root.RequestedTheme = AppSettings.Theme;
            SetThemeToggle(AppSettings.Theme);
        }

        private void CustomizeTitleBar()
        {
            // Customisation de la bar de titre
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(customTitleBar);
        }

        /// <summary>
        /// Set the theme toggle to the correct position (off for the default theme, and on for the non-default).
        /// </summary>
        private void SetThemeToggle(ElementTheme theme)
        {
            if (theme == AppSettings.DEFAULTTHEME)
                tglAppTheme.IsOn = false;
            else
                tglAppTheme.IsOn = true;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            FrameworkElement window = (FrameworkElement)Window.Current.Content;

            if (((ToggleSwitch)sender).IsOn)
            {
                AppSettings.Theme = AppSettings.NONDEFLTHEME;
                window.RequestedTheme = AppSettings.NONDEFLTHEME;
                Grid_Them_View_dark.Visibility = Visibility.Collapsed;
                Grid_Them_View_light.Visibility = Visibility.Visible;
            }
            else
            {
                AppSettings.Theme = AppSettings.DEFAULTTHEME;
                window.RequestedTheme = AppSettings.DEFAULTTHEME;
                Grid_Them_View_dark.Visibility = Visibility.Visible;
                Grid_Them_View_light.Visibility = Visibility.Collapsed;
            }
        }

        private void BUTTON_BACK_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsAppsPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private async void BTN_SETTINGS_COLORS_SYSTEM_Click(object sender, RoutedEventArgs e)
        {
            bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:personalization-colors"));
        }
    }
}

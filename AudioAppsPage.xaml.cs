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


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace MANAGER_NETWORK_DETECTOR
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AudioAppsPage : Page
    {
        private Color currentColor = Colors.Green;
        public AudioAppsPage()
        {
            this.InitializeComponent();
            CustomizeTitleBar();
            // Set theme for window root
            FrameworkElement root = (FrameworkElement)Window.Current.Content;
            root.RequestedTheme = AppSettings.Theme;
            ElementSoundPlayer.State = ElementSoundPlayerState.On;
            SLIDEVOLUM.Value = ElementSoundPlayer.Volume * 100;
            if (ElementSoundPlayer.State == ElementSoundPlayerState.On)
                TOGGLEAUDIO.IsOn = true;
            if (ElementSoundPlayer.SpatialAudioMode == ElementSpatialAudioMode.On && TOGGLEAUDIO.IsOn == true)
                CECKBSPATIAL.IsChecked = true;
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

        private void TOGGLEAUDIO_Toggled(object sender, RoutedEventArgs e)
        {
            if (TOGGLEAUDIO.IsOn == true)
            {
                CECKBSPATIAL.IsEnabled = true;
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
                SLIDEVOLUM.IsEnabled = true;
                VOLUM_I_TXT.Text = "VOLUME INTEGRER ( MODIFIABLE )";
                soundSelection.IsEnabled = true;
                BTN_TESTEAUDIO.IsEnabled = true;
            }
            else
            {
                CECKBSPATIAL.IsEnabled = false;
                CECKBSPATIAL.IsChecked = false;

                ElementSoundPlayer.State = ElementSoundPlayerState.Off;
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
                SLIDEVOLUM.IsEnabled = false;
                VOLUM_I_TXT.Text = "VOLUME INTEGRER ( DESACTIVER )";
                soundSelection.IsEnabled = false;
                BTN_TESTEAUDIO.IsEnabled = false;
            }
        }

        private void CECKBSPATIAL_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TOGGLEAUDIO.IsOn == true)
            {
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
                SLIDEVOLUM.IsEnabled = true;
                VOLUM_I_TXT.Text = "VOLUME INTEGRER ( MODIFIABLE )";
            }
        }

        private void CECKBSPATIAL_Checked(object sender, RoutedEventArgs e)
        {
            if (TOGGLEAUDIO.IsOn == true)
            {
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.On;
                SLIDEVOLUM.IsEnabled = false;
                VOLUM_I_TXT.Text = "VOLUME INTEGRER ( VERROUILLER )";
            }
        }

        private void SLIDEVOLUM_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            string msg = String.Format("{0}%", e.NewValue);
            this.TXTSTATUSVOLUM.Text = msg + "%";

            Slider slider = sender as Slider;
            double volumeLevel = slider.Value / 100;
            ElementSoundPlayer.Volume = volumeLevel;
        }

        private void BTN_TESTEAUDIO_Click(object sender, RoutedEventArgs e)
        {
            ElementSoundPlayer.Play((ElementSoundKind)soundSelection.SelectedIndex);
        }
    }
}

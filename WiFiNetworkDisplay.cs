using System;
using System.Threading.Tasks;
using Windows.Devices.WiFi;

namespace MANAGER_NETWORK_DETECTOR
{
    internal class WiFiNetworkDisplay
    {
        private WiFiAvailableNetwork network;
        private WiFiAdapter firstAdapter;

        public WiFiNetworkDisplay(WiFiAvailableNetwork network, WiFiAdapter firstAdapter)
        {
            this.network = network;
            this.firstAdapter = firstAdapter;
        }

        internal Task UpdateConnectivityLevel()
        {
            throw new NotImplementedException();
        }
    }
}
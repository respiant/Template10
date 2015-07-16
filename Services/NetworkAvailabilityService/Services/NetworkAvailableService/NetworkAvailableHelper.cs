﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace Templaet10.Services.NetworkAvailableService
{
    public class NetworkAvailableHelper
    {
        public NetworkAvailableHelper()
        {
            NetworkInformation.NetworkStatusChanged += async (s) =>
            {
                var available = await this.IsInternetAvailable();
                if (AvailabilityChanged != null)
                {
                    try { AvailabilityChanged(available); }
                    catch { }
                }
            };
        }

        public Action<bool> AvailabilityChanged { get; set; }

        public async Task<bool> IsInternetAvailable()
        {
            await Task.Delay(0);
            var _Profile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            if (_Profile == null)
                return false;
            var net = Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess;
            return _Profile.GetNetworkConnectivityLevel().Equals(net);
        }
    }
}

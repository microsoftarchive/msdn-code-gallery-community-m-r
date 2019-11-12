//  ------------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation
//  All rights reserved. 
//  
//  Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this 
//  file except in compliance with the License. You may obtain a copy of the License at 
//  http://www.apache.org/licenses/LICENSE-2.0  
//  
//  THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
//  EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
//  CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR 
//  NON-INFRINGEMENT. 
// 
//  See the Apache Version 2.0 License for specific language governing permissions and 
//  limitations under the License.
//  ------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Net;

using AmqpSendReceive;

using Microsoft.SPOT.Time;

namespace Microsoft.Samples.ObdRecorder
{
    public class TimeHelper
    {

        bool _bTimeUpdated = false;
        bool _bTimeUpdating = false;

        ILogger Logger = null;

        public TimeHelper(ILogger logger)
        {
            Logger = logger;
        }

        public bool UpdateTime()
        {
            if (!_bTimeUpdated && !_bTimeUpdating)
            {
                Thread t = new Thread(GetTimeFromTimeServer);
                t.Start();
            }
            return _bTimeUpdated;
        }

        void GetTimeFromTimeServer()
        {
            try
            {
                _bTimeUpdating = true;

                // Wait for DHCP (on LWIP devices)
                do
                {
                    IPAddress ip = IPAddress.GetDefaultLocalAddress();

                    if (ip != IPAddress.Any)
                    {
                        Logger.TraceLog("Network ready. IP address: " + ip.ToString());
                        break;
                    }
                    Logger.TraceLog("Waiting for IP address");
                    Thread.Sleep(3000);
                }
                while (true);

                var timeServerDNSName = "time.nist.gov";
                Logger.TraceLog("Time: Requesting IP addr for  " + timeServerDNSName);
                var timeServer = Dns.GetHostEntry(timeServerDNSName);

                // The Emulator seems to return NULL addresses: filter those out
                IPAddress[] validAddresses = new IPAddress[timeServer.AddressList.Length];
                int i = 0;
                foreach (var address in timeServer.AddressList)
                {
                    if (address != null)
                    {
                        validAddresses[i] = address;
                        i++;
                    }
                }

                Logger.TraceLog("Time: Obtained IP addr for " + timeServerDNSName + ": " + validAddresses[0].ToString());

                TimeServiceSettings s = new TimeServiceSettings();

                s.PrimaryServer = validAddresses[0].GetAddressBytes();
                if (i > 1)
                {
                    s.AlternateServer = validAddresses[1].GetAddressBytes();
                }

                s.ForceSyncAtWakeUp = true;
                s.RefreshTime = 12 * 60 * 60; // 12 hours
                TimeService.Settings = s;

                //TimeService.Start();
                //TimeService.UpdateNow(timeServer.AddressList[0].GetAddressBytes(), 1000);
                //TimeService.SystemTimeChanged += new SystemTimeChangedEventHandler(TimeService_SystemTimeChanged);
                //TimeService.TimeSyncFailed += new TimeSyncFailedEventHandler(TimeService_TimeSyncFailed);

                TimeService.UpdateNow(1000);
                _bTimeUpdated = true;
                Logger.TraceLog("Time: Obtained from " + timeServerDNSName);
            }
            catch (Exception ex2)
            {
                Logger.TraceLog("Error getting time: " + ex2.Message);
            }
            finally
            {
                _bTimeUpdating = false;
            }

        }

        void TimeService_TimeSyncFailed(object sender, TimeSyncFailedEventArgs e)
        {
            Logger.TraceLog("Error: Time sync failed " + e.ErrorCode + " - " + e.EventTime);
        }

        void TimeService_SystemTimeChanged(object sender, SystemTimeChangedEventArgs e)
        {
            Logger.TraceLog("Time: System Time Changed: " + e.EventTime);
        }


    }
}
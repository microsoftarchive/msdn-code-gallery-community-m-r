/* 
 * NtpClient.cs
 * 
 * Copyright (c) 2009, Michael Schwarz (http://www.schwarz-interactive.de)
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR
 * ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 * MS   09-02-16    added NtpClient
 * 
 * 
 */

namespace Microsoft.ServiceBus.Micro
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    ///   Static class to receive the time from a NTP server.
    /// </summary>
    public class NtpClient
    {
        /// <summary>
        ///   Gets the current DateTime from time-a.nist.gov.
        /// </summary>
        /// <returns>A DateTime containing the current time.</returns>
        public static DateTime GetNetworkTime()
        {
            return GetNetworkTime("time.windows.com");
        }

        /// <summary>
        ///   Gets the current DateTime from <paramref name = "ntpServer" />.
        /// </summary>
        /// <param name = "ntpServer">The hostname of the NTP server.</param>
        /// <returns>A DateTime containing the current time.</returns>
        public static DateTime GetNetworkTime(string ntpServer)
        {
            var address = Dns.GetHostEntry(ntpServer).AddressList;

            if (address == null ||
                address.Length == 0)
                throw new ArgumentException("Could not resolve ip address from '" + ntpServer + "'.", "ntpServer");

            var ep = new IPEndPoint(address[0], 123);
            return GetNetworkTime(ep);
        }

        /// <summary>
        ///   Gets the current DateTime form <paramref name = "ep" /> IPEndPoint.
        /// </summary>
        /// <param name = "ep">The IPEndPoint to connect to.</param>
        /// <returns>A DateTime containing the current time.</returns>
        public static DateTime GetNetworkTime(IPEndPoint ep)
        {
            var ntpData = new byte[48]; // RFC 2030 
            ntpData[0] = 0x1B;

            using (var s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                s.ReceiveTimeout = 2000;
                s.Connect(ep);
                s.Send(ntpData);
                s.Receive(ntpData);
                s.Close();
            }

            const byte offsetTransmitTime = 40;
            ulong intpart = 0;
            ulong fractpart = 0;

            for (var i = 0; i <= 3; i++)
                intpart = 256*intpart + ntpData[offsetTransmitTime + i];

            for (var i = 4; i <= 7; i++)
                fractpart = 256*fractpart + ntpData[offsetTransmitTime + i];

            var milliseconds = (intpart*1000 + (fractpart*1000)/0x100000000L);
            var timeSpan = TimeSpan.FromTicks((long) milliseconds*TimeSpan.TicksPerMillisecond);
            var dateTime = new DateTime(1900, 1, 1);
            dateTime += timeSpan;
            return dateTime;
        }
    }
}
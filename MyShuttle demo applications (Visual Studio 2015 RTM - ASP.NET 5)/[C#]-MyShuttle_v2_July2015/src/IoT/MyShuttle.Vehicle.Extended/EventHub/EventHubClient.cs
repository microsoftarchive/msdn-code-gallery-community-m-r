

namespace MyShuttle.Vehicle.EventHub
{
    using ElzeKool;
    using Json.NETMF;
    using Microsoft.ServiceBus.Micro;
    using Microsoft.SPOT;
    using MyShuttle.Vehicle.Model;
    using System;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    public class EventHubClient
    {
        public static void SendEvent(MetricEvent info)
        {
            try
            {
                var caCerts = new X509Certificate[] { new X509Certificate(Resources.GetBytes(Resources.BinaryResources.Baltimore)) };

                var eventHubAddressHttps = "https://" + VehicleConfig.ServiceNamespace + ".servicebus.windows.net/" 
                    + VehicleConfig.HubName + "/publishers/" + VehicleConfig.DeviceId + "/messages";

                Debug.Print(eventHubAddressHttps);
                
                var request = (HttpWebRequest)HttpWebRequest.Create(eventHubAddressHttps);
                {
                    string sasToken = CreateSasToken(eventHubAddressHttps, VehicleConfig.KeyName, VehicleConfig.Key);
                    request.Headers.Add("Authorization", sasToken);
                    request.Headers.Add("ContentType", "application/atom+xml;type=entry;charset=utf-8");
                    request.Method = "POST";

                    request.HttpsAuthentCerts = caCerts;
                    request.KeepAlive = true;

                    string serializedObject = JsonSerializer.SerializeObject(info);
                    Debug.Print(serializedObject);

                    byte[] buffer = Encoding.UTF8.GetBytes(serializedObject);

                    request.ContentLength = buffer.Length;

                    // request body
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        Debug.Print("HTTP Status:" + response.StatusCode + " : " + response.StatusDescription);
                    }
                }
            }
            catch (WebException we)
            {
                Debug.Print(we.Message);
            }
        }

        static string CreateSasToken(string uri, string keyName, string key)
        {
            // Set token lifetime to 20 minutes. When supplying a device with a token, you might want to use a longer expiration time.
            uint tokenExpirationTime = GetExpiry(20 * 60);

            string stringToSign = HttpUtility.UrlEncode(uri) + "\n" + tokenExpirationTime;
            var hmac = SHA.computeHMAC_SHA256(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(stringToSign));

            string signature = Convert.ToBase64String(hmac);
            signature = Base64NetMf42ToRfc4648(signature);

            string token = "SharedAccessSignature sr=" + HttpUtility.UrlEncode(uri) + "&sig=" + HttpUtility.UrlEncode(signature) + "&se=" + tokenExpirationTime.ToString() + "&skn=" + keyName;
            return token;
        }

        static uint GetExpiry(uint tokenLifetimeInSeconds)
        {
            const long ticksPerSecond = 1000000000 / 100; // 1 tick = 100 nano seconds

            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = DateTime.Now.ToUniversalTime() - origin;
            return ((uint)(diff.Ticks / ticksPerSecond)) + tokenLifetimeInSeconds;
        }

        static string Base64NetMf42ToRfc4648(string base64netMf)
        {
            var base64Rfc = string.Empty;
            for (var i = 0; i < base64netMf.Length; i++)
            {
                if (base64netMf[i] == '!')
                {
                    base64Rfc += '+';
                }
                else if (base64netMf[i] == '*')
                {
                    base64Rfc += '/';
                }
                else
                {
                    base64Rfc += base64netMf[i];
                }
            }

            return base64Rfc;
        }

    }
}

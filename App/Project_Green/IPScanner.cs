using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using Project_Green.Models;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace Project_Green
{
    public class IPScanner
    {
        Regex regex = new Regex("^.+(?=\\.\\d+$)");
        IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());

        public List<Greenhouse> GetGreenhouses()
        {
            var address = regex.Match(addresses[0].ToString()).ToString();
            var ips = new List<string>();
            var Greenhouses = new List<Greenhouse>();
            for (int i = 2; i < 10; i++)
            {
                var host = $"{address}.{i}";

                Ping ping = new Ping();
                PingReply pingReply;

                try
                {
                    pingReply = ping.Send(host, 10);

                    if (pingReply != null && pingReply.Status == IPStatus.Success)
                    {
                        var FullUrl = $"http://{host}";
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(FullUrl);
                        request.Timeout = 100;
                        request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
                        request.Method = "HEAD";

                        var _ = request.GetResponse();

                        var test = new ArdunoRest.ArdunoRestClient
                        {
                            BaseUrl = FullUrl
                        };

                        var id = test.Id();
                        Greenhouses.Add(new Greenhouse
                        {
                            Greenhouse_ID = id.Id ?? 99,
                            Greenhouse_Name = id.Name,
                            Greenhouse_IP = host,
                            Greenhouse_Image = "Images/GreenhouseDefault.png"
                        });
                    }
                }
                catch
                { }
            }
            return Greenhouses;
        }

    }

}

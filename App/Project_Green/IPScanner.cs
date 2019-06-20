using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using Project_Green.Models;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Project_Green
{
    public class IPScanner
    {
        Regex regex = new Regex("^.+(?=\\.\\d+$)");
        IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
        List<Greenhouse> Greenhouses = new List<Greenhouse>();

        public List<Greenhouse> GetGreenhouses()
        {
            Greenhouses.Clear();
            var address = regex.Match(addresses[0].ToString()).ToString();
            var iplist = new List<string>();
            for (int i = 2; i < 10; i++)
                iplist.Add($"{address}.{i}");

            List<Thread> threads = new List<Thread>();
            foreach (var ip in iplist)
            {
                var tws = new ThreadWithState(
                    ip,
                    new ThreadCallback(ResultCallback)
                );

                Thread t = new Thread(new ThreadStart(tws.ThreadProc));
                threads.Add(t);
                t.Start();//start thread and pass it the port            
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return Greenhouses;
        }

        public void ResultCallback(Greenhouse gr)
        {
            Greenhouses.Add(gr);
        }
    }



    // The ThreadWithState class contains the information needed for
    // a task, the method that executes the task, and a delegate
    // to call when the task is complete.
    //
    public class ThreadWithState
    {
        // State information used in the task.
        private string host;

        // Delegate used to execute the callback method when the
        // task is complete.
        private ThreadCallback callback;

        // The constructor obtains the state information and the
        // callback delegate.
        public ThreadWithState(string h, ThreadCallback callbackDelegate)
        {
            host = h;
            callback = callbackDelegate;
        }

        // The thread procedure performs the task, such as
        // formatting and printing a document, and then invokes
        // the callback delegate with the number of lines printed.
        public void ThreadProc()
        {
            Ping ping = new Ping();
            PingReply pingReply;

            pingReply = ping.Send(host, 500);

            if (pingReply != null && pingReply.Status == IPStatus.Success)
            {
                try
                {
                    var FullUrl = $"http://{host}";
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(FullUrl);
                    request.Timeout = 500;
                    request.Method = "OPTIONS";
                    var _ = request.GetResponse();

                    var client = new ArdunoRest.ArdunoRestClient
                    {
                        BaseUrl = FullUrl
                    };

                    var id = client.Id();
                    callback(new Greenhouse
                    {
                        Greenhouse_ID = id.Id ?? 99,
                        Greenhouse_Name = id.Name,
                        Greenhouse_IP = host,
                        Greenhouse_Image = "Images/GreenhouseDefault.png"
                    });
                }
                catch { }
            }
        }
    }
    // Delegate that defines the signature for the callback method.
    //
    public delegate void ThreadCallback(Greenhouse gr);
}


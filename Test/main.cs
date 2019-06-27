using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using ArdunoRest;
using Project_Green;

namespace CliTest
{
    public class Program
    {
        public static int Main(String[] args)
        {
            /*var document = File.ReadAllText(@"TEST.TXT");
            var input = new StringReader(document);

            var deserializer = new DeserializerBuilder()
                .Build();

            var list = deserializer.Deserialize<List<Sensor>>(input);

            Console.WriteLine("Order");
            Console.WriteLine("-----");
            Console.WriteLine();
            foreach (var item in list)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}",
                    item.temperature,
                    item.humidity,
                    item.lightlevel,
                    item.moisture);
            }*/

            var rest = new ArdunoRestClient {BaseUrl = "http://192.168.1.2/"};

            Console.WriteLine(rest.Id().Name);

            var x = IPScanner.Instance.GetGreenhouses();
            
            Console.WriteLine("-----------");
            foreach (var greenhouse in x)
            {
                Console.WriteLine(greenhouse.Greenhouse_Name);
                Console.WriteLine(greenhouse.Greenhouse_IP);
            }
            Console.WriteLine("-----------");

            return 0;
        }

        /* public class Sensor
         {
             public string time { get; set; }
             public float temperature { get; set; }
             public float humidity { get; set; }
             public float lightlevel { get; set; }
             public int moisture { get; set; }
             
             public int waterlevel { get; set; }
         }*/
    }
}


namespace Project_Green
{
    public class Greenhouse
    {
        public int Greenhouse_ID { get; set; }

        public string Greenhouse_Name { get; set; }

        public string Greenhouse_Image { get; set; }

        public string Greenhouse_IP { get; set; }

        public float SettingsTemperatureSlider { get; set; }

        public float SettingsMoistureSlider { get; set; }
    }

    public class IPScanner
    {
        Regex regex = new Regex("^.+(?=\\.\\d+$)");
        //IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
        List<Greenhouse> Greenhouses = new List<Greenhouse>();

        public List<Greenhouse> GetGreenhouses()
        {
            Greenhouses.Clear();
            var address = regex.Match("192.168.1.5").ToString();//"192.168.1";
            var iplist = new List<string>();
            for (int i = 1; i < 5; i++)
                iplist.Add($"{address}.{i}");

            List<Thread> threads = new List<Thread>();
            foreach (var ip in iplist)
            {
                var tws = new ThreadWithState(
                    ip,
                    ResultCallback
                );

                Thread t = new Thread(tws.ThreadProc);
                threads.Add(t);
                t.Start(); //start thread and pass it the port            
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

        #region Singleton

        private static readonly Lazy<IPScanner> LazyIPScanner =
            new Lazy<IPScanner>(() => new IPScanner());

        public static IPScanner Instance => LazyIPScanner.Value;

        private IPScanner()
        {
        }

        #endregion
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
                    HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(FullUrl);
                    request.Timeout = 500;
                    request.Method = "OPTIONS";
                    var _ = request.GetResponse();

                    var client = new ArdunoRestClient
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
                catch
                {
                }
            }
        }
    }

    // Delegate that defines the signature for the callback method.
    //
    public delegate void ThreadCallback(Greenhouse gr);
}
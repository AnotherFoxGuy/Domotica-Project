using System;
using ArdunoRest;

namespace CliTest
{
    public class Program
    {
        public static int Main(String[] args)
        {
            var rest = new ArdunoRestClient();
            rest.BaseUrl = "http://192.168.1.2";
            
            Console.WriteLine(rest.Id().Name);
            
            return 0;
        }
    }
}
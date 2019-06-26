using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CliTest
{
    public class Program
    {
        public static int Main(String[] args)
        {
            var document = File.ReadAllText(@"TEST.TXT");
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
            }


            return 0;
        }

        public class Sensor
        {
            public string time { get; set; }
            public float temperature { get; set; }
            public float humidity { get; set; }
            public float lightlevel { get; set; }
            public int moisture { get; set; }
            
            public int waterlevel { get; set; }
        }
    }
}
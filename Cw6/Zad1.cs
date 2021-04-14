using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Cw6
{
    class Connection
    {
        public string Country { get; }
        public string Url { get;}
        public bool Pingable { get; set; }

        public Connection(string country, string url)
        {
            Country = country;
            Url = url;
            Pingable = false;
        }
    }
    
    class Zad1
    {

        public static async Task Main1(string[] args)
        {

            var test1Connections = ReadData();
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            PingTestSequential(test1Connections);
            sw1.Stop();
            foreach (var connection in test1Connections)
            {
                Console.WriteLine("Country: {0}, Url: {1}, Status:{2}", connection.Country, connection.Url, connection.Pingable);
            }
            Console.WriteLine("\nPing test sequential: {0}\n\n",sw1.Elapsed);

            var test2Connections = ReadData();
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            PingTestAsParaller(test2Connections);
            sw2.Stop();
            foreach (var connection in test2Connections)
            {
                Console.WriteLine("Country: {0}, Url: {1}, Status:{2}", connection.Country, connection.Url, connection.Pingable);
            }
            Console.WriteLine("\nPing test as paraller: {0}\n\n",sw2.Elapsed);

            var test3Connections = ReadData();
            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            await PingTestTask(test3Connections);
            sw3.Stop();
            foreach (var connection in test3Connections)
            {
                Console.WriteLine("Country: {0}, Url: {1}, Status:{2}", connection.Country, connection.Url, connection.Pingable);
            }
            Console.WriteLine("\nPing test task: {0}\n\n",sw3.Elapsed);
        }

        public static List<Connection> ReadData()
        {
            var connections = new List<Connection>();
            
            StreamReader sr = new StreamReader("../../../ping.txt");
            var line = sr.ReadLine();
            line = sr.ReadLine();
            while (line != null)
            {
                var country = line.Substring(0, line.IndexOf(';'));
                var url = line.Substring(line.IndexOf(';')+1);
                connections.Add(new Connection(country, url));
                line = sr.ReadLine();
            }
            sr.Close();

            return connections;
        }
        
        public static void PingTestSequential(List<Connection> connections)
        {
            var replies = connections.Select(PingHost);
            foreach (var repy in replies)
            {
                //Extend functionality here
            }
        }

        public static void PingTestAsParaller(List<Connection> connections)
        {
            var replies = connections.AsParallel().WithDegreeOfParallelism(4).Select(PingHost);
            foreach (var repy in replies)
            {
                //Extend functionality here
            }
        }
        
        public static async Task PingTestTask(List<Connection> connections)
        {
            var pingTasks = connections.Select(connection => new Ping().SendPingAsync(connection.Url, 500));
            var replies = await Task.WhenAll(pingTasks);
            int i = 0;
            foreach (var reply in replies)
            {
                connections[i].Pingable = reply.Status == IPStatus.Success;
                i++;
            }
        }

        public static bool PingHost(Connection connection)
        {
            Ping pinger = new Ping();
            var reply = pinger.Send(connection.Url, 500);
            var pingable = reply.Status == IPStatus.Success;
            connection.Pingable = pingable;
            return pingable;
        }
        
    }
}
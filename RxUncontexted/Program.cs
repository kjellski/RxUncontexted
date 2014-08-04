using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RxUncontexted.Parsing;

namespace RxUncontexted
{
    public class Program
    {
        public static void Main()
        {
            var p = new UncontextedWebsocketSubscriber();
            p.ConnectAndStartReceivingToWebSocket("ws://literature.uncontext.com:80");


            Console.WriteLine("Press <Enter> to close...");
            Console.ReadLine();
            Console.WriteLine("Exited.");
        }
    }
}
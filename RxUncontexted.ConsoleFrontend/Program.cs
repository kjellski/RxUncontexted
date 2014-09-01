using System;
using System.Reactive;
using RxUncontexted;
using RxUncontexted.Parsing;

namespace ReactiveConsoleFrontend
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var uncontextedDataProvider = new UncontextedWebsocketSubscriber("ws://literature.uncontext.com:80");

            var observer = Observer.Create<UncontextedData>(
                onNext: data => Console.WriteLine("OnNext: " + data),
                onError: error => Console.WriteLine("OnError: " + error.Message),
                onCompleted: () => Console.WriteLine("Completed."));

            uncontextedDataProvider.UncontextedData.Subscribe(observer);
            uncontextedDataProvider.ConnectAndStartReceivingToWebSocket();

            Console.WriteLine("Press <Enter> to exit...");
            Console.ReadLine();
        }
    }
}
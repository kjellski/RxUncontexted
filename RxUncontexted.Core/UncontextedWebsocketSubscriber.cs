using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using fastJSON;
using RxUncontexted.Parsing;

namespace RxUncontexted
{
    public class UncontextedWebsocketSubscriber
    {
        public readonly Subject<UncontextedData> UncontextedData = new Subject<UncontextedData>();
        private readonly string _wsLiteratureUncontextCom;

        public UncontextedWebsocketSubscriber(string wsLiteratureUncontextCom)
        {
            _wsLiteratureUncontextCom = wsLiteratureUncontextCom;
        }

        public async void ConnectAndStartReceivingToWebSocket()
        {
            ClientWebSocket webSocket = null;
            var uncontext = new Uri(_wsLiteratureUncontextCom);

            try
            {
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(uncontext, CancellationToken.None);
                await Receive(webSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
            finally
            {
                if (webSocket != null)
                {
                    webSocket.Dispose();
                    UncontextedData.OnCompleted();
                }
                Console.WriteLine("Closed websocket.");
            }
        }

        // Define other methods and classes here
        private async Task Receive(ClientWebSocket webSocket)
        {
            var buffer = new byte[64];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    DeserializeAndEnqueue(buffer, result.Count);
                }

                if(Console.KeyAvailable)
                    break;
            }
        }

        private void DeserializeAndEnqueue(IEnumerable<byte> buffer, int length)
        {
            var jsonString = Encoding.UTF8.GetString(buffer.Take(length).ToArray());
            var uncontexted = JSON.ToObject<UncontextedData>(jsonString,
                new JSONParameters
                {
                    EnableAnonymousTypes = true,
                    IgnoreCaseOnDeserialize = true,
                    UseUTCDateTime = true
                });
            UncontextedData.OnNext(uncontexted);
        }
    }
}
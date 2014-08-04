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
    public class UncontextedWebsocketSubscriber
    {
        public async void ConnectAndStartReceivingToWebSocket(String wsLiteratureUncontextCom)
        {
            ClientWebSocket webSocket = null;
            var uncontext = new Uri(wsLiteratureUncontextCom);

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
                    LogStatus(true, buffer, result.Count);
                }
            }
        }

        private void LogStatus(bool receiving, byte[] buffer, int length)
        {
            var jsonString = Encoding.UTF8.GetString(buffer.Take(length).ToArray());
            Console.WriteLine(jsonString);

            var uncontexted = JsonConvert.DeserializeObject<UncontextedData>(jsonString);
        }
    }
}
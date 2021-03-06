using System.Text;
using System.Net.WebSockets;
using MessagePack;
using CSharpExample_MD.Types.Config;
using CSharpExample_MD.Types;

namespace CSharpExample_MD.Examples
{
    class WebSocketExample
    {
        private ClientWebSocket _webSocket;
        private readonly string _connection;
        private readonly string _token;
        private readonly string _symbol;
        private readonly SubscriptionType _requestType;

        public WebSocketExample(string connection, string token, string symbol, SubscriptionType requestType)
        {
            _connection = connection.Replace("http", "ws");
            _token = token;
            _symbol = symbol;
            _requestType = requestType;
        }

        public async Task RunAsync()
        {
            await CreateSocket();

            var disconnectToken = new CancellationTokenSource();

            new Thread(async () => await StartWebSocket(disconnectToken.Token)).Start();

            Console.WriteLine("Press any key to close websocket");
            Console.ReadKey();
            disconnectToken.Cancel();
        }

        private async Task CreateSocket()
        {
            _webSocket = new ClientWebSocket();

            await _webSocket.ConnectAsync(new Uri($"{_connection}/{_symbol}/{_requestType}"), CancellationToken.None);
            var sendBuffer1 = Encoding.UTF8.GetBytes(_token);
            await _webSocket.SendAsync(new ArraySegment<byte>(sendBuffer1), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async Task StartWebSocket(CancellationToken killConnectionToken)
        {
            while (true)
            {
                var receiveBuffer = new byte[200_000];
                var buffer = new ArraySegment<byte>(new byte[1024 * 1]);
                var messageType = WebSocketMessageType.Text;

                try
                {
                    while (_webSocket.State == WebSocketState.Open)
                    {
                        var receiveSize = 0;
                        while (_webSocket.State == WebSocketState.Open)
                        {
                            var result = await _webSocket.ReceiveAsync(buffer, killConnectionToken);

                            messageType = result.MessageType;
                            Buffer.BlockCopy(buffer.Array, 0, receiveBuffer, receiveSize, result.Count);
                            receiveSize += result.Count;
                            if (result.EndOfMessage)
                                break;
                        }

                        if (messageType == WebSocketMessageType.Close)
                            break;

                        var messageArray = new byte[receiveSize];
                        Buffer.BlockCopy(receiveBuffer, 0, messageArray, 0, receiveSize);

                        // deserialize message on appropriate object, to use at the users whim
                        switch (_requestType)
                        {
                            case SubscriptionType.Book:
                                var bookObj = MessagePackSerializer.Deserialize<Book>(messageArray, cancellationToken: killConnectionToken);
                                break;
                            case SubscriptionType.BestOffers:
                                var bestOffersObj = MessagePackSerializer.Deserialize<BestOffers>(messageArray, cancellationToken: killConnectionToken);
                                break;
                            case SubscriptionType.Trades:
                                var tradesObj = MessagePackSerializer.Deserialize<Trade>(messageArray, cancellationToken: killConnectionToken);
                                break;
                            default:
                                break;
                        }

                        // print as json
                        Console.WriteLine(MessagePackSerializer.ConvertToJson(messageArray, cancellationToken: killConnectionToken));
                    }
                }
                catch (WebSocketException ex)
                {
                    Console.WriteLine($"Websocket Error {ex.StackTrace}");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("WebSocket disconnected, operation cancelled");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Receive WebSocket Message Error: {ex.StackTrace}");
                }

                if (killConnectionToken.IsCancellationRequested)
                    break;

                if (_webSocket.State != WebSocketState.Open)
                {
                    _webSocket.Abort();
                    await Task.Delay(1000, killConnectionToken);
                    await CreateSocket();
                }
            }
        }
    }
}

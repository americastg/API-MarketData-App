using System.Text.Json;
using CSharpExample_MD.Types.Config;
using CSharpExample_MD.Examples;
using CSharpExample_MD.Utils;

Config config;
using (StreamReader r = new("appsettings.json"))
{
    string json = r.ReadToEnd();
    config = JsonSerializer.Deserialize<Config>(json);
}
var token = await Util.GetAuthToken(config);

var symbol = "PETR4";
var requestType = SubscriptionType.Book;

await new WebSocketExample(config.BaseAddress, token, symbol, requestType).RunAsync();

Console.ReadLine();
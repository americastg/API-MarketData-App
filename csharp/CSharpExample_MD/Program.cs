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

var symbols = new List<string>() { "PETR4", "VALE3" }; // 200 symbols max
var updateFreq = 1; // integer, if not set, defaults to 500ms
var requestType = SubscriptionType.Book;

await new WebSocketExample(config.BaseAddress, token, symbols, updateFreq, requestType).RunAsync();

Console.ReadLine();
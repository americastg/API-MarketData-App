using CSharpExample_MD.Types.Config;
using IdentityModel.Client;

namespace CSharpExample_MD.Utils
{
    internal static class Util
    {
        public static async Task<string> GetAuthToken(Config config)
        {
            var client = new HttpClient();
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = config.TokenAddress,
                ClientId = config.ClientId,
                ClientSecret = config.ClientSecret,
                Scope = "mdapi4"
            });

            if (response.IsError) throw new Exception($"Erro: [{response.Error}] | Status: [{response.HttpStatusCode}]");

            var token = response.AccessToken.ToString();

            Console.WriteLine("Got access token");
            Console.WriteLine();

            return token;
        }
    }
}

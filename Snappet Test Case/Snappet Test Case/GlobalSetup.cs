using Snappet_Test_Case.Drivers;
using Snappet_Test_Case.Models;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Snappet_Test_Case
{
    [Binding]
    public static class GlobalSetup
    {
        [BeforeTestRun]
        public static async Task SetupClientAsync()
        {
            var uri = "https://boyns97jh5.execute-api.eu-west-1.amazonaws.com/Prod/api/v1/Client";
            var client = new ClientRequest()
            {
                name = "pineapple",
                secret = "salt"
            };
            string json = JsonSerializer.Serialize(client);
            var response = await RequestsClient.SendJsonPostRequestAsync(json, uri);
            var clientResponse = JsonSerializer.Deserialize<ClientResponse>(response);
            LibraryClient.ClientId = clientResponse;
            LibraryClient.ClientRequest = client;
        }
    }
}

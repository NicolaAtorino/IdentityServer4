using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer4Tests.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wait for the identity server and API to initialize and then press any button.");
            Console.ReadLine();

            Task.Run(() => AsyncMain()).GetAwaiter().GetResult();

            Console.ReadLine();
        }

        private static async Task AsyncMain()
        {
            var discoveries = await DiscoveryClient.GetAsync("http://localhost:5000");

            var tokenClient = new TokenClient(discoveries.TokenEndpoint, "IdentityServer4Tests.Client", "secret");

            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("IdentityServer4Tests.ApiResource");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            //call API
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity/get");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }



        }
    }
}

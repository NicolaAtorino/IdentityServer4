using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace IdentityServer4Tests.Client
{
    class Program
    {
        static ConsoleKey[] Options = new ConsoleKey[] {
            ConsoleKey.D1,
            ConsoleKey.NumPad1,
            ConsoleKey.D2,
            ConsoleKey.NumPad2
        };

        const string ClientCredentialsFlow = "ClientCredentials";
        const string ResourceOwnerFlow = "ResourceOwnerFlow";

        static void Main(string[] args)
        {
            Console.WriteLine("Wait for the identity server and API to initialize and then press any button.");
            Console.ReadKey();
            while (true)
            {
                MainMethod();
                Console.ReadLine();
            }
        }

        private static void MainMethod()
        {
            var key = ConsoleKey.NoName;

            while (!Options.Contains(key))
            {
                Console.WriteLine("Select the Flow");
                Console.WriteLine("1 - ClientCredentials");
                Console.WriteLine("2 - ResourceOwner");

                key = Console.ReadKey().Key;
            }

            string flow = (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1) ? ClientCredentialsFlow :
                          (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2) ? ResourceOwnerFlow :
                          null;

            Task.Run(() => ConnectToAPI(flow)).GetAwaiter().GetResult();
        }

        private static async Task ConnectToAPI(string flow)
        {

            string accessToken = null;

            switch (flow)
            {
                case ClientCredentialsFlow:
                    accessToken = await GetClientCredentialsAccessTokenAsync();
                    break;
                case ResourceOwnerFlow:
                    accessToken = await GetResourceOwnerAccessTokenAsync();
                    break;
                default:
                    throw new NotImplementedException();
            }

            //call API
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync("http://localhost:5001/identity/get");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }


            Console.ReadLine();
        }

        private static async Task<string> GetResourceOwnerAccessTokenAsync()
        {
            //var discoveries = await DiscoveryClient.GetAsync("http://localhost:5000");

            //var tokenClient = new TokenClient(discoveries.TokenEndpoint, "IdentityServer4Tests.Client", "secret");

            //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice","password","IdentityServer4Tests.ApiResource");

            //if (tokenResponse.IsError)
            //{
            //    throw new Exception(tokenResponse.Error);
            //}

            //return tokenResponse.AccessToken;
            return "test";
        }

        private static async Task<string> GetClientCredentialsAccessTokenAsync()
        {
            //var discoveries = await DiscoveryClient.GetAsync("http://localhost:5000");

            //var tokenClient = new TokenClient(discoveries.TokenEndpoint, "IdentityServer4Tests.Client", "secret");

            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("IdentityServer4Tests.ApiResource");

            //if (tokenResponse.IsError)
            //{
            //    throw new Exception(tokenResponse.Error);
            //}

            //return tokenResponse.AccessToken;

            return "test";
        }
    }
}

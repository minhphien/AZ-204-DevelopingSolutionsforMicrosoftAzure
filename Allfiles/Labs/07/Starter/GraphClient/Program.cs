using Microsoft.Graph;    
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GraphClient

{
    class Program
    {
        private const string _clientId = "ea3ba699-f167-4a39-a540-d1513b3f8a73";
        private const string _tenantId = "2aad2039-aa19-4fe4-9e6b-6c1ba0e20ebc";
        
        public static async Task Main(string[] args)
        {
            IPublicClientApplication app = PublicClientApplicationBuilder
                .Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost")
                .Build();

            List<string> scopes = new List<string> 
            { 
                "user.read"
            };

            DeviceCodeProvider provider = new DeviceCodeProvider(app, scopes);
            GraphServiceClient client = new GraphServiceClient(provider);
            User myProfile = await client.Me
                .Request()
                .GetAsync();

            Console.WriteLine($"Name:\t{myProfile.DisplayName}");
            Console.WriteLine($"AAD Id:\t{myProfile.Id}");
        }
    }
}

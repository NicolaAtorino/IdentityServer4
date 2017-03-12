using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Tests
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("IdentityServer4Tests.ApiResource","test API")
            };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //client credentials flow client
                new Client
                {
                    ClientId = "IdentityServer4Tests.Client",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = { "IdentityServer4Tests.ApiResource" }
                },

                ////resource owner client
                //new Client
                //{
                //    ClientId = "ro.client",
                //    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                //    ClientSecrets =
                //    {
                //        new Secret("secret".Sha256())
                //    },
                        
                //    AllowedScopes = { "IdentityServer4Tests.ApiResource" }
                //}
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser> {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
        }

    }
}

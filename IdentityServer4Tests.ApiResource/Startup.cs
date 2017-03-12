using IdentityServer3.AccessTokenValidation;
using IdentityServer4Tests.ApiResource;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace IdentityServer4Tests.ApiResource
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new IdentityServerBearerTokenAuthenticationOptions()
            {
                Authority = "http://localhost:5000",
                RequiredScopes = new[] { "IdentityServer4Tests.ApiResource" },
                //NameClaimType = ClaimTypes.Name,
                //RoleClaimType = ClaimTypes.Role
            };

            app.UseIdentityServerBearerTokenAuthentication(options);
        }


    }
}
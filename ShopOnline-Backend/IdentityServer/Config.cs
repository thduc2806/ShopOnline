using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline_Backend.IdentityServer
{
	public class Config
	{
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
             new ApiScope[]
             {
                  new ApiScope("shop.api", "Online Shop Api")
             };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // machine to machine client
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "shop.api" }
                },

                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44338/signin-oidc" },

                    PostLogoutRedirectUris = { "https://localhost:44338/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "shop.api"
                    }
                },

                new Client
                {
                    ClientId = "swagger",
                    ClientName = "Swagger Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,
					AllowAccessTokensViaBrowser = true,
					RequireConsent = false,
                    //RequirePkce = true,

                    RedirectUris =           { $"https://localhost:5001/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"https://localhost:5001/swagger/oauth2-redirect.html" },
                    AllowedCorsOrigins =     { $"https://localhost:5001" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "shop.api"
                    }
                },

               new Client {
                    ClientName = "admin",
                    ClientId = "admin",
                    // AccessTokenType = AccessTokenType.Reference,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    // RequirePkce = true,
                    AlwaysSendClientClaims = true,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:3000/authentication/login-callback",
                        "http://localhost:3000/silent-renew.html",
                        "http://localhost:3000"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:3000/authentication/logout-callback",
                        "http://localhost:3000/unauthorized",
                        "http://localhost:3000"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:3000"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "shop.api",
                    }
                }
            };
    }
}

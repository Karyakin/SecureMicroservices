using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "movieClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "movieAPI" }
                },
                new Client
                {
                    ClientId = "movies_mvc_client",
                    ClientName = "movies Mvc web App",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowRememberConsent = false,

                    // where to redirect to after login
                    RedirectUris =new List<string>()
                    {
                        "https://localhost:5002/signin-oidc" // this is client app port
                    },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = new List<string>()
                    {
                         "https://localhost:5002/signout-callback-oidc"
                    },

                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    //    ,"api1"
                    }
                }
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("movieAPI","movie API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {

            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()

            };
        public static List<TestUser> TestUsers =>
            new List<TestUser>()
            {
                new TestUser()
                {
                     SubjectId = "123",
                     Username = "Agent",
                     Password = "123",
                     Claims = new List<Claim>()
                     {
                         new Claim(JwtClaimTypes.GivenName,"Dmitry1"),
                         new Claim(JwtClaimTypes.FamilyName,"Karyakin")
                         ,new Claim(JwtClaimTypes.EmailVerified,"VovaMi6a@Gmail.com")
                         ,new Claim(JwtClaimTypes.Email,"VovaMi6a@Gmail.com")
                         ,new Claim(JwtClaimTypes.Address,"papapa")

                     }

                }
            };
    }
}

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {

        public static IEnumerable<Client> Clients => new Client[] { 
            new Client{
                ClientId ="movieClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { 
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "movieAPI" }
            },
             new Client{
                ClientId ="movies_mvc_client",
                ClientName = "Movie MVC web App",
                AllowedGrantTypes = GrantTypes.Hybrid,
                RequirePkce = false,
                AllowRememberConsent = false,
                RedirectUris =
                {
                    "https://localhost:5002/signin-oidc"
                },
                PostLogoutRedirectUris =
                {
                    "https://localhost:5002/signout-callback-oidc"
                },
                ClientSecrets = {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string>{ 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Address,
                    "movieAPI",
                    "roles"
                }
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Address(),
            new IdentityResources.Email(),
            new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>{ "role" }
                )
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {

        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
            new ApiScope("movieAPI","Movie API")
        };

        public static List<TestUser> TestUsers => new List<TestUser> {
            new TestUser{ 
                SubjectId ="adc011aa-4e2a-11ed-bdc3-0242ac120002",
                Username="younes",
                Password="younes",
                Claims =new List<Claim>{ 
                    new Claim(JwtClaimTypes.GivenName,"younes"),
                    new Claim(JwtClaimTypes.FamilyName,"reguieg")
                }

            }
        };

    }
}

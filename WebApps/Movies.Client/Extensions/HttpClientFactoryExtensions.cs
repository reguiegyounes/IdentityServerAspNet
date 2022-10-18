using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Movies.Client.HttpHandlers;
using System;

namespace Movies.Client.Extensions
{
    public static class HttpClientFactoryExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddTransient<AuthenticationDelegatingHandler>();
            services.AddHttpClient("MovieAPIClient",
                client =>
                {
                    client.BaseAddress=new Uri(configuration["Movies.API:Url"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                }
            ).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            services.AddHttpClient("ISClient",
                client =>
                {
                    client.BaseAddress=new Uri(configuration["oidc:authority_url"]);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                }
            );

            services.AddSingleton(new ClientCredentialsTokenRequest
            {
                Address=$"{configuration["oidc:authority_url"]}/connect/token",
                ClientId=configuration["Movies.API:ClientId"],
                ClientSecret=configuration["Movies.API:ClientSecret"],
                Scope= configuration["Movies.API:Scope"]
            });

            return services;
        }
    }
}

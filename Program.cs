using System;
using System.Net.Http.Headers;
using CloudLibrary.Controllers;
using CloudLibrary.lib;
using CloudLibrary.Models;
using LocalTest.Configuration;
using LocalTest.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LocalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Register for Di 
            IHost host = RegisterServices();

            var runner = new GetUserBlocks();
            runner.Run(host);
        }

        public static IHost RegisterServices()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Client factory: Typed Client
                    services.AddHttpClient<IApiHandler, ApiHandler>(c =>
                    {
                        c.BaseAddress = new Uri(Constants.ApiBaseUrl);
                        c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    });

                    services.AddTransient<ILocalConfiguration, LocalConfiguration>();
                    services.AddTransient<IAuthenticator, Authenticator>();
                    services.AddTransient<IBlockCatcher, BlockCatcher>();

                })
                .Build();

            return host;
        }
    }
}
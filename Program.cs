using CloudLibrary.Controllers;
using CloudLibrary.lib;
using CloudLibrary.Models;
using LocalTest.Configuration;
using LocalTest.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading;

namespace LocalTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //while (true)
            //{
            //    Stopwatch SpeedCounter = Stopwatch.StartNew();

            //    Thread.Sleep(1000);

            //    Console.WriteLine($"code speed: {SpeedCounter.ElapsedMilliseconds} milliseconds");
            //}



            //Register for Di
            IHost host = RegisterServices();

            var runner = new GetUserBlocks();
            runner.Run(host);

            //List<bool> test = new List<bool>() { true, (bool)("offer" == "offer") };
            //Console.WriteLine(test.All(element => element));

            //DynamoHandler.DeleteBlocksTable().Wait();

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
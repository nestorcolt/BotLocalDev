using CloudLibrary.Controllers;
using CloudLibrary.Lib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;

namespace LocalTest.Test
{
    class GetUserBlocks
    {
        private string _userId = "9";

        public void Run(IHost host)
        {
            // Catcher DI
            var catcher = ActivatorUtilities.CreateInstance<BlockCatcher>(host.Services);

            // Data to parse
            string userData = DynamoHandler.QueryUser(_userId).Result;
            UserDto userDto = JsonConvert.DeserializeObject<UserDto>(userData);
            userDto.TimeZone = "Eastern Standard Time";
            userDto.MinimumPrice = 1000000;

            while (true)
            {
                bool result = catcher.LookingForBlocks(userDto).Result;
                Console.WriteLine($"Iteration Result: {result}");

                if (!result)
                {
                    break;
                }

                // Wait!
                Thread.Sleep(2000);
            }
        }

    }
}

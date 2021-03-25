﻿using Microsoft.Extensions.Configuration;
using System.IO;

namespace LocalTest.Configuration
{
    public class LocalConfiguration : ILocalConfiguration
    {
        public static IConfigurationRoot Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        IConfigurationRoot ILocalConfiguration.Configuration => Configuration;
    }
}

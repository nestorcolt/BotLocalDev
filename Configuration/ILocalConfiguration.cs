using Microsoft.Extensions.Configuration;

namespace LocalTest.Configuration
{
    public interface ILocalConfiguration
    {
        IConfigurationRoot Configuration { get; }
    }
}

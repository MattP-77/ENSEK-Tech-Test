using Microsoft.Extensions.Configuration;

namespace ENSEK.Classes.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfiguration? config;
        public static void Initialize(IConfiguration Configuration)
        {
            config = Configuration;
        }
    }
}

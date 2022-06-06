using Hangfire;
using Newtonsoft.Json;

namespace MediatR_and_HangFire
{
    public static class HangfireExtensions
    {
        public static IGlobalConfiguration UseMediatR(this IGlobalConfiguration config)
        {
            config.UseSerializerSettings(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            return config;
        }
    }

}

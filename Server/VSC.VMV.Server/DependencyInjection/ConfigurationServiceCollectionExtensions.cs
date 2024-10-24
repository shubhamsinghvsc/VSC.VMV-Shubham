using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Sperry.ToolDataManager.Core.Common.Models;

namespace VSC.VMV.Server.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static void AddConfigurations(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<VMVDBConnectionInfo>(config.GetSection("Database"));
            services.TryAddSingleton(sp => sp.GetRequiredService<IOptions<VMVDBConnectionInfo>>().Value);
        }
    }
}

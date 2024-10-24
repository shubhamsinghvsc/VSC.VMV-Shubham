
using VSC.VMV.Common.Interfaces;
using VSC.VMV.Repository;

namespace VSC.VMV.Server.DependencyInjection
{
    public static class ResigterServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IHospitalManagementRepository, HospitalManagementRepository>();
            services.AddScoped<IPeopleManagementRepository, PeopleManagementRepository>();
            services.AddScoped<IMappingsRepository, MappingsRepository>();

            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}

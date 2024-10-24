
using VSC.VMV.Server.DependencyInjection;

namespace VSC.VMV.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://*:53010");

            builder.Host.ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                        .AddEnvironmentVariables();
            });

            builder.Services.AddConfigurations(builder.Configuration);
            builder.Services.RegisterServices(builder.Configuration);

            //if (Convert.ToBoolean(builder.Configuration["IsCloudDeployment"]))
            //{
            //    string ConnectionString = builder.Configuration["ApplicationInsightsConfiguration:ConnectionString"];
            //    builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions()
            //    {
            //        ConnectionString = builder.Configuration["ApplicationInsightsConfiguration:ConnectionString"]
            //    });
            //}

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

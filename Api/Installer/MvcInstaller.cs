using Api.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Api.Installer
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpContextAccessor()
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;

                    options.Filters.Add<ApiExceptionFilter>();
                })
                .AddJsonOptions(options => { options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}
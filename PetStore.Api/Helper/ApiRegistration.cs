using System.Reflection;

namespace PetStore.Api.Helper
{
    public static class ApiRegistration
    {
        public static IServiceCollection AddApiRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

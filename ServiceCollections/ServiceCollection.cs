using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using multi_tenant.DAO;
using multi_tenant.Services;

namespace multi_tenant.ServiceCollections
{
    public static partial class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services,
            IConfiguration Configuration)
        {
            //add services here
            
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<AuthDao>();
            
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<UserDao>();
            
            services.AddScoped<IPersonService,PersonService>();
            services.AddScoped<PersonDao>();

            services.AddScoped<LayananTenantDao>();
        }
    }
}
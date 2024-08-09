using Microsoft.Extensions.DependencyInjection;
using Store.Repositories;
using Store.Repositories.IRepository;
using Store.Repositories.Repository;


namespace Store.IOC
{
    public static class IOContiner
    {
        public static void ConfigureIOC(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ProductIRepository, ProductRepository>();
            services.AddScoped<AcayearIRepository, AcadyearRepository>();
            services.AddScoped<CustomerIRepository, CustomerRepository>();
            services.AddScoped<OrderIRepository, OrderRepository>();
            services.AddScoped<RoleIRepository, RoleRepository>(); 
            services.AddScoped<ModuleIRepository, ModuleRepository>();
            services.AddScoped<RoleuserIRepository, RoleUserRepository>();
            services.AddScoped<RoleModuleIRepository, RoleModuleRepository>();






        }
    }
}

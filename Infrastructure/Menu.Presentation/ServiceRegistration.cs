using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Menu.Application.Absrtacts.Services;




using Menu.Persistence.Concreters.Services;
using Menu.Presentation.Concreters.Repositories.Categorys;
using Menu.Application.Absrtacts.Repositories.Categorys;
using Menu.Application.Abstracts.Services;
using Menu.Presentation.Concreters.Services;
using Menu.Infrastructure.Concreters.Services;
using Menu.Application.Abstracts.Repositories.Products;
using Menu.Presentation.Concreters.Repositories.Products;



namespace Menu.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IFileService,FileService>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<IProductReadRepository,ProductReadRepository>();
            services.AddScoped<IProductWriteRepository,ProductWriteRepository>();

         

            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        }
    }
}

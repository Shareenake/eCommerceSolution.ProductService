

using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.RepositoryContract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        opt.UseMySQL(config.GetConnectionString("DefaultConnection")!));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}

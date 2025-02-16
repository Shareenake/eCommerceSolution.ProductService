    

using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.Repositories;
using eCommerce.DataAccessLayer.RepositoryContract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace eCommerce.ProductService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services,IConfiguration config)
    {
        //services.AddDbContext<ApplicationDbContext>(opt =>
        //opt.UseMySQL(config.GetConnectionString("DefaultConnection")!));

        string ConnectionStringTemplate = config.GetConnectionString("DefaultConnection")!;
        string connectionString = ConnectionStringTemplate
            .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
            .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"))
            .Replace("$MYSQL_USERNAME", Environment.GetEnvironmentVariable("MYSQL_USERNAME"))
            .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT"))
            .Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE"));
        services.AddDbContext<ApplicationDbContext>
            (opt =>
            {
                opt.UseMySQL(connectionString);

            });

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}

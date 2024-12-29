

using eCommerce.BusinessLogicLayer.Mappers;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using Microsoft.Extensions.DependencyInjection;
using eCommerce.BusinessLogicLayer.Services;
using FluentValidation;
using BusinessLogicLayer.Validators;

namespace eCommerce.ProductService.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        //TO DO: Add business logic layer into the IOC container
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidators>();
        services.AddScoped<IProductService, eCommerce.BusinessLogicLayer.Services.ProductService>();
        return services;
    }
}

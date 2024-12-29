using BusinessLogicLayer.Validators;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ProductMicroService.API.ApiEndpoints;

public static class ProductApiEndpoints
{
    public static IEndpointRouteBuilder MapProductApiEndpoints(this IEndpointRouteBuilder app)
    {
        //GET  /api/products
        app.MapGet("/api/products", async (IProductService productService) =>
        {
            List<ProductResponse?> products=await productService.GetProducts();
            return Results.Ok(products);
        });


        //GET  /api/products/search/product-id/000000000-0000-0000-0000-000000000
        app.MapGet("/api/products/search/product-id/{ProductID:guid}", async (IProductService productService,Guid ProductID) =>
        {
            ProductResponse? product = await productService.GetProductByCondition(x=>x.ProductID==ProductID);
            return Results.Ok(product);
        });

        //GET  /api/products/search/xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        app.MapGet("/api/products/search/{searchString}", async (IProductService productService, string searchString) =>
        {
            List<ProductResponse?> productByProductName = await productService.GetProductsByCondition(x =>
            x.ProductName!=null && x.ProductName.Contains
            (searchString,StringComparison.OrdinalIgnoreCase));

            List<ProductResponse?> productByCategory = await productService.GetProductsByCondition(x =>
            x.Category != null && x.Category.Contains
            (searchString, StringComparison.OrdinalIgnoreCase));

            var products= productByProductName.Union(productByCategory);


            return Results.Ok(products);
        });

        //POST  /api/products/
        app.MapPost("/api/products", async (IProductService productService, ProductAddRequest productAddRequest,
            IValidator<ProductAddRequest> productAddRequestValidator) =>
        {
            ValidationResult validationResult= await productAddRequestValidator.ValidateAsync(productAddRequest);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(tmp => tmp.PropertyName)
                .ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
            }

                var addedProductResponse = await productService.AddProduct(productAddRequest);
                if (addedProductResponse != null)
                {
                    return Results.Created($"/api/products/search/product-id/{addedProductResponse.ProductID}", addedProductResponse);
                }
                else
                    return Results.Problem("Error in adding Product");
        });

        //PUT  /api/products/
        app.MapPut("/api/products", async (IProductService productService, ProductUpdateRequest productUpdateRequest,
            IValidator<ProductUpdateRequest> productUpdateRequestValidator) =>
        {
            ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(tmp => tmp.PropertyName)
                .ToDictionary(grp => grp.Key, grp => grp.Select(err => err.ErrorMessage).ToArray());
            }

            var updatedProductResponse = await productService.UpdateProduct(productUpdateRequest);
            if (updatedProductResponse != null)
            {
                return Results.Ok(updatedProductResponse);
            }
            else
                return Results.Problem("Error in Updating Product");
        });

        //DELETE  /api/products/000000000-0000-0000-0000-000000000
        app.MapDelete("/api/products/{ProductID:guid}", async (IProductService productService, Guid ProductID) =>
        {
            bool isDeleted = await productService.DeleteProduct(ProductID);
            if (isDeleted)
            {
                return Results.Ok(true);
            }
            else
                return Results.Problem("Error in Deleting Product");
        });



        return app;
    }
}

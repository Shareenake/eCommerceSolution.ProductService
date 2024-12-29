

using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.ServiceContracts;

public interface IProductService
{
    /// <summary>
    /// Retrieves the lists of products from the Product repository
    /// </summary>
    /// <returns></returns>
    Task<List<ProductResponse?>>GetProducts();
    /// <summary>
    /// Retrieves products based on the condition
    /// </summary>
    /// <param name="conditionexpression">Expression that represents condition to check</param>
    /// <returns>Returns matching Products</returns>
    Task<List<ProductResponse?>>GetProductsByCondition(Expression<Func<Product, bool>> conditionexpression);
    /// <summary>
    /// Retrieve the product based on the condition from the repository
    /// </summary>
    /// <param name="conditionexpression">Expression that represents condition to check</param>
    /// <returns>Returns matching Product</returns>
    Task<ProductResponse?>GetProductByCondition(Expression<Func<Product, bool>> conditionexpression);
    /// <summary>
    /// Add Product into the Products table using Product repository
    /// </summary>
    /// <param name="product">Product to be added</param>
    /// <returns>Returns product object if successful otherwise null</returns>
    Task<ProductResponse?>AddProduct(ProductAddRequest productAddRequest);
    /// <summary>
    /// Update Product in Products table based on the given ProductId
    /// </summary>
    /// <param name="">Product to be updated</param>
    /// <returns>Returns product object if successful otherwise null</returns>
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);
    /// <summary>
    /// Deletes the Product based o9n the ProductId
    /// </summary>
    /// <param name="ProductId">Product Id to Search the Product to be deleted</param>
    /// <returns>Returns true if deleted otherwise false</returns>
    Task<bool>DeleteProduct(Guid ProductId);
}

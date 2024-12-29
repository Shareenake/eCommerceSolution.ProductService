

using eCommerce.DataAccessLayer.Entities;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.RepositoryContract;

/// <summary>
/// Represents repository for managing Products table 
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Retrieves all products asynchronously
    /// </summary>
    /// <returns>Retrieves all products from the product table</returns>
    Task<IEnumerable<Product>> GetProducts();

    /// <summary>
    /// Retrieves all the products based on the condition
    /// </summary>
    /// <param name="conditionExpression">Condition to filter the products</param>
    /// <returns>Returns all, the matching products otherwise null</returns>
    Task<IEnumerable<Product?>>GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// Retrieves the product based on the condition
    /// </summary>
    /// <param name="conditionExpression">Condition to filter the product</param>
    /// <returns>Returns all, the matching product otherwise null</returns>
    Task<Product?>GetProductByCondition(Expression<Func<Product, bool>>conditionExpression);
    /// <summary>
    /// Adds new product into the table asynchronously
    /// </summary>
    /// <param name="product">the product to be added</param>
    /// <returns>Returns the added product object or null if unsuccessful</returns>
    Task<Product?> AddProduct(Product product);
    /// <summary>
    /// Updates an existing product asynchronously
    /// </summary>
    /// <param name="product">The product to be updated</param>
    /// <returns>Return the updated object or null if unsuccessful</returns>
    Task<Product?> UpdateProduct(Product product);
    /// <summary>
    /// Delete the product asynchronously
    /// </summary>
    /// <param name="productID">The product to be deleted</param>
    /// <returns>Returns true if deleted or false if unsuccessful</returns>
    Task<bool>DeleteProduct(Guid productID);
}

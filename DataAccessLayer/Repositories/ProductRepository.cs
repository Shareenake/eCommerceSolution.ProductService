
using eCommerce.DataAccessLayer.Context;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.DataAccessLayer.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Product?> AddProduct(Product product)
    {
         _dbContext.Products.Add(product);
          await _dbContext.SaveChangesAsync();
         return product;

    }

    public async Task<bool> DeleteProduct(Guid productID)
    {
        Product? existingProduct =await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductID == productID);
        if (existingProduct == null)
        {
            return false;
        }
        else
        {
            _dbContext.Products.Remove(existingProduct);
            int affectedRowCount = await _dbContext.SaveChangesAsync();
            return affectedRowCount > 0;
        }
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        Product? existingProduct = await _dbContext.Products.FirstOrDefaultAsync(conditionExpression);
        return existingProduct;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        return await _dbContext.Products.Where(conditionExpression).ToListAsync();
    }

    public async Task<Product?> UpdateProduct(Product product)
    {
        Product? existingProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductID ==product.ProductID);
        if (existingProduct == null)
        {
            return null;
        }
        else
        {
            existingProduct.ProductName=product.ProductName;
            existingProduct.QuantityInstock = product.QuantityInstock;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.Category= product.Category;

            await _dbContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}


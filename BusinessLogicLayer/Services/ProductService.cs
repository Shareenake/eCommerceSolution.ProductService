

using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContract;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace eCommerce.BusinessLogicLayer.Services;

public class ProductService : IProductService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IValidator<ProductAddRequest> productAddRequestValidator,
        IValidator<ProductUpdateRequest> productUpdateRequestValidator,
        IMapper mapper,
        IProductRepository productRepository)
    {
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _mapper=mapper;
        _productRepository=productRepository;
    }
    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if(productAddRequest==null)
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }
        //Validate the Product using Fluent validation
        ValidationResult validationResult=await _productAddRequestValidator.ValidateAsync(productAddRequest);

        if(!validationResult.IsValid)
        {
            string errors = string.Join(",",
                validationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(errors);
        }

        Product productInput=_mapper.Map<Product>(productAddRequest);
        Product? addedProduct = await _productRepository.AddProduct(productInput);
        if (addedProduct == null)
        {
            return null;
        }
        ProductResponse addedProductResponse = _mapper.Map<ProductResponse>(addedProduct);

        return addedProductResponse;
    }

    public async Task<bool> DeleteProduct(Guid ProductId)
    {
        Product? existingProduct =await _productRepository.GetProductByCondition(x => x.ProductID == ProductId);
        if(existingProduct==null)
        {
            return false;
        }
        bool isDeleted=await _productRepository.DeleteProduct(ProductId);
        return isDeleted;

    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionexpression)
    {
        Product? product =await _productRepository.GetProductByCondition(conditionexpression);
        if(product==null)
        {
            return null;
        }
        ProductResponse productResponse = _mapper.Map<ProductResponse>(product);
        return productResponse;
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        IEnumerable<Product?> products=await _productRepository.GetProducts();
        
        IEnumerable<ProductResponse?> productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);
        return productResponses.ToList();
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionexpression)
    {

        IEnumerable<Product?> products = await _productRepository.GetProductsByCondition(conditionexpression);

        IEnumerable<ProductResponse?> productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);
        return productResponses.ToList();
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        Product? existingProduct=await _productRepository.GetProductByCondition(tmp=>tmp.ProductID==productUpdateRequest.ProductID);
        if(existingProduct==null)
        {
            throw new ArgumentException("Invalid Product ID");
        }
        ValidationResult validationResult=await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
        if (!validationResult.IsValid)
        {
            string errors=string.Join(", ", 
                validationResult.Errors.Select(tmp=>tmp.ErrorMessage));
            throw new ArgumentNullException(errors);
        }

        Product? product = _mapper.Map<Product>(productUpdateRequest);
        Product? updatedProduct=await _productRepository.UpdateProduct(product);
        if (updatedProduct == null)
        {
            return null;
        }
        ProductResponse productResponse = _mapper.Map<ProductResponse>(updatedProduct);
        return productResponse;

    }
}

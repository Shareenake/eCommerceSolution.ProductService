
using FluentValidation;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.DataAccessLayer.Entities;


namespace BusinessLogicLayer.Validators;

public class ProductAddRequestValidators:AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidators()
    {
        //ProductName
        RuleFor(temp => temp.ProductName)
            .NotEmpty().WithMessage("Product name cannot be blank");
        //Category
        RuleFor(temp => temp.Category)
            .NotEmpty().WithMessage("Category name cannot be blank");
        //Unit Price
        RuleFor(temp => temp.UnitPrice)
            .InclusiveBetween(0, double.MaxValue).WithMessage($"Unit Price " +
            $"should be between 0 to {double.MaxValue}");
        //Quantity in stock
        RuleFor(temp => temp.QuantityInStock)
            .InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in stock " +
            $"should be between 0 to {int.MaxValue}");
    }
}

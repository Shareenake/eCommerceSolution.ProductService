
using System.ComponentModel.DataAnnotations;

namespace eCommerce.DataAccessLayer.Entities;

public class Product
{
    [Key]
    public Guid ProductID { get; set; }
    [Required]
    public string ProductName { get; set; }
    public string Category { get; set; }
    public double? UnitPrice { get; set; }
    public int? QuantityInstock { get; set; }
}

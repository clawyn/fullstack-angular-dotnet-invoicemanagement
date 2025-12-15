
using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.API.Dtos.Product
{
    public class ProductFormDto
    {
        [MaxLength(100)]
        [Required]
        public string ProductName { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Specification { get; set; }
        public decimal Price { get; set; }
    }
}
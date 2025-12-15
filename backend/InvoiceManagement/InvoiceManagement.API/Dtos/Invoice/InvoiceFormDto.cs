using System.ComponentModel.DataAnnotations;
using InvoiceManagement.API.Dtos.Invoice_Product;
using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.API.Dtos.Invoice
{
    public class InvoiceFormDto
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }
        public decimal WorkForce { get; set; }
        
        public Guid CustomerId { get; set; }
        public List<InvoiceProductFormDto> Products { get; set; } = new List<InvoiceProductFormDto>();
    }
}


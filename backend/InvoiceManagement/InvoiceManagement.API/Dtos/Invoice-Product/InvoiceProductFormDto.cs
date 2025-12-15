using System.ComponentModel.DataAnnotations;
using InvoiceManagement.API.Dtos.Invoice_Product;
using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.API.Dtos.Invoice_Product
{
    public class InvoiceProductFormDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    
}


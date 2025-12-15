namespace InvoiceManagement.API.Dtos.Invoice
{
    public class InvoiceShortDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        
        public string? Description { get; set; } = null!;
        
    }
}


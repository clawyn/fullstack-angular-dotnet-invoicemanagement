namespace InvoiceManagement.API.Dtos.Product
{
    public class ProductShortDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public Decimal Price { get; set; }
        
    }
}


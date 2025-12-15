namespace InvoiceManagement.API.Dtos.Customer
{
    public class CustomerDetailsDto
    {
        public Guid Id { get; set; }
        
        public String Name { get; set; } = null!;
        
        public String Address { get; set; } = null!;
        
        public String Email { get; set; } = null!;
        
        public String Phone { get; set; } = null!;
    }
}


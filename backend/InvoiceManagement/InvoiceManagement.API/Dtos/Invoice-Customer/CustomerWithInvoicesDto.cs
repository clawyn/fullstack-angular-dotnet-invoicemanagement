using InvoiceManagement.API.Dtos.Invoice;

namespace InvoiceManagement.API.Dtos.Invoice_Customer;

public class CustomerWithInvoicesDto
{
    public Guid Id { get; set; }
    public String Name { get; set; } = null!;
        
    public String Address { get; set; } = null!;
        
    public String Email { get; set; } = null!;
        
    public String Phone { get; set; } = null!;
    
    public List<InvoiceDetailsDto>? Invoices { get; set; }
}
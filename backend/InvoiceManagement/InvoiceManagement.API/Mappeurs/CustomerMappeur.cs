using InvoiceManagement.API.Dtos.Customer;
using InvoiceManagement.API.Dtos.Invoice;
using InvoiceManagement.API.Dtos.Invoice_Customer;
using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.API.Mappeurs
{
    public static class CustomerMappeur
    {
        public static CustomerShortDto ToCustomerShortDto(this Customer c)
        {
            return new CustomerShortDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
            };
        }

        public static CustomerDetailsDto ToCustomerDetailsDto(this Customer c)
        {
            return new CustomerDetailsDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Email = c.Email,
                Phone = c.Phone,
                
            };
        }

        public static Customer ToCustomer(this CustomerFormDto c)
        {
            return new Customer
            {
                Name = c.Name,
                Address = c.Address,
                Email = c.Email,
                Phone = c.Phone,
            };
        }
        
        public static CustomerWithInvoicesDto ToDtoWithInvoices(Customer customer)
        {
            return new CustomerWithInvoicesDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Invoices = customer.Invoices?.Select(i => new InvoiceDetailsDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    WorkForce = i.WorkForce,
                    CustomerId = i.CustomerId
                }).ToList() ?? new List<InvoiceDetailsDto>()
            };
        }
    }
}


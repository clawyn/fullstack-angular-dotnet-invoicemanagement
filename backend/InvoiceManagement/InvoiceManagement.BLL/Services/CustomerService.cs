using InvoiceManagement.BLL.Services.Interfaces;
using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerManagementRepository _customerRepository;

        public CustomerService(ICustomerManagementRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Delete(Guid id)
        {
            Customer? existingCustomer = _customerRepository.FindOne(id);
            if(existingCustomer == null)
            {
                throw new Exception($"Customer with id {id} doesn't exist");
            }
            _customerRepository.Delete(existingCustomer);
        }

        public IEnumerable<Customer> FindAll()
        {
            return _customerRepository.FindMany();
        }

        public Customer FindById(Guid id)
        {
            Customer? customer = _customerRepository.FindOne(id);
            if(customer == null)
            {
                throw new Exception($"Invoice with id {id} doesn't exist");
            }
            return customer;
        }

        public Customer Save(Customer customer)
        {
            if(_customerRepository.Any(i => i.Name == customer.Name))
            {
                throw new Exception($"Customer with id {customer.Name} already exist");
            }
            return _customerRepository.Save(customer);
        }

        public void Update(Guid id, Customer customer)
        {
            Customer? existingCustomer = _customerRepository.FindOne(id);
            if(existingCustomer == null)
            {
                throw new Exception($"Customer with id {id} doesn't exist");
            }
            existingCustomer.Name = customer.Name;
            existingCustomer.Address = customer.Address;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;

            _customerRepository.Update(existingCustomer);
        }
    }
}
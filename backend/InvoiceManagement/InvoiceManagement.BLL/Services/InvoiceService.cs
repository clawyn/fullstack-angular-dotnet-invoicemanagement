using InvoiceManagement.BLL.Services.Interfaces;
using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvoiceManagement.BLL.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceManagementRepository _invoiceManagementRepository;
        private readonly IInvoiceProductManagementRepository _invoiceProductManagementRepository;
        private readonly IProductManagementRepository _productManagementRepository;
        private readonly ILogger<InvoiceService> _logger;


        public InvoiceService(
            IInvoiceManagementRepository invoiceManagementRepository,
            IInvoiceProductManagementRepository invoiceProductManagementRepository,
            IProductManagementRepository productManagementRepository,
            ILogger<InvoiceService> logger)
        {
            _invoiceManagementRepository = invoiceManagementRepository;
            _invoiceProductManagementRepository = invoiceProductManagementRepository;
            _productManagementRepository = productManagementRepository;
            _logger = logger;
        }

        public void Delete(Guid id)
        {
            Invoice? existingInvoice = _invoiceManagementRepository.FindOne(id);
            if (existingInvoice == null)
            {
                throw new Exception($"Invoice with id {id} doesn't exist");
            }

            _invoiceManagementRepository.Delete(existingInvoice);
        }

        public IEnumerable<Invoice> FindAll()
        {
            return _invoiceManagementRepository.FindMany();
        }

        public Invoice FindById(Guid id)
        {
            Invoice? invoice = _invoiceManagementRepository
                .FindOneWithProducts(id);

            if (invoice == null)
            {
                throw new Exception($"Invoice with id {id} doesn't exist");
            }

            return invoice;
        }


        public Invoice Save(Invoice invoice)
        {
            if (_invoiceManagementRepository.Any(i => i.Name == invoice.Name))
            {
                throw new Exception($"Invoice with id {invoice.Name} already exist");
            }

            return _invoiceManagementRepository.Save(invoice);
        }

        public void Update(Guid id, Invoice invoice)
        {
            Invoice? existingInvoice = _invoiceManagementRepository.FindOne(id);
            if (existingInvoice == null)
            {
                throw new Exception($"Invoice with id {id} doesn't exist");
            }

            existingInvoice.Name = invoice.Name;
            existingInvoice.Description = invoice.Description;
            existingInvoice.WorkForce = invoice.WorkForce;
            existingInvoice.CustomerId = invoice.CustomerId;

            _invoiceManagementRepository.Update(existingInvoice);
        }


        public Invoice SaveWithProducts(Invoice invoice)
        {
            try
            {
                if (_invoiceManagementRepository.Any(i => i.Name == invoice.Name))
                {
                    _logger.LogError($"Invoice with name {invoice.Name} already exists");
                    throw new Exception($"Invoice with name {invoice.Name} already exists");
                }

                if (invoice.InvoiceProducts != null && invoice.InvoiceProducts.Any())
                {
                    foreach (var invoiceProduct in invoice.InvoiceProducts)
                    {
                        var productDetails = _productManagementRepository.FindOne(invoiceProduct.ProductId);

                        if (productDetails == null)
                        {
                            _logger.LogError($"Product with id {invoiceProduct.ProductId} not found");
                            throw new Exception($"Product with id {invoiceProduct.ProductId} not found");
                        }

                        decimal workForce = invoice.WorkForce;
                        invoiceProduct.Amount = productDetails.Price * invoiceProduct.Quantity;
                        invoiceProduct.Amount = invoiceProduct.Amount + workForce;
                        invoiceProduct.Product = productDetails;
                        invoiceProduct.InvoiceId = invoice.Id;

                        _logger.LogInformation(
                            $"Product ID: {invoiceProduct.ProductId}, Quantity: {invoiceProduct.Quantity}, Price: {productDetails.Price}, Calculated Amount: {invoiceProduct.Amount}");
                    }
                }


                var savedInvoice = _invoiceManagementRepository.Save(invoice);

                _logger.LogInformation("Invoice and products saved successfully.");
                return savedInvoice;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while saving the invoice: {ex.Message}");
                _logger.LogError($"Inner Exception: {ex.InnerException?.Message}");
                throw;
            }
        }

        public IEnumerable<Invoice> GetInvoicesByCustomerId(Guid customerId)
        {
            var invoices = _invoiceManagementRepository.FindMany(i => i.CustomerId == customerId);
            return invoices;
        }
    }
}
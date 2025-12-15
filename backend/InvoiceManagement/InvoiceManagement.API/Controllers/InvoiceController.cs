using InvoiceManagement.API.Dtos.Invoice;
using InvoiceManagement.API.Mappeurs;
using InvoiceManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public ActionResult<List<InvoiceShortDto>> GetAll()
        {
            List<InvoiceShortDto> result = _invoiceService.FindAll()
                .Select(b => b.ToInvoiceShortDto())
                .ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<InvoiceDetailsDto> GetById([FromRoute] Guid id)
        {
            InvoiceDetailsDto dto = _invoiceService.FindById(id).ToInvoiceDetailsDto();
            return Ok(dto);
        }

        [HttpPost]
        //[Authorize("Auth")]
        public ActionResult Create([FromBody] InvoiceFormDto form)
        {
            _invoiceService.SaveWithProducts(form.ToInvoice());
            return Accepted();
        }

        /*[HttpPut("{id}")]
        [Authorize("Auth")]
        public ActionResult Update([FromRoute] Guid id, [FromBody] InvoiceFormDto form)
        {
            _invoiceService.Update(id, form.ToInvoice());
            return Accepted();
        }*/

        [HttpDelete("{id}")]
        //[Authorize("Auth")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            _invoiceService.Delete(id);
            return Accepted();
        }

        [HttpGet("customer/{customerId}/invoices")]
        public IActionResult GetInvoicesByCustomerId(Guid customerId)
        {
            Console.WriteLine($"Received customerId: {customerId}");

            if (customerId == Guid.Empty)
            {
                return BadRequest("Le Customer ID est invalide.");
            }

            // Supposons que ton service récupère les invoices par CustomerId
            var invoices = _invoiceService.GetInvoicesByCustomerId(customerId);

            if (invoices == null || !invoices.Any())
            {
                return NotFound("Aucune facture trouvée pour ce client.");
            }

            return Ok(invoices);
        }
    }
}
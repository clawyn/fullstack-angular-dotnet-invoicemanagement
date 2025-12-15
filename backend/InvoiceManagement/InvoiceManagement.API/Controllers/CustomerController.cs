using InvoiceManagement.API.Dtos.Customer;
using InvoiceManagement.API.Mappeurs;
using InvoiceManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly IInvoiceService _invoiceService;

        public CustomerController(ICustomerService customerService, IInvoiceService invoiceService)
        {
            _customerService = customerService;
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public ActionResult<List<CustomerShortDto>> GetAll()
        {
            List<CustomerShortDto> result = _customerService.FindAll()
                .Select(b => b.ToCustomerShortDto())
                .ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDetailsDto> GetById([FromRoute] Guid id)
        {
            CustomerDetailsDto dto = _customerService.FindById(id).ToCustomerDetailsDto();
            return Ok(dto);
        }

        [HttpPost]
        //[Authorize("Auth")]
        public ActionResult Create([FromBody] CustomerFormDto form)
        {
            _customerService.Save(form.ToCustomer());
            return Accepted();
        }

        [HttpPut("{id}")]
        //[Authorize("Auth")]
        public ActionResult Update([FromRoute] Guid id, [FromBody] CustomerFormDto form)
        {
            _customerService.Update(id, form.ToCustomer());
            return Accepted();
        }

        [HttpDelete("{id}")]
        [Authorize("Auth")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            _customerService.Delete(id);
            return Accepted();
        }
    } 
}


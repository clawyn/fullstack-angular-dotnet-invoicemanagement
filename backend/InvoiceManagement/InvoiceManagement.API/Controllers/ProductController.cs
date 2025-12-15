using InvoiceManagement.API.Dtos.Product;
using InvoiceManagement.API.Mappeurs;
using InvoiceManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<ProductShortDto>> GetAll()
        {
            List<ProductShortDto> result = _productService.FindAll()
                .Select(b => b.ToProductShortDto())
                .ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDetailsDto> GetById([FromRoute] Guid id)
        {
            ProductDetailsDto dto = _productService.FindById(id).ToProductDetailsDto();
            return Ok(dto);
        }

        [HttpPost]
        [Authorize("Auth")]
        public ActionResult Create([FromBody] ProductFormDto form)
        {
            _productService.Save(form.ToProduct());
            return Accepted();
        }

        [HttpPut("{id}")]
        [Authorize("Auth")]
        public ActionResult Update([FromRoute] Guid id, [FromBody] ProductFormDto form)
        {
            _productService.Update(id, form.ToProduct());
            return Accepted();
        }

        [HttpDelete("{id}")]
        [Authorize("Auth")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            _productService.Delete(id);
            return Accepted();
        }
    }
}
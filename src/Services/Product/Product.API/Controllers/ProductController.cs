using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Dto;
using Product.API.ProductServices;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route(nameof(AddProduct))]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.AddProduct(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _service.DeleteProduct(id);
            if(!result)
            {
                return NotFound(new {Message = "Invalid request"});
            }
            return NoContent();
        }
    }
}

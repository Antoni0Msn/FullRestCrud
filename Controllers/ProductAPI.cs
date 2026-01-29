using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPI : ControllerBase
    {
        readonly ProductService _service;

        public ProductAPI(ProductService service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await _service.GetAll();
            if (response.Count > 0)
            {
                return Ok(response);
            }
            return NotFound("Nothing in the list");
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByID(int id)
        {
            var response = await _service.GetByIdService(id);
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound("Product Not Found!");
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductClass product)
        {
            var response = await _service.CreateProductService(product);
            if (response == null)
            {
                return BadRequest($"Product Could Not Be Created! \nReason: {_service.errorException}");
            }
            return Ok(response);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductClass produto)
        {
            if (produto == null)
            {
                return BadRequest("Product body is required");
            }
            var response = await _service.UpdateService(id, produto);

            if (response == null)
            {
                return NotFound($"Product with id {id} not found");
            }
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var response = await _service.DeleteService(id);
            if (response == null)
            {
                return BadRequest($"Erro ao deletar! \nErro foi: {_service.errorException}.");
            }
            return Ok(response);
        }



    }
}

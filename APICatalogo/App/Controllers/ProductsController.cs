using APICatalogo.App.Domain.Products.Entities;
using APICatalogo.App.Domain.Products.Models.DTO;
using APICatalogo.App.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _uof.ProductRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<ProductDTO?>> GetProduct(int id)
        {
            var product = await _uof.ProductRepository.GetById(id);

            if (product is null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductDTO>(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO data)
        {

            var product = await _uof.ProductRepository.Create(_mapper.Map<ProductEntity>(data));
            await _uof.Commit();

            return new CreatedAtRouteResult(
                "GetById", 
                new { id = product.Id }, 
                _mapper.Map<ProductDTO>(product)
            );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductDTO data)
        {
            if (id != data.ProductId)
            {
                return BadRequest();
            }

            var product = _uof.ProductRepository.Update(_mapper.Map<ProductEntity>(data));
            await _uof.Commit();
            
            return Ok(_mapper.Map<ProductDTO>(product));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _uof.ProductRepository.GetById(id);

            if (product is null)
            {
                return NotFound("No products founded.");
            }

            _uof.ProductRepository.Delete(product);
            await _uof.Commit();

            return NoContent();
        }
    }
}

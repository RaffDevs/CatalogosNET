using APICatalogo.App.Domain.Category.Entities;
using APICatalogo.App.Domain.Category.Models.DTO;
using APICatalogo.App.Domain.Category.Models.Pagination;
using APICatalogo.App.Filters;
using APICatalogo.App.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;

namespace APICatalogo.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _uof.CategoryRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(categories));
        }

        [HttpGet("{id:int:min(1)}")]
        [EnableRateLimiting("fixedwindow")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _uof.CategoryRepository.GetById(id);

            if (category is null)
            {
                return NotFound("No category has been founded!");
            }

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryAndProducts()
        {
            var items = await _uof.CategoryRepository.GetCategoryAndProducts();

            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(items));
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll([FromQuery] CategoryPaginationParameters paginationParams)
        {
            var items = await _uof.CategoryRepository.GetAll(paginationParams);

            var metadata = new
            {
                items.TotalCount,
                items.PageSize,
                items.TotalPages,
                items.HasNext,
                items.HasPrevious
            };
            
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            
            return Ok(_mapper.Map<IEnumerable<CategoryDTO>>(items));
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO data)
        {
            var category = await _uof.CategoryRepository.Create(_mapper.Map<CategoryEntity>(data));
            await _uof.Commit();

            return new CreatedAtRouteResult(
                "GetById",
                new { id = category.Id },
                _mapper.Map<CategoryDTO>(category)
            );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(CategoryDTO data, int id)
        {
            if (id != data.Id)
            {
                return BadRequest();
            }

            var category = _uof.CategoryRepository.Update(_mapper.Map<CategoryEntity>(data));
            await _uof.Commit();

            return Ok(category);
        }

        [HttpPatch("{id:int}/partial")]
        public async Task<ActionResult<CategoryDTO>> PatchCategory(int id, JsonPatchDocument<CategoryDTO> data)
        {
            if (data is null || id <= 0)
            {
                return BadRequest();
            }

            var category = await _uof.CategoryRepository.GetById(id);

            if (category is null)
            {
                return NotFound();
            }

            var categoryToUpdate = _mapper.Map<CategoryDTO>(category);
            
            data.ApplyTo(categoryToUpdate);
            
            var categoryUpdated = _uof.CategoryRepository.Update(
                _mapper.Map<CategoryEntity>(categoryToUpdate)
            );
            
            await _uof.Commit();

            return Ok(_mapper.Map<CategoryDTO>(categoryUpdated));

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _uof.CategoryRepository.GetById(id);

            if (category is null)
            {
                return NotFound();
            }

            _uof.CategoryRepository.Delete(category);
            await _uof.Commit();

            return NoContent();
        }
    }
}
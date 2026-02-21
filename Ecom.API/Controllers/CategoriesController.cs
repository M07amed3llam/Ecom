using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var categories = await work.CategoryRepository.GetAllAsync();
                if (categories is null || !categories.Any())
                    return NotFound("No categories found.");
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var category = await work.CategoryRepository.GetByIdAsync(id);
                if (category is null)
                    return NotFound($"Category with ID {id} not found.");
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> add(CategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await work.CategoryRepository.AddAsync(category);
                return Ok(new { message = "Category has been added" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-category/{id}")]
        public async Task<IActionResult> update(UpdateCategoryDTO CategoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(CategoryDTO);
                await work.CategoryRepository.UpdateAsync(category);
                return Ok(new { message = "Category has been updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                await work.CategoryRepository.DeleteAsync(id);
                return Ok(new { message = "Category has been deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
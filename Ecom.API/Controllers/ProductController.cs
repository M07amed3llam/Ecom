using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var Product = await work
                                    .ProductRepository
                                    .GetAllAsync(p => p.Category, p => p.Photos);

                var result = mapper.Map<List<ProductDTO>>(Product);

                if (result is null || !result.Any())
                    return BadRequest(new ResponseAPI(400));
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var product = await work
                                    .ProductRepository
                                    .GetByIdAsync(id, p => p.Category, p => p.Photos);
                var result = mapper.Map<ProductDTO>(product);
                if (result is null)
                    return BadRequest(new ResponseAPI(400));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }
    }
}
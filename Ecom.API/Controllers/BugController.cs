using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var Category = await work.CategoryRepository.GetByIdAsync(100);
            if (Category == null) 
                return NotFound();
            return Ok(Category);
        }

        [HttpGet("server-error")]
        public async Task<ActionResult> GetServerError()
        {
            var Category = await work.CategoryRepository.GetByIdAsync(100);
            Category.Name = "";
            return Ok(Category);
        }

        [HttpGet("bad-request/{id}")]
        public async Task<ActionResult> GetBadRequest(int id)
        {
            return Ok();
        }

        [HttpGet("bad-request")]
        public async Task<ActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    } 
}

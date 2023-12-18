
using IntroAPI.Dtos.CategoryDtos;
using IntroAPI.Dtos.TagDtos;
using IntroAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int page, int take = 3)
        {
            var result = await _service.GetAllAsync(page,take);

            return Ok(result);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id < 0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK,await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateTagDto tagDto)
        {
            await _service.CreateAsync(tagDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            
            return NoContent();


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.DeleteAsync(id);
            return NoContent();
        }






    }
}

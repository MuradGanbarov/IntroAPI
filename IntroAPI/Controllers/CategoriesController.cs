
using IntroAPI.Repositories.Implementations;
using IntroAPI.Repositories.Interfaces;
using IntroAPI.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq.Expressions;

namespace IntroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        
        private readonly ICategoryRepository _repository;
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryRepository repository,ICategoryService service)
        {
            
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
           var result = await _service.GetAllAsync(page, take);
           return Ok(result);
        }

        [HttpGet("/api/[controller]/order")]
        public async Task<IActionResult> GetByOrder(string data,bool isDescending=false,int page = 1, int take = 3)
        {
            var result = await _service.GetAllOrderByAsync(data,isDescending,page,take,false);
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            
            return StatusCode(StatusCodes.Status200OK,await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto categoryDto)
        {
            await _service.CreateAsync(categoryDto);
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

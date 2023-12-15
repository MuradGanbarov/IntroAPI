
using IntroAPI.Repositories.Implementations;
using IntroAPI.Repositories.Interfaces;
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
        private readonly AppDbContext _context;
        private readonly IRepository _repository;

        public CategoriesController(AppDbContext context,IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page,int take=3)
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            //return BadRequest() arxada geden proses
            Category category = await _repository.GetByIdAsync(id);
            if (category is null) return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, category);
            //return Ok(category) arxa geden proses yuxarda yazilib
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto categoryDto)
        {
            IQueryable<Category> db = _context.Categories;
            Category category = new()
            {
                Name = categoryDto.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created,category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category existed = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Name = name;
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Remove(existed);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}

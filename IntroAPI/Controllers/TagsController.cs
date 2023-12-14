
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TagsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int page, int take = 3)
        {
            List<Tag> tags = await _context.Tags.Skip((page - 1) * take).Take(take).ToListAsync();

            return Ok(tags);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            //return BadRequest() arxada geden proses
            Tag tag = await _context.Tags.FirstOrDefaultAsync(c => c.Id == id);
            if (tag is null) return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, tag);
            //return Ok(category) arxa geden proses yuxarda yazilib
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Tag existed = await _context.Tags.FirstOrDefaultAsync(c => c.Id == id);
            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Name = name;
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Tag existed = await _context.Tags.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Remove(existed);
            await _context.SaveChangesAsync();

            return NoContent();

        }






    }
}

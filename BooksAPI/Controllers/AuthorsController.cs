using BooksAPI.Models;
using BooksAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public BookManagementContext _context;
        public AuthorsController(BookManagementContext context)
        {
            _context = context;
        }
        // GET api/authors
        // Получить всех авторов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
        }
        // GET api/authors/5
        // Получить автора по id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetAuthor(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(b => b.Id == id);

            if (author == null)
            {
                return NotFound("Автор не найден");
            }

            return Ok(author);
        }
        // POST api/authors
        // Добавить автора
        [HttpPost]
        public async Task<ActionResult<Book>> CreateAuthor(AuthorDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Данные автора не переданы");
            }

            var author = new Author
            {
                Id = _context.Authors.Max(x => x.Id) + 1,
                Name = dto.Name,
                Biography = dto.Biography
            };


            try
            {
                _context.Authors.Add(author);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при сохранении автора: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetAuthor),
            new { id = author.Id },
            new
            {
                author.Id,
                author.Name,
                author.Biography
            }
            );

        }
    }
}

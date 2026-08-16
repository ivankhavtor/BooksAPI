using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BooksAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        public BookManagementContext _context;
        public GenresController(BookManagementContext context)
        {
            _context = context;
        }
        // GET api/genres
        // Получить все жанры
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>>GetGenres()
        {
            var genres = await _context.Genres.ToListAsync();
            return Ok(genres);
        }
        // DELETE api/genres/5
        // Удалить жанр
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            var book = await _context.Books.FirstOrDefaultAsync(x => x.GenreId == genre.Id);

            if (genre == null)
            {
                return NotFound("Жанр не найден");
            }
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            _context.Genres.Remove(genre);

            await _context.SaveChangesAsync();


            return NoContent();
        }
    }
}

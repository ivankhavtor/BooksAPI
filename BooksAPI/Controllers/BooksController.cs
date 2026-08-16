using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksAPI.Models;
using BooksAPI.Models.DTO;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookManagementContext _context;

        public BooksController(BookManagementContext context)
        {
            _context = context;
        }


        // GET api/books
        // Получить все книги
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    PublishedYear = b.PublishedYear,
                    Isbn = b.Isbn,
                    Author = b.Author.Name,
                    Genre = b.Genre.Name
                })
                .ToListAsync();

            return Ok(books);
        }


        // GET api/books/5
        // Получить книгу по Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre).Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    PublishedYear = b.PublishedYear,
                    Isbn = b.Isbn,
                    Author = b.Author.Name,
                    Genre = b.Genre.Name
                })
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound("Книга не найдена");
            }

            return Ok(book);
        }


        // POST api/books
        // Добавить книгу
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(InfoBookDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Данные книги не переданы");
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(x => x.Name.Contains(dto.Author));

            if (author == null)
            {
                return BadRequest($"Автор {dto.Author} не найден");
            }


            var genre = await _context.Genres
                .FirstOrDefaultAsync(x => x.Name.Contains(dto.Genre));

            if (genre == null)
            {
                return BadRequest($"Жанр {dto.Genre} не найден");
            }


            var book = new Book
            {
                Id = _context.Books.Max(x => x.Id) + 1,
                Title = dto.Title,
                Description = dto.Description,
                AuthorId = author.Id,
                GenreId = genre.Id,
                PublishedYear = dto.PublishedYear,
                Isbn = dto.Isbn,
                CreatedAt = DateTime.Now
            };


            try
            {
                _context.Books.Add(book);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при сохранении книги: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetBook),
            new { id = book.Id },
            new
            {
                book.Id,
                book.Title,
                book.Description,
                book.PublishedYear,
                book.Isbn,
                book.CreatedAt,
                
            }
            );

        }


        // PUT api/books/5
        // Изменить книгу
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, InfoBookDto dtoInf)
        {
            // Ищем существующую книгу
            var book = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == id);

            if (book == null)
            {
                return NotFound($"Книга с Id {id} не найдена");
            }


            // Ищем автора по имени
            var author = await _context.Authors
                .FirstOrDefaultAsync(x => x.Name.Contains(dtoInf.Author));

            if (author == null)
            {
                author = new Author
                {
                    Id = _context.Authors.Max(x => x.Id) + 1,
                    Name = dtoInf.Author
                };

                _context.Authors.Add(author);

                await _context.SaveChangesAsync();
            }


            // Ищем жанр по названию
            var genre = await _context.Genres
                .FirstOrDefaultAsync(x => x.Name.Contains(dtoInf.Genre));

            if (genre == null)
            {
                genre = new Genre
                {
                    Id = _context.Genres.Max(x => x.Id) + 1,
                    Name = dtoInf.Genre
                };

                _context.Genres.Add(genre);

                await _context.SaveChangesAsync();
            }



            book.Title = dtoInf.Title;
            book.Description = dtoInf.Description;
            book.AuthorId = author.Id;
            book.GenreId = genre.Id;
            book.PublishedYear = dtoInf.PublishedYear;
            book.Isbn = dtoInf.Isbn;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == id))
                {
                    return NotFound($"Книга с Id {id} не найдена");
                }

                throw;
            }


            return NoContent();
        }


        // DELETE api/books/5
        // Удалить книгу
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);


            if (book == null)
            {
                return NotFound("Книга не найдена");
            }


            _context.Books.Remove(book);

            await _context.SaveChangesAsync();


            return NoContent();
        }
    }
}
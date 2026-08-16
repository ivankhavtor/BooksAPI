using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название книги обязательно")]
        [StringLength(200, MinimumLength = 1,
            ErrorMessage = "Название книги должно содержать от 1 до 200 символов")]
        public string Title { get; set; }

        [StringLength(1000,
            ErrorMessage = "Описание не должно превышать 1000 символов")]
        public string Description { get; set; }

        [Range(0, 2026,
            ErrorMessage = "Год публикации должен быть от 0 до 2026")]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "ISBN обязателен")]
        [StringLength(13, MinimumLength = 10,
            ErrorMessage = "ISBN должен содержать от 10 до 13 символов")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Автор обязателен")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Имя автора должно содержать от 2 до 100 символов")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Жанр обязателен")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Название жанра должно содержать от 2 до 100 символов")]
        public string Genre { get; set; }
    }
}

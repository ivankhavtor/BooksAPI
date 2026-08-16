using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Models.DTO
{
    public class AuthorDto
    {
        [Required(ErrorMessage = "Имя автора обязательно")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя автора должно быть от 2 до 100 символов")]
        public string Name { get; set; }


        [StringLength(1000, ErrorMessage = "Биография не должна превышать 1000 символов")]
        public string Biography { get; set; }
    }
}

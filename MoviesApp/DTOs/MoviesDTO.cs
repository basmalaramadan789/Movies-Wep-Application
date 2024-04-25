using System.ComponentModel.DataAnnotations;

namespace MoviesApp.DTOs
{
    public class MoviesDTO
    {
        [MaxLength(100)]
        public string Title { get; set; }
        public int year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoreLine { get; set; }
        public IFormFile? Poster { get; set; }
        public byte GenreId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MoviesApp.DTOs
{
    public class GeneresDTO
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}

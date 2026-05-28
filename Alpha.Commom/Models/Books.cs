using System.ComponentModel.DataAnnotations;

namespace Alpha.Common.Models
{
    public class Books
    {
        public Guid BookId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Author { get; set; }

        [Range(1, 10000)]
        public decimal Price { get; set; }
    }
}
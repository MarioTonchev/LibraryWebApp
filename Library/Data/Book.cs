using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Library.Data.DataConstants;

namespace Library.Data
{
	public class Book
	{
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(BookMaxTitle)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(BookMaxAuthor)]
        public string Author { get; set; } = string.Empty;
		[Required]
        [MaxLength(BookMaxDescription)]
        public string Description { get; set; } = string.Empty;
		[Required]
        public string ImageUrl { get; set; } = string.Empty;
		[Required]
        [Range(BookMinRating, BookMaxRating)]
        public decimal Rating { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
        public ICollection<IdentityUserBook> UsersBooks { get; set; } = new List<IdentityUserBook>();
    }
}

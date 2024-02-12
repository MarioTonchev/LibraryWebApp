using Library.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants;

namespace Library.Models
{
	public class BookFormViewModel
	{
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(BookMaxTitle, MinimumLength = BookMinTitle, ErrorMessage = StringLengthErrorMessage)]
		public string Title { get; set; } = string.Empty;
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(BookMaxAuthor, MinimumLength = BookMinAuthor, ErrorMessage = StringLengthErrorMessage)]
		public string Author { get; set; } = string.Empty;
		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(BookMaxDescription, MinimumLength = BookMinDescription, ErrorMessage = StringLengthErrorMessage)]
		public string Description { get; set; } = string.Empty;
		[Required(ErrorMessage = RequiredErrorMessage)]
		public string Url { get; set; } = string.Empty;
		[Required(ErrorMessage = RequiredErrorMessage)]
		[Range(BookMinRating, BookMaxRating, ErrorMessage = RatingRangeErrorMessage)]
		public decimal Rating { get; set; }
		[Required(ErrorMessage = RequiredErrorMessage)]
		[Display(Name = "Category")]
		public int CategoryId { get; set; }
		public ICollection<CategoryViewModel> Categories { get; set; } 
	}
}

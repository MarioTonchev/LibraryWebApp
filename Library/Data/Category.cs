using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
	public class Category
	{
		[Key]
        public int Id { get; set; }
		[Required]
		[MaxLength(DataConstants.CategoryMaxName)]
		public string Name { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
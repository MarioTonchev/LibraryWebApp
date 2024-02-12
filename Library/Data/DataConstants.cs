namespace Library.Data
{
	public static class DataConstants
	{
		//Book
		public const int BookMaxTitle = 50;
		public const int BookMinTitle = 10;

		public const int BookMaxAuthor = 50;
		public const int BookMinAuthor = 5;

		public const int BookMaxDescription = 5000;
		public const int BookMinDescription = 5;

		public const double BookMaxRating = 10.00;
		public const double BookMinRating = 0.00;

		//Category
		public const int CategoryMaxName = 50;
		public const int CategoryMinName = 5;

		//Warnings
		public const string RequiredErrorMessage = "Field {0} is required";
		public const string StringLengthErrorMessage = "Field {0} must be between {2} and {1} characters long";
		public const string RatingRangeErrorMessage = "Field {0} must be between {2} and {1}";
	}
}

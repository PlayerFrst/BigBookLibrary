namespace BigBookLibrary.Common
{
    public static class ValidationConstants
    {
        // Book
        public const int BookTitleMinLength = 2;
        public const int BookTitleMaxLength = 100;
        public const int BookDescriptionMaxLength = 1000;
        public const int BookIsbnLength = 20;

        // Author
        public const int AuthorNameMinLength = 2;
        public const int AuthorNameMaxLength = 50;
        public const int AuthorBiographyMaxLength = 2000;

        // Genre
        public const int GenreNameMinLength = 2;
        public const int GenreNameMaxLength = 30;

        // Review
        public const int ReviewCommentMinLength = 10;
        public const int ReviewCommentMaxLength = 500;
        public const int ReviewRatingMin = 1;
        public const int ReviewRatingMax = 5;
    }
}

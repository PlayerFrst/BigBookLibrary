namespace BigBookLibrary.ViewModels.Reviews
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public int Rating { get; set; }

        public string Comment { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }

}

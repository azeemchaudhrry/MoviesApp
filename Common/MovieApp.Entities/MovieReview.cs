namespace MovieApp.Entities
{
    public class MovieReview
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public float? Rating { get; set; }
        public byte[] Attachment { get; set; }

        public MovieReview()
        {

        }

        public MovieReview(MovieReview movieReview)
        {
            Title = movieReview.Title;
            Review = movieReview.Review;
            Rating = movieReview.Rating;
            Attachment = movieReview.Attachment;
        }
    }
}

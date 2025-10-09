namespace Filminurk.Models.Movies
{
    public class MoviesIndexViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateOnly FirstPublished { get; set; }
        public decimal? CurrentRating { get; set; }
        public int? DeadCounter { get; set; }
    }
}

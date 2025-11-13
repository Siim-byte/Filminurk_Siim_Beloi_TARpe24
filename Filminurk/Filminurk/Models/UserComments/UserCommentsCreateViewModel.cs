namespace Filminurk.Models.UserComments
{
    public class UserCommentsCreateViewModel
    {
        public Guid? CommentID { get; set; }
        public string? CommenterUserID { get; set; }
        public string CommentBody { get; set; }
        public int CommentedScore { get; set; }
        public int? IsHelpful { get; set; } // kasutaja ei saa loomise ajal muuta laike
        public int? IsHarmful { get; set; } //ega dislike
        // andmebaasi vajalikud asjad
        public DateTime CommentCreatedAt { get; set; }
        public DateTime CommentModifiedAt { get; set; }
        public DateTime? CommentDeletedAt { get; set; } // kasutaja ei saa kustutada kommentaari loomise hetkel
    }
}

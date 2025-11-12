using Filminurk.Core.Domain;

namespace Filminurk.Models.Actors
{
    public class ActorsCreateUpdateViewModel
    {
        public Guid? ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string? MoviesActedFor { get; set; }
        public int PortraitID { get; set; }

        //andmebaasi jaoks vajalik

        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }

        //minu mõeldud

        public HomeCountry? HomeCountry { get; set; }
        public string? HomeCity { get; set; }
        public string? HomeRegion { get; set; }
    }
}

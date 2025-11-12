using Filminurk.Core.Domain;

namespace Filminurk.Models.Actors
{
    public class ActorsIndexViewModel
    {
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string? HomeCity { get; set; }
        public HomeCountry? HomeCountry { get; set; }
    }
}

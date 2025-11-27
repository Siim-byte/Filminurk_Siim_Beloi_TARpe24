using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Models.Actors
{
    public class ActorsCreateUpdateViewModel
    {
        public Guid? ActorID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NickName { get; set; }
        public List<string>? MoviesActedFor { get; set; }
        public int?   PortraitID { get; set; }
        public List<IFormFile>? Files { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();
        public IEnumerable<FileToApiDTO>? FileToApiDTOs { get; set; } = new List<FileToApiDTO>();
        //andmebaasi jaoks vajalik

        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }

        //minu mõeldud

        public HomeCountry? HomeCountry { get; set; }
        public string? HomeCity { get; set; }
        public string? HomeRegion { get; set; }
    }
}

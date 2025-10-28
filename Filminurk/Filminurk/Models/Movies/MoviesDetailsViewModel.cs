﻿using Filminurk.Core.Domain;
namespace Filminurk.Models.Movies
{
    public class MoviesDetailsViewModel
    {
        public Guid? ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? FirstPublished { get; set; }
        public string? Director { get; set; }
        public List<string>? Actors { get; set; }
        public decimal? CurrentRating { get; set; }
        //public List<UserComment>? Reviews { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();
        public int? DeadCounter { get; set; }   
        public int? AliveCounter { get; set; }
        public int? ActorCounter { get; set; }
        /* andmebaasi jaoks vajalikud*/
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
    }
}

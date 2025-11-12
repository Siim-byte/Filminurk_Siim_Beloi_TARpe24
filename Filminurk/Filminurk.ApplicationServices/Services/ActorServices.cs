using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class ActorServices : IActorsServices
    {
        private readonly FilminurkTARpe24Context _context;
        public ActorServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public async Task<Actors> Create(ActorsDTO dto)
        {
            Actors actors = new Actors();
            actors.ActorID = Guid.NewGuid();
            actors.FirstName = dto.FirstName;
            actors.LastName = dto.LastName;
            actors.NickName = dto.NickName;
            actors.MoviesActedFor = string.IsNullOrWhiteSpace(dto.MoviesActedFor) ? null : dto.MoviesActedFor;
            actors.PortraitID = dto.PortraitID;
            actors.EntryCreatedAt = DateTime.Now;
            actors.EntryModifiedAt = DateTime.Now;
            actors.HomeCountry = dto.HomeCountry;
            actors.HomeCity = dto.HomeCity;
            actors.HomeRegion = dto.HomeRegion;

            await _context.Actors.AddAsync(actors);
            await _context.SaveChangesAsync();

            return actors;
        }
        public async Task<Actors> DetailsAsync(Guid id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(x => x.ActorID == id);
            return result;
        }
        public async Task<Actors> Update(ActorsDTO dto)
        {
            Actors actors = new Actors();
            actors.ActorID = Guid.NewGuid();
            actors.FirstName = dto.FirstName;
            actors.LastName = dto.LastName;
            actors.NickName = dto.NickName;
            actors.MoviesActedFor = dto.MoviesActedFor;
            actors.PortraitID = dto.PortraitID;
            actors.EntryCreatedAt = DateTime.Now;
            actors.EntryModifiedAt = DateTime.Now;
            actors.HomeCountry = dto.HomeCountry;
            actors.HomeCity = dto.HomeCity;
            actors.HomeRegion = dto.HomeRegion;

            await _context.Actors.AddAsync(actors);
            await _context.SaveChangesAsync();
            return actors;
        }
        /*        public async Task<Actors> Delete(ActorsDTO dto, Guid id)
                {
                    var result = await _context.Actors
                        .FirstOrDefaultAsync(m => m.ActorID == id);

                    _context.Actors.Remove(result);
                    await _context.SaveChangesAsync();

                    return result;
                }
          */
        public async Task<Actors> Delete(Guid id)
        {
            var result = await _context.Actors.FirstOrDefaultAsync(m => m.ActorID == id);

            _context.Actors.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }

    }
}

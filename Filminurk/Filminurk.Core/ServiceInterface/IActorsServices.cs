using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.ServiceInterface
{
    public interface IActorsServices
    {
        Task<Actors> Create(ActorsDTO dto);
        Task<Actors> Delete(ActorsDTO dto);
        Task<Actors> DetailsAsync(ActorsDTO dto);
        Task<Actors> Update(ActorsDTO dto);
    }
}

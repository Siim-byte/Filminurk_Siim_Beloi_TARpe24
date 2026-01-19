using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto.OMDbDTOs
{
    public class OMDbApiRootDTO : OMDbApiResultDTO
    {
        public string Response { get; set; } // True or False
        public string Error { get; set; } = string.Empty;

        // When Response == true
        public OMDbApiResultDTO Result { get; set; }
    }
}

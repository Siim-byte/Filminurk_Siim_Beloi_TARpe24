using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto
{
    public class FileToApiDTO
    {
        public Guid ImageID { get; set; }
        public string? ExistingFilePath { get; set; }
        public Guid? MovieID { get; set; }
        public bool? IsPoster { get; set; } // määrab ära kas plt on poster või mitte
    }
}

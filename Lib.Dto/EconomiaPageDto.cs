using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Data;

namespace Lib.Dto
{
    public class EconomiaPageDto : BaseDto
    {
        public List<EconomiasDal> Economias { get; set; }
        public List<EconomiasMetaDal> EconomiasMeta { get; set; }
    }
}

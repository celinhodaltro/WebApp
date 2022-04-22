using Lib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dto
{
    public class AtribuirChamadoDto : BaseDto
    {
        public ChamadoDal chamado { get; set; }
        public List<ContaDal> Contas { get; set; }
        public List<ContaDal> ContasAtribuidas { get; set; }
    }
}

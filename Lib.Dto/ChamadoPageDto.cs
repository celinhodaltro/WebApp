using Lib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dto
{
    public class ChamadoPageDto : BaseDto
    {
        public List<ChamadoDal> Chamados { get; set; }
        public List<ProjetoDal> Projetos { get; set; }
    }
}

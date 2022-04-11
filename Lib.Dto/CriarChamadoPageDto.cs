using Lib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dto
{
    public class CriarChamadoPageDto : BaseDto
    {
        public ChamadoDal Chamado { get; set; }
        public List<ProjetoDal> Projeto { get; set; }
    }
}

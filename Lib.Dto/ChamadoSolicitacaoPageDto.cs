using Lib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dto
{
    public class ChamadoSolicitacaoPageDto :BaseDto
    {
        public List<Lib.Data.ChamadoSolicitacaoDal> Solicitacoes { get; set; }
        public ChamadoSolicitacaoDal Solicitar { get; set; }
        public bool Atribuinte { get; set; }
    }
}

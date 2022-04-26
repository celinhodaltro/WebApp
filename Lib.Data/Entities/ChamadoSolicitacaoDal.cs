using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data
{
    public class ChamadoSolicitacaoDal : BaseDal
    {
        public int IdChamado { get; set; }
        public DateTime Data { get; set; }
        public string Mensagem { get; set; }
        public int idTipoSolicitacao {get; set;}
        public bool Atribuinte { get; set; }
        public string NomeDoAutor { get; set; }
        public string EmailDoAutor { get; set; } = "";
    }
}

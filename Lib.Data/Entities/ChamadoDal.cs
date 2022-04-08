using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data
{
    public class ChamadoDal : BaseDal
    {
        public string Nome { get; set; }
        public string Desc { get; set; }
        public int Prioridade { get; set; }
        public int IdConta { get; set; }
        public int IdProjeto { get; set; }
        public bool Validacao { get; set; }
        public int IdStatus { get; set; }
    }
}

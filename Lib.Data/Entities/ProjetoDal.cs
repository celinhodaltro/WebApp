using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data
{
    public class ProjetoDal : BaseDal
    {
        public string Nome { get; set; }
        public string Desc { get; set; }
        public int Prioridade { get; set; }
        public int IdCriador { get; set; }
    }
}

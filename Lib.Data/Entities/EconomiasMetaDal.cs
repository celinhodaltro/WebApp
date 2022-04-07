using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data
{
    public class EconomiasMetaDal : BaseDal
    {
        public double Valor { get; set; }
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
    }
}

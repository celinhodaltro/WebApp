using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data
{
    public class TarefaDal : BaseDal
    {
        public string Nome{ get; set; }
        public bool Feita { get; set; }
        public int IdConta { get; set; }
        public DateTime Dia { get; set; }
    }
}

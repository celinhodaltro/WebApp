using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data
{
    public class ContaDal: BaseDal
    {
        public string Conta { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string DataDeCriacao { get; set; }
        public bool Admin { get; set; }
    }
}

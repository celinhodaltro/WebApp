using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dto
{
    public class NovaContaDto : BaseDto
    {
        public string Conta { get; set; }
        public string Senha { get; set; }
        public string SenhaRepetida { get; set; }
        public string Email { get; set; }
        public string DataDeCriacao { get; set; }
    }
}

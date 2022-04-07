using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data
{
    public class EconomiasDal : BaseDal
    {
        public double Valor { get; set; }
        public int IdPessoa { get; set; }
        public DateTime Data { get; set; }
        public int IdEconomiaMeta { get; set; }
        public string NomeEconomiaMeta { get; set; }
    }
}

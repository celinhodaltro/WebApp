using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Data;

namespace Lib.Dto
{
    public class CargoPageDto : BaseDto
    {
        public List<CargoDal> CargosPessoa { get; set; }
        public List<CargoDal> Cargos { get; set; }
        public int IdPessoa { get; set; }
    }
}

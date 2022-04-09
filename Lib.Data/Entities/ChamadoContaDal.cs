﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data
{
    public class ChamadoContaDal : BaseDal
    {
        public int IdConta { get; set; }
        public int IdProjeto { get; set; }
        public string Funcao { get; set; }
        public int IdContaAtribuinte { get; set; }
        public int IdContaAtribuido { get; set; }
    }
}

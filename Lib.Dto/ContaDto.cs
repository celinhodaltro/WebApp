using System;

namespace Lib.Dto
{
    public class ContaDto : BaseDto
    {
        public string Conta { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string DataDeCriacao { get; set; }
    }
}

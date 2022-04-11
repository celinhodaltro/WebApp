using Lib.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Lib.Enum;

namespace FacadeApp.Services
{
    public class ChamadoService : BaseService
    {
        public ChamadoService(DataBaseContext item) : base(item) { }

        public async Task<List<ChamadoDal>> ConsultarTodos(bool arquivados = false)
        {
            var chamados = await AppContext.Chamados.Where(tb => tb.Arquivado == arquivados).ToListAsync();
            return chamados;
        }

        public async Task Adicionar(ChamadoDal chamado, ChamadoContaDal chamadoConta)
        {
            await AppContext.Chamados.AddAsync(new ChamadoDal {Nome = chamado.Nome, Desc = chamado.Desc, IdProjeto = chamado.IdProjeto, IdStatus = (int)ChamadoStatus.EmAnalise, IdTipoChamado = chamado.IdTipoChamado, Arquivado = false, NomeProjeto = chamado.NomeProjeto, Prioridade = chamado.Prioridade });
            await AppContext.ChamadoConta.AddAsync(chamadoConta);
        }








    }
}

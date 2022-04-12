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

        public async Task<ChamadoDal> Consultar(int id)
        {
            var chamado = await AppContext.Chamados.Where(tb => tb.Id == id).FirstOrDefaultAsync();
            return chamado;
        }

        public async Task<List<ChamadoDal>> ConsultarChamadosDoUsuario(int id)
        {
            var chamadosId = await AppContext.ChamadoConta.Where(tb => tb.IdContaAtribuido == id || tb.IdContaAtribuinte == id).Select(tb => tb.IdChamado).ToListAsync();
            List<ChamadoDal> chamados = new();
            foreach(var chamadoid in chamadosId)
            {
                var chamado = await AppContext.Chamados.Where(tb => tb.Id == chamadoid).FirstOrDefaultAsync();
                chamados.Add(chamado);
                chamado = null;
            }
            return chamados;
        }

        public async Task Adicionar(ChamadoDal chamado)
        {
            await AppContext.Chamados.AddAsync(new ChamadoDal {Nome = chamado.Nome, Desc = chamado.Desc, IdProjeto = chamado.IdProjeto, IdStatus = (int)ChamadoStatus.Processando, IdTipoChamado = chamado.IdTipoChamado, Arquivado = false, NomeProjeto = chamado.NomeProjeto, Prioridade = chamado.Prioridade });
            await AppContext.SaveChangesAsync();
        }








    }
}

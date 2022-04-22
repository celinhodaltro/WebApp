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

        public async Task<List<ChamadoDal>> ConsultarChamadosDoUsuario(int id, int idProjeto = 0)
        {

            var chamadosId = await AppContext.ChamadoConta.Where(tb => tb.IdContaAtribuido == id || tb.IdContaAtribuinte == id).Select(tb => tb.IdChamado).ToListAsync();


            List<ChamadoDal> chamados = new();
            foreach (var chamadoid in chamadosId)
            {
                ChamadoDal chamado = new();
                if(idProjeto!=0)
                    chamado = await AppContext.Chamados.Where(tb => tb.Id == chamadoid && tb.IdProjeto == idProjeto).FirstOrDefaultAsync();
                else
                    chamado = await AppContext.Chamados.Where(tb => tb.Id == chamadoid).FirstOrDefaultAsync();
                if(chamado!=null)
                    chamados.Add(chamado);

                chamado = null;
            }
            return chamados;
        }

        public async Task Adicionar(ChamadoDal chamado)
        {
            await AppContext.Chamados.AddAsync(new ChamadoDal { Nome = chamado.Nome, Desc = chamado.Desc, IdProjeto = chamado.IdProjeto, IdStatus = (int)ChamadoStatus.Processando, IdTipoChamado = chamado.IdTipoChamado, Arquivado = false, NomeProjeto = chamado.NomeProjeto, Prioridade = chamado.Prioridade });
            await AppContext.SaveChangesAsync();
        }

        public async Task Atribuir(int idChamado, int idAtribuinte, int idAtribuido)
        {
            await AppContext.ChamadoConta.AddAsync(new ChamadoContaDal { IdChamado = idChamado, IdContaAtribuinte = idAtribuinte, IdContaAtribuido = idAtribuido });
            await AppContext.SaveChangesAsync();
        }








    }
}

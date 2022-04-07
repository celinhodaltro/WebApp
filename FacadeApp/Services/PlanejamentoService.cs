using Lib.Data;
using Lib.Dto;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FacadeApp.Services
{
    public class PlanejamentoService : BaseService
    {
        public PlanejamentoService(DataBaseContext item) : base(item) { }




        //Tarefas
        public async Task<List<TarefaDal>> ConsultarTarefas(DateTime dia, int ContaId)
        {
            var tarefas = await AppContext.Tarefas.Where(tb=>tb.Dia == dia && tb.IdConta == ContaId).ToListAsync();
            return tarefas;
        }

        public async Task AdicionarTarefa(string Nome, int Id)
        {
            if (Nome == "")
                throw new Exception("A tarefa deve conter pelo menos uma letra!");


            await AppContext.AddAsync(new TarefaDal { Dia = DateTime.Today, Feita = false, IdConta = Id, Nome = Nome });
            await AppContext.SaveChangesAsync();

        }

        public async Task RemoverTarefa(int Id)
        {
            var tarefas = await AppContext.Tarefas.Where(tb => tb.Id == Id).ToListAsync();
            AppContext.Tarefas.RemoveRange(tarefas);
            await AppContext.SaveChangesAsync();
        }

        public async Task AlterarFeitos(List<TarefaDal> tarefas)
        {
            foreach(var obj in tarefas)
            {
                var tarefa = await AppContext.Tarefas.Where(tb => tb.Id == obj.Id).FirstOrDefaultAsync();
                if(tarefa.Feita != obj.Feita)
                {
                    tarefa.Feita = !obj.Feita;
                }
            }
            await AppContext.SaveChangesAsync();
        }

        //Economias
        public async Task<List<EconomiasDal>> ConsultarEconomias(DateTime dia, int ContaId)
        {
            var Economias = await AppContext.Economias.Where(tb => tb.Data.Day == dia.Day && tb.IdPessoa == ContaId).ToListAsync();
            return Economias;
        }

        public async Task<List<EconomiasMetaDal>> ConsultarEconomiasMetas(int ContaId)
        {
            var EconomiasMeta = await AppContext.EconomiasMetas.Where(tb => tb.IdPessoa == ContaId).ToListAsync();
            return EconomiasMeta;
        }


        public async Task AdicionarEconomia(double Valor, int Id, int economiaId)
        {
            if (Valor == 0)
                throw new Exception("A sua economia deve valer algo!");

            
            var economia = await AppContext.EconomiasMetas.Where(tb => tb.Id == economiaId).FirstOrDefaultAsync();

            if (economia == null)
                economia = new EconomiasMetaDal { Id = 0, Nome = "" };
            else
                economia.Valor += Valor;

            await AppContext.Economias.AddAsync(new EconomiasDal { Valor = Valor, IdPessoa = Id, Data = DateTime.Now, IdEconomiaMeta = economia.Id, NomeEconomiaMeta = economia.Nome});
            await AppContext.SaveChangesAsync();

        }

        public async Task AdicionarEconomiaMeta(string nomeMeta, double Valor, int Id)
        {
            if (Valor == 0)
                throw new Exception("A sua economia deve valer algo!");


            await AppContext.EconomiasMetas.AddAsync(new EconomiasMetaDal { ValorTotal = Valor, IdPessoa = Id, Nome = nomeMeta });
            await AppContext.SaveChangesAsync();

        }

        public async Task RemoverEconomia(int Id)
        {
            var Economia = await AppContext.Economias.Where(tb => tb.Id == Id).FirstOrDefaultAsync();
            var EconomiaMeta = await AppContext.EconomiasMetas.Where(tb => tb.Id == Economia.IdEconomiaMeta).FirstOrDefaultAsync();
            EconomiaMeta.Valor -= Economia.Valor;
            AppContext.Economias.Remove(Economia);
            await AppContext.SaveChangesAsync();
        }



    }
}

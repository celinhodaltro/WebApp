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




    }
}

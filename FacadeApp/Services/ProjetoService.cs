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
    public class ProjetoService : BaseService
    {
        public ProjetoService(DataBaseContext item) : base(item) { }

        public async Task<List<ProjetoDal>> ConsultarTodos()
        {
            var projetos = await AppContext.Projetos.OrderBy(tb=>tb.Prioridade).ToListAsync();
            return projetos;
        }

        public async Task Adicionar(string nome , int prioridade, string descricao, int idCriador)
        {
            var verificarprojeto = await AppContext.Projetos.Where(tb => tb.Nome == nome && tb.IdCriador == idCriador).FirstOrDefaultAsync();
            if (verificarprojeto != null)
                throw new Exception("Este projeto ja existe.");
            await AppContext.Projetos.AddAsync(new ProjetoDal { Nome = nome, Prioridade = prioridade, Desc = descricao, IdCriador = idCriador });
            await AppContext.SaveChangesAsync();
        }









    }
}

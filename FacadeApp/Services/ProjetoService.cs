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
        public async Task<ProjetoDal> Consultar(int id)
        {
            var projeto = await AppContext.Projetos.Where(tb => tb.Id == id).FirstOrDefaultAsync();
            return projeto;
        }

        public async Task<List<ProjetoDal>> ConsultarProjetosDoUsuario(int id)
        {
            var projetoId = await AppContext.ContaProjeto.Where(tb => tb.IdConta == id).Select(tb=>tb.IdProjeto).ToListAsync();

            List<ProjetoDal> Projetos = new List<ProjetoDal>();

            foreach(var obj in projetoId)
            {
                var projeto = await AppContext.Projetos.Where(tb => tb.Id == obj).FirstOrDefaultAsync();
                Projetos.Add(projeto);
                projeto = null;
            }

            return Projetos;
        }

        public async Task Editar(int id, ProjetoDal projetodal)
        {
            var projeto = await AppContext.Projetos.Where(tb => tb.Id == id).FirstOrDefaultAsync();
            projeto.Nome = projetodal.Nome;
            projeto.Desc = projetodal.Desc;
            projeto.Prioridade = projetodal.Prioridade;
            await AppContext.SaveChangesAsync();

        }

        public async Task Adicionar(ProjetoDal projeto, int idCriador)
        {
            var verificarprojeto = await AppContext.Projetos.Where(tb => tb.Nome == projeto.Nome).FirstOrDefaultAsync();
            if (verificarprojeto != null)
                throw new Exception("Este projeto ja existe.");
            await AppContext.Projetos.AddAsync(new ProjetoDal { Nome = projeto.Nome, Prioridade = projeto.Prioridade, Desc = projeto.Desc, IdCriador = idCriador });
            await AppContext.SaveChangesAsync();
        }









    }
}

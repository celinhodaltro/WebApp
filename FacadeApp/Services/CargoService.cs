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
    public class CargoService : BaseService
    {
        public CargoService(DataBaseContext item) : base(item) { }

        public async Task<List<CargoDal>> ConsultarTodos()
        {
            var cargos = await AppContext.Cargos.OrderBy(tb=>tb.Nivel).ToListAsync();
            return cargos;
        }

        public async Task<CargoDal> Consultar(int id)
        {
            var cargo = await AppContext.Cargos.Where(tb=> tb.Id == id).FirstOrDefaultAsync();
            return cargo;
        }

        public async Task Editar(int id, CargoDal cargoDal)
        {
            var cargo = await AppContext.Cargos.Where(tb => tb.Id == id).FirstOrDefaultAsync();

            cargo.Nivel = cargoDal.Nivel;
            cargo.Nome = cargoDal.Nome;
            await AppContext.SaveChangesAsync();

        }

        public async Task Adicionar(string Nome, int Nivel)
        {
            if (Nome == "")
                throw new Exception("A tarefa deve conter pelo menos uma letra.");

            if (Nivel == 0)
                throw new Exception("Adicione um Nivel para o cargo.");


            await AppContext.Cargos.AddAsync(new CargoDal { Nome = Nome, Nivel = Nivel });
            await AppContext.SaveChangesAsync();

        }

        public async Task RemoverCargo(int Id)
        {
            var cargos = await AppContext.Cargos.Where(tb => tb.Id == Id).ToListAsync();
            AppContext.Cargos.RemoveRange(cargos);
            await AppContext.SaveChangesAsync();
        }

        public async Task<List<CargoDal>> ConsultarCargosDoUsuario(int contaid)
        {
            var contacargos = await AppContext.ContaCargo.Where(tb => tb.IdConta == contaid).Select(tb => tb.IdCargo).ToListAsync();
            var cargos = new List<CargoDal>();
            foreach (var cargo in contacargos)
            {
                var cargocontext = await AppContext.Cargos.Where(tb => tb.Id == cargo).FirstOrDefaultAsync();
                cargos.Add(cargocontext);
                cargocontext = null;
            }

            return cargos;
        }

        public async Task AtribuirCargoUsuario(int idPessoa, int idCargo)
        {
            var verificarcargo = await AppContext.ContaCargo.Where(tb => tb.IdConta == idPessoa && tb.IdCargo == idCargo).FirstOrDefaultAsync();
            if (verificarcargo != null)
                throw new Exception("Esta conta já tem este cargo");
            await AppContext.ContaCargo.AddAsync(new ContaCargoDal { IdConta = idPessoa, IdCargo = idCargo });
            await AppContext.SaveChangesAsync();
        }

        public async Task RemoverCargoUsuario(int idPessoa, int idCargo)
        {
            var contaCargos = await AppContext.ContaCargo.Where(tb => tb.IdConta == idPessoa && tb.IdCargo == idCargo).ToListAsync();
            AppContext.ContaCargo.RemoveRange(contaCargos);
            await AppContext.SaveChangesAsync();
        }






    }
}

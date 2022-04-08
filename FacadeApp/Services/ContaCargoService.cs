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
    public class ContaCargoService : BaseService
    {
        public ContaCargoService(DataBaseContext item) : base(item) { }

        public async Task<List<CargoDal>> ConsultarCargos(int contaid)
        {
            var contacargos = await AppContext.ContaCargo.Where(tb=> tb.IdConta == contaid).Select(tb=> tb.IdCargo).ToListAsync();
            var cargos = new List<CargoDal>();
            foreach(var cargo in contacargos)
            {
                var cargocontext = await AppContext.Cargos.Where(tb => tb.Id == cargo).FirstOrDefaultAsync();
                cargos.Add(cargocontext);
                cargocontext = null;
            }

            return cargos;
        }

        public async Task<CargoDal> Consultar(int id)
        {
            var cargo = await AppContext.Cargos.Where(tb=> tb.Id == id).FirstOrDefaultAsync();
            return cargo;
        }

        public async Task Adicionar(int idPessoa, int idCargo )
        {
            var verificarcargo = await AppContext.ContaCargo.Where(tb => tb.IdConta == idPessoa && tb.IdCargo == idCargo).FirstOrDefaultAsync();
            if (verificarcargo != null)
                throw new Exception("Esta conta já tem este cargo");
            await AppContext.ContaCargo.AddAsync(new ContaCargoDal { IdConta = idPessoa, IdCargo = idCargo });
            await AppContext.SaveChangesAsync();
        }

        public async Task Remover(int idPessoa, int idCargo)
        {
            var contaCargos = await AppContext.ContaCargo.Where(tb => tb.IdConta == idPessoa && tb.IdCargo == idCargo).ToListAsync();
            AppContext.ContaCargo.RemoveRange(contaCargos);
            await AppContext.SaveChangesAsync();
        }








    }
}

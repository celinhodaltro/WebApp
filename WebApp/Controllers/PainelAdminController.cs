using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lib.Dto;
using Microsoft.AspNetCore.Authentication;
using Lib.Application;
using Lib.Data;


namespace WebApp.Controllers
{
    public class PainelAdminController : BaseController
    {

        public PainelAdminController(Facade facade) : base(facade)
        {
        }


        public async Task<IActionResult> Cargos()
        {
            var cargos = await FacadeApplication.Cargo.ConsultarTodos();
            return View(cargos);
        }

        public async Task<IActionResult> EditarCargo(int id)
        {
            var cargo = await FacadeApplication.Cargo.Consultar(id);
            return View(cargo);
        }
        [HttpPost]
        public async Task<IActionResult> EditarCargo(int id, CargoDal cargodal)
        {
            await FacadeApplication.Cargo.Editar(id, cargodal);
            return RedirectToAction("Cargos", "PainelAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCargo()
        {
            var Nome = Convert.ToString(Request.Form["Nome"]);
            var Nivel = Convert.ToInt32(Request.Form["Nivel"]);
            await FacadeApplication.Cargo.Adicionar(Nome, Nivel);
            return RedirectToAction("Cargos", "PainelAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> RemoverCargo(int Id)
        {
            await FacadeApplication.Planejamento.RemoverTarefa(Id);
            return RedirectToAction("Tarefas", "Planejamento");
        }





    }
}

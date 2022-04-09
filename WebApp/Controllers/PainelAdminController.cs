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

        public async Task<IActionResult> Index()
        {
            var contas = await FacadeApplication.Conta.ConsultarTodos();
            return View(contas);
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

        public async Task<IActionResult> AdicionarCargo(int id)
        {
            var cargos = new CargoPageDto();
            cargos.CargosPessoa = await FacadeApplication.ContaCargo.ConsultarCargos(id);
            cargos.Cargos = await FacadeApplication.Cargo.ConsultarTodos();
            return View(cargos);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarContaCargo(int id, string nome)
        {
            var idCargo = Convert.ToInt32(Request.Form["Cargo"]);
            await FacadeApplication.ContaCargo.Adicionar(id, idCargo);
            return RedirectToAction("AdicionarCargo", "PainelAdmin", new {id = id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoverContaCargo(int idConta, int idCargo)
        {
            await FacadeApplication.ContaCargo.Remover(idConta, idCargo);
            return RedirectToAction("AdicionarCargo", "PainelAdmin", new { id = idConta });
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

        public async Task<IActionResult> Projetos(int Id)
        {
            var projetos = await FacadeApplication.Projeto.ConsultarTodos();
            return View(projetos);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProjeto()
        {
            var Nome = Convert.ToString(Request.Form["Nome"]);
            var Nivel = Convert.ToInt32(Request.Form["Prioridade"]);
            var Desc = Convert.ToString(Request.Form["Des"]);
            await FacadeApplication.Projeto.Adicionar(Nome, Nivel, Desc, Convert.ToInt32(User.Identity.Name));
            return RedirectToAction("Projetos", "PainelAdmin");
        }






    }
}

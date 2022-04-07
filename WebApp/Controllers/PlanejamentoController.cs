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
    public class PlanejamentoController : BaseController
    {

        public PlanejamentoController(Facade facade) : base(facade)
        {
        }

        //Tarefas
        public async Task<IActionResult> Tarefas()
        {
            var tarefas = await FacadeApplication.Planejamento.ConsultarTarefas(DateTime.Today, Convert.ToInt32(User.Identity.Name));
            return View(tarefas);
        }



        [HttpPost]
        public async Task<IActionResult> Tarefas(List<TarefaDal> tarefas) 
        {
            await FacadeApplication.Planejamento.AlterarFeitos(tarefas);
            return RedirectToAction("Tarefas", "Planejamento");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTarefa()
        {
            var Nome = Convert.ToString(Request.Form["NomeDaTarefa"]);
            await FacadeApplication.Planejamento.AdicionarTarefa(Nome, Convert.ToInt32(User.Identity.Name));
            return RedirectToAction("Tarefas", "Planejamento");
        }

        [HttpPost]
        public async Task<IActionResult> RemoverTarefa(int Id)
        {
            await FacadeApplication.Planejamento.RemoverTarefa(Id);
            return RedirectToAction("Tarefas", "Planejamento");
        }


        //Economias

        public async Task<IActionResult> Economias()
        {
            var economiaDto = new EconomiaPageDto();
            economiaDto.Economias = await FacadeApplication.Planejamento.ConsultarEconomias(DateTime.Today, Convert.ToInt32(User.Identity.Name));
            economiaDto.EconomiasMeta = await FacadeApplication.Planejamento.ConsultarEconomiasMetas(Convert.ToInt32(User.Identity.Name));

            return View(economiaDto);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEconomia()
        {
            var Valor = Convert.ToDouble(Request.Form["Valor"]);
            await FacadeApplication.Planejamento.AdicionarEconomia(Valor, Convert.ToInt32(User.Identity.Name));
            return RedirectToAction("Economias", "Planejamento");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEconomiaMeta()
        {
            var Nome = Convert.ToString(Request.Form["NomeMeta"]);
            var Valor = Convert.ToDouble(Request.Form["ValorMeta"]);
            await FacadeApplication.Planejamento.AdicionarEconomiaMeta(Nome, Valor, Convert.ToInt32(User.Identity.Name));
            return RedirectToAction("Economias", "Planejamento");
        }


    }
}

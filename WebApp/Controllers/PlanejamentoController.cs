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

    }
}

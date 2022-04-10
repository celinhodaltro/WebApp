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

        //Cargos
        public async Task<IActionResult> Cargos()
        {
            var cargos = await FacadeApplication.Cargo.ConsultarTodos();
            return View(cargos);
        }

        public async Task<IActionResult> AdicionarCargo(int id)
        {
            var cargos = new CargoPageDto();
            cargos.CargosPessoa = await FacadeApplication.Cargo.ConsultarCargosDoUsuario(id);
            cargos.Cargos = await FacadeApplication.Cargo.ConsultarTodos();
            cargos.IdPessoa = id;
            return View(cargos);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarContaCargo(int id, string nome)
        {
            var idCargo = Convert.ToInt32(Request.Form["Cargo"]);
            await FacadeApplication.Cargo.AtribuirCargoUsuario(id, idCargo);
            return RedirectToAction("AdicionarCargo", "PainelAdmin", new {id = id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoverContaCargo(int idConta, int idCargo)
        {
            await FacadeApplication.Cargo.RemoverCargoUsuario(idConta, idCargo);
            return RedirectToAction("AdicionarCargo", "PainelAdmin", new { id = idConta });
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


        //Projetos
        public async Task<IActionResult> Projetos(int Id)
        {
            var projetos = await FacadeApplication.Projeto.ConsultarTodos();
            return View(projetos);
        }

        public IActionResult CriarProjeto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarProjeto(ProjetoDal projeto)
        {
            await FacadeApplication.Projeto.Adicionar(projeto, Convert.ToInt32(User.Identity.Name));
            return RedirectToAction("Projetos", "PainelAdmin");
        }

        public async Task<IActionResult> EditarProjeto(int id)
        {
            var projeto = await FacadeApplication.Projeto.Consultar(id);
            return View(projeto);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProjeto(int id, ProjetoDal projeto)
        {
            await FacadeApplication.Projeto.Editar(id, projeto);
            return RedirectToAction("Projetos", "PainelAdmin");
        }

        public async Task<IActionResult> AtribuirProjeto(int id)
        {
            var AtribuirProjetoDto = new AtribuirProjetoDto();
            AtribuirProjetoDto.Projetos = await FacadeApplication.Projeto.ConsultarTodos();
            AtribuirProjetoDto.ProjetosUsuario = await FacadeApplication.Projeto.ConsultarProjetosDoUsuario(id);

            return View(AtribuirProjetoDto);
        }

        [HttpPost]
        public async Task<IActionResult> AtribuirProjeto(int id, string project)
        {
            var Projeto = Convert.ToInt32(Request.Form["Projeto"]);
            var Func = Convert.ToString(Request.Form["Funcao"]);
            var Gerente= Request.Form["Gerente"].Count()!=0?true:false;

            await FacadeApplication.Projeto.AtribuirProjetoUsuario(Projeto, id, Gerente, Func);

            return RedirectToAction("AtribuirProjeto", "PainelAdmin", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoverProjetoUsuario(int IdUsuario, int IdProjeto)
        {
            await FacadeApplication.Projeto.RemoverUsuarioDoProjeto(IdProjeto, IdUsuario);

            return RedirectToAction("AtribuirProjeto", "PainelAdmin", new { id = IdUsuario });
        }





    }
}

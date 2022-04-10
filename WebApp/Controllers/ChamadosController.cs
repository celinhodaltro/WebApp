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
    public class ChamadosController : BaseController
    {

        public ChamadosController(Facade facade) : base(facade)
        {
        }

        public async Task<IActionResult> Index()
        {
            var usuario = Convert.ToInt32(User.Identity.Name);
            var projetos = await FacadeApplication.Projeto.ConsultarProjetosDoUsuario(usuario);
            return View(projetos);
        }
    }
}

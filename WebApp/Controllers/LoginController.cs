using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lib.Dto;
using Microsoft.AspNetCore.Authentication;
using Lib.Application;

namespace WebApp.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(Facade facade) : base(facade)
        {
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Entrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(ContaDto conta)
        {
            return View(conta);
        }

        public IActionResult Criar()
        {
            return View(new NovaContaDto());
        }

        [HttpPost]
        public IActionResult Criar(NovaContaDto novaconta)
        {
            FacadeApplication.Conta.CriarConta(novaconta);
            return View();
        }

        public async Task<IActionResult> Lista()
        {
            var contas = await FacadeApplication.Conta.ConsultarTodos();
            return View(contas);
        }


        private void EntrarConta(ContaDto usuario)
        {
            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                };
            var userIdentity = new ClaimsIdentity(userClaims, nameof(userClaims));
            var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

            HttpContext.SignInAsync(userPrincipal);
        }
    }
}

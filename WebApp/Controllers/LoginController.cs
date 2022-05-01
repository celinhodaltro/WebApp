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
            ViewData["Entrar"] = true;

            return View(new ContaDto());
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(ContaDto contadto)
        {
            try
            {
                var Conta = await FacadeApplication.Conta.ConsultarEntrada(nome: contadto.Conta, senha: contadto.Senha);
                if (Conta == null)
                    throw new Exception("Conta invalida.");
                EntrarConta(Conta);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewData["Entrar"] = true;
                return View(contadto);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Criar()
        {
            ViewData["Entrar"] = true;
            return View(new NovaContaDto());
        }

        [HttpPost]
        public async Task<IActionResult> Criar(NovaContaDto novaconta)
        {
            ViewData["Entrar"] = true;
            await FacadeApplication.Conta.CriarConta(novaconta);
            return View();
        }

        public async Task<IActionResult> Lista()
        {
            ViewData["Entrar"] = true;
            var contas = await FacadeApplication.Conta.ConsultarTodos();
            return View(contas);
        }


        private void EntrarConta(ContaDal usuario)
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

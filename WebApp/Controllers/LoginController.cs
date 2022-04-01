using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lib.Dto;
using Microsoft.AspNetCore.Authentication;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        private void Entrar(ContaDto usuario)
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

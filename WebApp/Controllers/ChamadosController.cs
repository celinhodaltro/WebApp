﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(int id)
        {


            ChamadoPageDto chamadosPageDto = new();
            var usuario = Convert.ToInt32(User.Identity.Name);
            chamadosPageDto.Projetos = await FacadeApplication.Projeto.ConsultarProjetosDoUsuario(usuario);

            if (id != 0)
                chamadosPageDto.Chamados = await FacadeApplication.Chamado.ConsultarChamadosDoUsuario(usuario, id);
            else
                chamadosPageDto.Chamados = await FacadeApplication.Chamado.ConsultarChamadosDoUsuario(usuario);

            return View(chamadosPageDto);
        }

        public async Task<IActionResult> Chamado(int id)
        {
            ChamadoSolicitacaoPageDto chamadoSolicitacaoPageDto = new();
            chamadoSolicitacaoPageDto.Solicitacoes = await FacadeApplication.Chamado.ConsultarSolicitacaoChamado(id);
            chamadoSolicitacaoPageDto.Atribuinte = await FacadeApplication.Chamado.ChecarAtribuicao(Convert.ToInt32(User.Identity.Name), id, true);
            var chamado = await FacadeApplication.Chamado.Consultar(id);
            chamadoSolicitacaoPageDto.SolicitarAtivo = chamado.IdStatus < (int)Lib.Enum.ChamadoStatus.Aprovado? true: false;

            return View(chamadoSolicitacaoPageDto);
        }


        [HttpPost]
        public async Task<IActionResult> Chamado(int id, ChamadoSolicitacaoPageDto chamadoSolicitacaoPageDto)
        {


            var idConta = Convert.ToInt32(User.Identity.Name);
            var Conta = await FacadeApplication.Conta.Consultar(id: idConta);

            chamadoSolicitacaoPageDto.Solicitar.IdChamado = id;
            chamadoSolicitacaoPageDto.Solicitar.NomeDoAutor = Conta.Conta;
            chamadoSolicitacaoPageDto.Solicitar.EmailDoAutor = Conta.Email;
            chamadoSolicitacaoPageDto.Solicitar.Data = DateTime.Now;
            


            var ContasAtribuintes = await FacadeApplication.Conta.ConsultarUsuariosDoChamado(idConta, atribuinte: true);
            foreach (var ContaAtribuinte in ContasAtribuintes)
                if (ContaAtribuinte.Id == idConta)
                    chamadoSolicitacaoPageDto.Solicitar.Atribuinte = true;



            await FacadeApplication.Chamado.AdicionarSolicitacao(chamadoSolicitacaoPageDto.Solicitar);


            chamadoSolicitacaoPageDto.Solicitacoes = await FacadeApplication.Chamado.ConsultarSolicitacaoChamado(id);
            chamadoSolicitacaoPageDto.Atribuinte = await FacadeApplication.Chamado.ChecarAtribuicao(Convert.ToInt32(User.Identity.Name), id, true);
            var chamado = await FacadeApplication.Chamado.Consultar(id);
            chamadoSolicitacaoPageDto.SolicitarAtivo = chamado.IdStatus < (int)Lib.Enum.ChamadoStatus.Aprovado ? true : false;

            return View(chamadoSolicitacaoPageDto);
        }


        public async Task<IActionResult> AceitarSolicitacao(int id)
        {
            var Conta = await FacadeApplication.Conta.Consultar(Convert.ToInt32(User.Identity.Name));
            await FacadeApplication.Chamado.AceitarSolicitacao(id, Conta);

            return RedirectToAction("Index","Chamados");
        }

        public async Task<IActionResult> NegarChamado(int id)
        {
            var solicitacao = await FacadeApplication.Chamado.ConsultarSolicitacaoChamado(id, true);
            return View(solicitacao);
        }


        [HttpPost]
        public async Task<IActionResult> NegarChamado(int id, string mensagem)
        {
            var conta = await FacadeApplication.Conta.Consultar(id : Convert.ToInt32(User.Identity.Name));
            await FacadeApplication.Chamado.RecusarSolicitacao(mensagem, conta, id);

            return View();
        }



    }
}

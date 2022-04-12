﻿using Lib.Data;
using Lib.Dto;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeApp.Services
{
    public class ContaService : BaseService
    {
        public ContaService(DataBaseContext item) : base(item) { }

        public async Task<List<ContaDal>> ConsultarTodos()
        {
            var quantidadeContas = await AppContext.Contas.CountAsync();
            var contas = await AppContext.Contas.Take(quantidadeContas >= 10 ? 10 : quantidadeContas).ToListAsync();
            return contas;
        }

        public async Task<List<ContaDal>> ConsultarUsuariosDoProjeto(int idProjeto)
        {

            var contasId = await AppContext.ContaProjeto.Where(tb => tb.IdProjeto == idProjeto).Select(tb => tb.IdConta).ToListAsync();

            List<ContaDal> Contas = new();

            foreach (var obj in contasId)
            {
                var conta = await AppContext.Contas.Where(tb => tb.Id == obj).FirstOrDefaultAsync();
                Contas.Add(conta);
                conta = null;
            }

            return Contas;
        }


        public async Task<ContaDal> Consultar(int id = 0, string nome = "", string email = "", string senha = "")
        {

            return await AppContext.Contas
                .Where(tb => tb.Id == id || tb.Conta == nome || tb.Email == email || tb.Senha == senha)
                .FirstOrDefaultAsync();
        }

        public async Task<ContaDal> ConsultarEntrada(string nome = "", string senha = "")
        {

            return await AppContext.Contas
                .Where(tb => (tb.Conta == nome || tb.Email == nome) && tb.Senha == senha)
                .FirstOrDefaultAsync();
        }

        public ContaDal Validar(NovaContaDto conta)
        {
            if (conta.Conta.Length <= 3)
                throw new Exception("Os digitos da conta devem ser superior a 3 digitos");
            if (conta.Senha.Length <= 3)
                throw new Exception("Os digitos da senha devem ser superior a 5 digitos");
            if (conta.SenhaRepetida != conta.Senha)
                throw new Exception("Senha repetida deve ser identica á sua senha.");
            if (conta.Email.Length <= 4)
                throw new Exception("Email invalido.");

            if (Consultar(conta.Id, conta.Conta, conta.Email, conta.Senha) != null)
                throw new Exception("Está conta já existe.");


            return new ContaDal { Conta = conta.Conta, Email = conta.Email, Senha = conta.Senha };

        }

        public async Task CriarConta(NovaContaDto conta)
        {
            var novaconta = Validar(conta);
            await AppContext.Contas.AddAsync(novaconta);
            await AppContext.SaveChangesAsync();
        }



    }
}

using Lib.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Lib.Enum;
using System;

namespace FacadeApp.Services
{
    public class ChamadoService : BaseService
    {
        public ChamadoService(DataBaseContext item) : base(item) { }

        public async Task<List<ChamadoDal>> ConsultarTodos(bool arquivados = false)
        {
            var chamados = await AppContext.Chamados.Where(tb => tb.Arquivado == arquivados).ToListAsync();
            return chamados;
        }

        public async Task<ChamadoDal> Consultar(int id)
        {
            var chamado = await AppContext.Chamados.Where(tb => tb.Id == id).FirstOrDefaultAsync();
            return chamado;
        }

        public async Task<bool> ChecarAtribuicao(int idConta, int idChamado, bool atribuinte = false)
        {
            bool resultado = false;
            List<ChamadoContaDal> chamadoConta = new();
            if (atribuinte)
                chamadoConta = await AppContext.ChamadoConta.Where(tb => tb.IdContaAtribuinte == idConta && tb.IdChamado == idChamado).ToListAsync();
            else
                chamadoConta = await AppContext.ChamadoConta.Where(tb => tb.IdContaAtribuido == idConta && tb.IdChamado == idChamado).ToListAsync();

            if (chamadoConta.Count != 0)
                resultado = true;

            return resultado;
        }

        public async Task<List<ChamadoDal>> ConsultarChamadosDoUsuario(int id, int idProjeto = 0)
        {

            var chamadosId = await AppContext.ChamadoConta.Where(tb => tb.IdContaAtribuido == id || tb.IdContaAtribuinte == id).Select(tb => tb.IdChamado).ToListAsync();


            List<ChamadoDal> chamados = new();
            foreach (var chamadoid in chamadosId)
            {
                ChamadoDal chamado = new();
                if (idProjeto != 0)
                    chamado = await AppContext.Chamados.Where(tb => tb.Id == chamadoid && tb.IdProjeto == idProjeto).FirstOrDefaultAsync();
                else
                    chamado = await AppContext.Chamados.Where(tb => tb.Id == chamadoid).FirstOrDefaultAsync();
                if (chamado != null)
                    chamados.Add(chamado);

                chamado = null;
            }
            return chamados;
        }

        public async Task Adicionar(ChamadoDal chamado)
        {
            await AppContext.Chamados.AddAsync(new ChamadoDal { Nome = chamado.Nome, Desc = chamado.Desc, IdProjeto = chamado.IdProjeto, IdStatus = (int)ChamadoStatus.Processando, IdTipoChamado = chamado.IdTipoChamado, Arquivado = false, NomeProjeto = chamado.NomeProjeto, Prioridade = chamado.Prioridade });
            await AppContext.SaveChangesAsync();
        }

        public async Task Atribuir(int idChamado, int idAtribuinte, int idAtribuido)
        {
            await AppContext.ChamadoConta.AddAsync(new ChamadoContaDal { IdChamado = idChamado, IdContaAtribuinte = idAtribuinte, IdContaAtribuido = idAtribuido });
            var contaAtribuinte = await AppContext.Contas.Where(tb => tb.Id == idAtribuinte).FirstOrDefaultAsync();
            var contaAtribuido = await AppContext.Contas.Where(tb => tb.Id == idAtribuido).FirstOrDefaultAsync();

            await AppContext.ChamadoSolicitacaoDal.AddAsync(new ChamadoSolicitacaoDal { Atribuinte = true, EmailDoAutor = contaAtribuinte.Email, NomeDoAutor = contaAtribuinte.Conta, IdChamado = idChamado, idTipoSolicitacao = 0, Mensagem = $"Chamado atribuido de {contaAtribuinte.Conta} para {contaAtribuido.Conta}" });

            await AppContext.SaveChangesAsync();
        }

        public async Task<List<ChamadoSolicitacaoDal>> ConsultarSolicitacaoChamado(int id)
        {
            var solicitacaoChamado = await AppContext.ChamadoSolicitacaoDal.Where(tb => tb.IdChamado == id).OrderByDescending(tb => tb.Data).ToListAsync();
            return solicitacaoChamado;
        }

        public async Task<ChamadoSolicitacaoDal> ConsultarSolicitacaoChamado(int id, bool solicitacao)
        {
            var solicitacaoChamado = await AppContext.ChamadoSolicitacaoDal.Where(tb => tb.Id == id).FirstOrDefaultAsync();
            return solicitacaoChamado;
        }

        public async Task AdicionarSolicitacao(ChamadoSolicitacaoDal chamadoSolicitacaoDal)
        {
            await AppContext.AddAsync(chamadoSolicitacaoDal);
            await AppContext.SaveChangesAsync();
        }

        public async Task AceitarSolicitacao(int Id, ContaDal conta)
        {
            var solicitacao = await AppContext.ChamadoSolicitacaoDal.Where(tb => tb.Id == Id).FirstOrDefaultAsync();
            var chamado = await AppContext.Chamados.Where(tb => tb.Id == solicitacao.IdChamado).FirstOrDefaultAsync();

            solicitacao.Processado = true;

            ChamadoSolicitacaoDal chamadoSolicitacao = new ChamadoSolicitacaoDal
            {
                Atribuinte = true,
                EmailDoAutor = conta.Email,
                NomeDoAutor = conta.Conta,
                IdChamado = chamado.Id,
                Data = DateTime.Now
            };


            if (solicitacao.idTipoSolicitacao == (int)Lib.Enum.TipoDeChamadoSolicitacaoEnum.Aprovacao) {
                chamado.IdStatus = (int)Lib.Enum.ChamadoStatus.Aprovado;
                chamadoSolicitacao.idTipoSolicitacao = (int)Lib.Enum.TipoDeChamadoSolicitacaoEnum.Aprovado;
            }
            else if (solicitacao.idTipoSolicitacao == (int)Lib.Enum.TipoDeChamadoSolicitacaoEnum.Arquivamento) { 
                chamado.IdStatus = (int)Lib.Enum.ChamadoStatus.Arquivado;
                chamadoSolicitacao.idTipoSolicitacao = (int)Lib.Enum.TipoDeChamadoSolicitacaoEnum.Arquivado;
            }
            else if (solicitacao.idTipoSolicitacao == (int)Lib.Enum.TipoDeChamadoSolicitacaoEnum.Encerramento) { 
                chamado.IdStatus = (int)Lib.Enum.ChamadoStatus.Encerrado;
                chamadoSolicitacao.idTipoSolicitacao = (int)Lib.Enum.TipoDeChamadoSolicitacaoEnum.Encerrado;
            }

            chamadoSolicitacao.Mensagem = $"Este chamado foi {(Lib.Enum.TipoDeChamadoSolicitacaoEnum)chamadoSolicitacao.idTipoSolicitacao} por:  {conta.Conta}";


            await AppContext.ChamadoSolicitacaoDal.AddAsync(chamadoSolicitacao);
            await AppContext.SaveChangesAsync();
        }

        public async Task RecusarSolicitacao(string mensagem, ContaDal conta, int idSolicitacao)
        {
            var solicitacao = await AppContext.ChamadoSolicitacaoDal.Where(tb => tb.Id == idSolicitacao).FirstOrDefaultAsync();
            solicitacao.Processado = true;
            var chamado = await Consultar(solicitacao.IdChamado);

            chamado.IdStatus = (int)Lib.Enum.ChamadoStatus.Processando;


            ChamadoSolicitacaoDal chamadoSolicitacao = new ChamadoSolicitacaoDal { 
                idTipoSolicitacao = (int)Lib.Enum.TipoDeChamadoSolicitacaoEnum.Recusado,
                Mensagem = mensagem,
                Atribuinte = true,
                EmailDoAutor = conta.Email,
                NomeDoAutor = conta.Conta,
                IdChamado = solicitacao.IdChamado,
                Data = DateTime.Now};


            await AppContext.ChamadoSolicitacaoDal.AddAsync(chamadoSolicitacao);

            await AppContext.SaveChangesAsync();
                

        }








    }
}

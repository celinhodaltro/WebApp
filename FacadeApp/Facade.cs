using FacadeApp.Services;
using Lib.Data;
using System;

namespace Lib.Application
{
    public class Facade : IDisposable
    {
        private DataBaseContext __context = null;
        private ContaService __conta = null;
        private PlanejamentoService __planejamento = null;
        private CargoService __cargo = null;
        private ContaCargoService __contacargo = null;

        public Facade()
        {
            __context = new DataBaseContext();
        }

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    __context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ContaService Conta
        {
            get
            {
                if (__conta == null)
                    __conta = new ContaService(__context);
                return __conta;
            }
        }

        public PlanejamentoService Planejamento
        {
            get
            {
                if (__planejamento == null)
                    __planejamento = new PlanejamentoService(__context);
                return __planejamento;
            }
        }

        public CargoService Cargo
        {
            get
            {
                if (__cargo == null)
                    __cargo = new CargoService(__context);
                return __cargo;
            }
        }

        public ContaCargoService ContaCargo
        {
            get
            {
                if (__contacargo == null)
                    __contacargo = new ContaCargoService(__context);
                return __contacargo;
            }
        }


    }
}
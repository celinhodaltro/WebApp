using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lib.Data;

namespace Lib.Data
{
    public class DataBaseContext : DbContext
    {
        public string strConnection { get; set; } = "Server=(localdb)\\MSSQLLocalDB;Database=WebAppDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(strConnection);
        }

        public DbSet<ContaDal> Contas { get; set; }
        public DbSet<TarefaDal> Tarefas { get; set; }
        public DbSet<EconomiasDal> Economias { get; set; }
        public DbSet<EconomiasMetaDal> EconomiasMetas { get; set; }



    }
}

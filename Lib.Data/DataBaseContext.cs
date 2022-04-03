using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lib.Data.Entities;

namespace Lib.Data
{
    public class DataBaseContext : DbContext
    {
        public string strConnection { get; set; } = "Server=(localdb)\\MSSQLLocalDB;Database=Blazor_Base_ProjectDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(strConnection);
        }

        public DbSet<ContaDal> Contas { get; set; }



    }
}

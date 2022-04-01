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

        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ContaDal> Contas { get; set; }



    }
}

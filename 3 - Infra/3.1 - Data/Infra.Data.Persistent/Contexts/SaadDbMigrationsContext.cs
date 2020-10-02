using System;
using Infra.Data.Core.ConnectionStrings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Persistent.Contexts
{
    public class SaadDbMigrationsContext : SaadDbContext
    {
        public SaadDbMigrationsContext()
        {
        }

        private readonly ConnectionStringHandler connectionStringHandler; 
        public SaadDbMigrationsContext(ConnectionStringHandler connectionStringHandler
        ,DbContextOptions options) : base(connectionStringHandler, options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string _connString = string.Empty;
           
	        _connString = "Data Source=SQL5080.site4now.net;Initial Catalog=DB_A2C5A5_saad;User Id=DB_A2C5A5_saad_admin;Password=@provasaad2020;";

			Console.WriteLine("------ ");
            Console.WriteLine(_connString);

            optionsBuilder.UseSqlServer(_connString);
        }

    }
}
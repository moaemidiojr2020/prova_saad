using Microsoft.Extensions.Configuration;

namespace Infra.Data.Core.ConnectionStrings
{
    public class ConnectionStringHandler
    {
       
        public string GetConnectionString()
        {
	        var connString = "Data Source=SQL5080.site4now.net;Initial Catalog=DB_A2C5A5_saad;User Id=DB_A2C5A5_saad_admin;Password=@provasaad2020;";
            return connString;
        }
    }
}
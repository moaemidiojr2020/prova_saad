using Microsoft.Extensions.Configuration;

namespace Infra.Data.Core.ConnectionStrings
{
    public class ConnectionStringHandler
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
#if DEBUG
            var connString = _configuration.GetConnectionString("DefaultConnection");
            return connString;

#else

            var connString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            return connString;
            
#endif
        }
    }
}
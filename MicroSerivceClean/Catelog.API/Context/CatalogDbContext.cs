using Microsoft.Extensions.Configuration;
using MongoRepo.Context;

namespace Catelog.API.Context
{
    public class CatalogDbContext : ApplicationDbContext
    {

        static IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
        static string connectionString = config.GetConnectionString("CataLog.API");
        static string databaseName = config.GetValue<string>("DatabaseName");
        public CatalogDbContext() : base(connectionString, databaseName)
        {

        }
    }
}

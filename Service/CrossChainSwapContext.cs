using BlockChainTracer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlockChainTracer.Service
{
    public class CrossChainSwapContext : DbContext
    {

        private readonly IConfiguration _config;
        public DbSet<CrossChainSwap> CrossChainSwaps { get; set; }

        public CrossChainSwapContext(IConfiguration configuration)
        {
            _config = configuration;
            if (_config["useDatabase"].Equals("True"))
            {
                Database.EnsureCreated();
                Console.WriteLine("Connected to the database");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_config["useDatabase"].Equals("True"))
            {
                optionsBuilder.UseNpgsql(_config["dbParams"]);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

    }
}
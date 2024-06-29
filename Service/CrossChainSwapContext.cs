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
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config["dbParams"]);
            optionsBuilder.EnableSensitiveDataLogging();
        }

    }
}
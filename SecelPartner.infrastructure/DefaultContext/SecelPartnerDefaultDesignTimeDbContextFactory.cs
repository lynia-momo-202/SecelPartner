using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SecelPartner.Infrastructure.DefaultContext
{
    public class SecelPartnerDefaultDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SecelPartnerDataContext>
    {
        public SecelPartnerDataContext CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                               .SetBasePath(path)
                               .AddJsonFile("appsettings.json");


            var config = builder.Build();

            var connectionString = config.GetConnectionString("SecelPartnerDataContextConnection");

            DbContextOptionsBuilder<SecelPartnerDataContext> optionBuilder = new DbContextOptionsBuilder<SecelPartnerDataContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new SecelPartnerDataContext(optionBuilder.Options);
        }
    }
}

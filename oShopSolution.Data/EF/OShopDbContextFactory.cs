using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Data.EF
{
	class OShopDbContextFactory : IDesignTimeDbContextFactory<OShopDbContext>
	{
        public OShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("oShopSolutionDb");

            var optionsBuilder = new DbContextOptionsBuilder<OShopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new OShopDbContext(optionsBuilder.Options);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Meowv.Blog
{
    public class MeowvBlogMigrationsDbContextFactory : IDesignTimeDbContextFactory<MeowvBlogMigrationsDbContext>
    {
        public MeowvBlogMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MeowvBlogMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new MeowvBlogMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
          
            return builder.Build();
        }
    }
}

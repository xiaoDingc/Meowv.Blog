using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Meowv.Blog
{
   public class MeowvBlogMigrationsDbContext:AbpDbContext<MeowvBlogMigrationsDbContext>
    {
        public MeowvBlogMigrationsDbContext(DbContextOptions<MeowvBlogMigrationsDbContext>  options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configure();
        }
    }
}

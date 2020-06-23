﻿using System;
using System.Collections.Generic;
using System.Text;
using Meowv.Blog.Blog;
using Meowv.Blog.Domain.Wallpaper;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Meowv.Blog
{
    [ConnectionStringName("SqlServer")]
    public class MeowvBlogDbContext : AbpDbContext<MeowvBlogDbContext>
    {

        public MeowvBlogDbContext(DbContextOptions<MeowvBlogDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configure();
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<FriendLink> FriendLinks { get; set; }

        public DbSet<WallPaper> WallPapers { get; set; }

        public DbSet<HotNews.HotNews> HotNewses { get; set; }

    }
}

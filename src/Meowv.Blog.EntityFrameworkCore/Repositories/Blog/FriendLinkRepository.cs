using System;
using System.Collections.Generic;
using System.Text;
using Meowv.Blog.Blog;
using Meowv.Blog.Blog.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Meowv.Blog.Repositories.Blog
{
    public class FriendLinkRepository : EfCoreRepository<MeowvBlogDbContext, FriendLink, int>, IFriendLinkRepository
    {
        public FriendLinkRepository(IDbContextProvider<MeowvBlogDbContext> contextProvider):base(contextProvider)
        {
            
        }

    }
}

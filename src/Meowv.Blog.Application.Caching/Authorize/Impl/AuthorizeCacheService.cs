using Meowv.Blog.Domain.Shared;
using Meowv.Blog.ToolKits.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meowv.Blog.ToolKits.Extensions;

namespace Meowv.Blog.Application.Caching.Authorize.Impl
{
    public class AuthorizeCacheService : CachingServiceBase, IAuthorizeCacheService
    {
        private const string KEY_GetLoginAddress = "Authorize:GetLoginAddress";

        private const string KEY_GetAccessToken = "Authorize:GetAccessToken-{0}";

        private const string KEY_GenerateToken = "Authorize:GenerateToken-{0}";

        public async Task<ServiceResult<string>> GetLoginAddressAsync(Func<Task<ServiceResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GetLoginAddress, factory, CacheStrategy.NEVER);
        }

        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code, Func<Task<ServiceResult<string>>> factory)
        {
            var res=KEY_GetAccessToken.FormatWith(code);
            return await Cache.GetOrAddAsync(res, factory, CacheStrategy.FIVE_MINUTES);
        }

        public async Task<ServiceResult<string>> GenerateTokenAsync(string access_token, Func<Task<ServiceResult<string>>> factory)
        {
            return await Cache.GetOrAddAsync(KEY_GenerateToken.FormatWith(access_token), factory, CacheStrategy.ONE_HOURS);
        }
    }
}

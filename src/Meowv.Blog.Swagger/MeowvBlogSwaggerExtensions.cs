using Meowv.Blog.Domain.Configurations;
using Meowv.Blog.Swagger.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using static Meowv.Blog.Domain.Shared.MeowvBlogConsts;

namespace Meowv.Blog.Swagger
{
    public static class MeowvBlogSwaggerExtensions
    {
        public static string description="接口描述";
        public static string version=AppSettings.ApiVersion;

        private static readonly List<SwaggerApiInfo> swaggerApiInfos=new List<SwaggerApiInfo>
        {
            new SwaggerApiInfo
            {
                UrlPrefix=Grouping.GroupName_v1,
                 Name="博客前台接口",
                  OpenApiInfo=new OpenApiInfo
                  {
                      Version = version,
                    Title = "博客前台接口",
                    Description = description
                  }
            },
             new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v2,
                Name = "博客后台接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "博客后台接口",
                    Description = description
                }
            },
             new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v3,
                Name = "通用公共接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "通用公共接口",
                    Description = description
                }
            },
             new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v4,
                Name = "JWT授权接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "JWT授权接口",
                    Description = description
                }
            }
        };

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                swaggerApiInfos.ForEach(x=>options.SwaggerDoc(x.UrlPrefix,x.OpenApiInfo));

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Meowv.Blog.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Meowv.Blog.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Meowv.Blog.Application.Contracts.xml"));

                

                var security= new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };

                options.AddSecurityDefinition("oauth2",security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{{security,new List<string>()} });
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.DocumentFilter<SwaggerDocumentFilter>();
            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(option =>
            {
                swaggerApiInfos.ForEach(x =>
                {
                    option.SwaggerEndpoint($"/swagger/{x.UrlPrefix}/swagger.json", x.Name);
                });

                ///默认展开深度 -1完全隐藏模型
                option.DefaultModelExpandDepth(-1);
                //api文档仅展开标记
                option.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);

                //路由前缀设置为空
                option.RoutePrefix = string.Empty;
                // API页面Title
                option.DocumentTitle = "😍接口文档";

            });
        }

    }

    internal class SwaggerApiInfo
    {
        /// <summary>
        /// 网址前缀
        /// </summary>
        public string UrlPrefix { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// OpenApiInfo
        /// </summary>
        public OpenApiInfo OpenApiInfo { get; set; }

    }

 
}

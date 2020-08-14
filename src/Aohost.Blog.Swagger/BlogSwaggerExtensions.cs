using System;
using System.Collections.Generic;
using System.IO;
using Aohost.Blog.Domain.Configuration;
using Aohost.Blog.Domain.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Aohost.Blog.Swagger
{
    public static class BlogSwaggerExtensions
    {
        private static readonly List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>
        {
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v1,
                Name = "博客前台接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = AppSettings.ApiVersion,
                    Title = "Ahost - 博客前台接口",
                    Description = "博客前台接口"
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v2,
                Name = "博客后台接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = AppSettings.ApiVersion,
                    Title = "Aohost - 博客后台接口",
                    Description = "博客后台接口"
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v3,
                Name = "其他通用接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = AppSettings.ApiVersion,
                    Title = "Aohost - 其他通用接口",
                    Description = "其他通用接口"
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v4,
                Name = "JWT授权接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = AppSettings.ApiVersion,
                    Title = "Aohost - JWT授权接口",
                    Description = "JWT授权接口"
                }
            }
        };



        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                //options.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Version = "1.0.0",
                //    Title = "My Api",
                //    Description = "Api Desc"
                //});
                ApiInfos.ForEach(x => { options.SwaggerDoc(x.UrlPrefix, x.OpenApiInfo); });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Aohost.Blog.HttpApi.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Aohost.Blog.Domain.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    "Aohost.Blog.Application.Contracts.xml"));
            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                ApiInfos.ForEach(x => options.SwaggerEndpoint($"/swagger/{x.UrlPrefix}/swagger.json", x.Name));

                // 模型的默认扩展深度，设置为-1 完全隐藏模型
                options.DefaultModelExpandDepth(-1);
                // API文档仅展开标记
                options.DocExpansion(DocExpansion.List);
                // API前缀设置为空
                options.RoutePrefix = string.Empty;
                // API页面Title
                options.DocumentTitle = "接口文档 - Aohost";
            });
        }

        internal class SwaggerApiInfo
        {
            /// <summary>
            /// URL 前缀
            /// </summary>
            public string UrlPrefix { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// ApiInfo
            /// </summary>
            public OpenApiInfo OpenApiInfo { get; set; }
        }
    }
}

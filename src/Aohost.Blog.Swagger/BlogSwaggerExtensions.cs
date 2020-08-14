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
        private static readonly string version = $"v{AppSettings.ApiVersion}";

        private static readonly string description =
            @"<b>Blog</b>：<a target=""_blank"" href=""https://meowv.com"">https://meowv.com</a> <b>GitHub</b>：<a target=""_blank"" href=""https://github.com/aohost/Aohost.Blog"">https://github.com/aohost/Aohost.Blog</a> <b>Hangfire</b>：<a target=""_blank"" href=""/hangfire"">任务调度中心</a> <code>Powered by .NET Core 3.1 on Linux</code>";

        private static readonly List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>
        {
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v1,
                Name = "博客前台接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "Ahost - 博客前台接口",
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
                    Title = "Aohost - 博客后台接口",
                    Description = description
                }
            },
            new SwaggerApiInfo
            {
                UrlPrefix = Grouping.GroupName_v3,
                Name = "其他通用接口",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = version,
                    Title = "Aohost - 其他通用接口",
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
                    Title = "Aohost - JWT授权接口",
                    Description = description
                }
            }
        };



        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                ApiInfos.ForEach(x => { options.SwaggerDoc(x.UrlPrefix, x.OpenApiInfo); });

                var security = new OpenApiSecurityScheme
                {
                    Description = "JWT模式授权，请输入 Bearer {Token} 进行身份验证",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };
                options.AddSecurityDefinition("JWT", security);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {{security, new List<string>()}});

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

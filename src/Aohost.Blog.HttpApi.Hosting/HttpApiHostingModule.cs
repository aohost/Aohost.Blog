using System;
using System.Linq;
using Aohost.Blog.BackgroundJobs;
using Aohost.Blog.Domain.Configuration;
using Aohost.Blog.EntityFrameworkCore;
using Aohost.Blog.HttpApi.Hosting.Filters;
using Aohost.Blog.HttpApi.Hosting.Middleware;
using Aohost.Blog.Swagger;
using Aohost.Blog.ToolKits.Base;
using Aohost.Blog.ToolKits.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Aohost.Blog.HttpApi.Hosting
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(BlogHttpApiModule),
        typeof(BlogSwaggerModule),
        typeof(BlogFrameworkCoreModule)
        //,typeof(BlogBackgroundJobsModule)
    )]
    public class HttpApiHostingModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            #region 移除默认的异常处理Filter

            Configure<MvcOptions>(options =>
            {
                var filterMetadata = options.Filters.FirstOrDefault(x =>
                    x is ServiceFilterAttribute attribute && attribute.ServiceType == typeof(AbpExceptionFilter));
                
                // 移除 AbpExceptionFilter
                options.Filters.Remove(filterMetadata);

                // 添加自己实现的Filter
                options.Filters.Add(typeof(BlogExceptionFilter));
            });

            #endregion

            // 跨域
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // 身份验证
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(30),
                        ValidateIssuerSigningKey = true,
                        ValidAudience = AppSettings.JWT.Domain,
                        ValidIssuer = AppSettings.JWT.Domain,
                        IssuerSigningKey = new SymmetricSecurityKey(AppSettings.JWT.SecurityKey.GetBytes())
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async jwtContext =>
                        {
                            // 跳过默认的处理逻辑，返回下面的数据模型
                            jwtContext.HandleResponse();

                            jwtContext.Response.ContentType = "application/json;charset=utf-8";
                            jwtContext.Response.StatusCode = StatusCodes.Status200OK;

                            var result = new ServiceResult();
                            result.IsFailed("UnAuthorized");

                            await jwtContext.Response.WriteAsync(result.ToJson());
                        }
                    };
                });
            // 认证授权
            context.Services.AddAuthorization();

            // HTTP请求
            context.Services.AddHttpClient();

            // 路由配置
            context.Services.AddRouting(options =>
            {
                // 设置url为小写
                options.LowercaseUrls = true;
                // 在生成的url后面添加斜杠
                options.AppendTrailingSlash = true;
            });

        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            // 环境变量，开发环境
            if (env.IsDevelopment())
            {
                // 生成异常页面
                app.UseDeveloperExceptionPage();
            }


            // 使用HSTS，添加严格传输安全头
            app.UseHsts();

            // 转发将表头代理到当前请求，配合 nginx 使用，获取用户真实IP
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            // 路由
            app.UseRouting();

            // 跨域
            app.UseCors(x=> {
                x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            // 异常处理中间件
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // 身份认证
            app.UseAuthentication();

            // 认证授权
            app.UseAuthorization();

            // Http请求转Https
            app.UseHttpsRedirection();

            // 路由映射
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
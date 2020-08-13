using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Aohost.Blog.Swagger
{
    public static class BlogSwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "1.0.0",
                    Title = "My Api",
                    Description = "Api Desc"
                });

                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Aohost.Blog.HttpApi.xml"));
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Aohost.Blog.Domain.xml"));
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                //    "Aohost.Blog.Application.Contracts.xml"));
            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "Default Api");
            });
        }
    }
}

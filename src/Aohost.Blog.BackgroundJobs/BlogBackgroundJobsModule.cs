using Aohost.Blog.Domain.Configuration;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.MySql.Core;
using Hangfire.SQLite;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;

namespace Aohost.Blog.BackgroundJobs
{
    [DependsOn(typeof(AbpBackgroundJobsHangfireModule))]
    public class BlogBackgroundJobsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            context.Services.AddHangfire(config =>
            {
                switch (AppSettings.EnableDb)
                {
                    case "MySql":
                        config.UseStorage(new MySqlStorage(AppSettings.ConnectionStrings));
                        break;
                    case "SqlServer":
                        config.UseSqlServerStorage(AppSettings.ConnectionStrings);
                        break;
                    case "Sqlite":
                        config.UseSQLiteStorage(AppSettings.ConnectionStrings);
                        break;

                    default:
                        config.UseSqlServerStorage(AppSettings.ConnectionStrings);
                        break;
                }

            });
            //context.Services.AddHttpClient();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseHangfireServer();
            app.UseHangfireDashboard(options: new DashboardOptions
            {
                Authorization = new[]
                {
                    new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = false,
                        SslRedirect = false,
                        LoginCaseSensitive = true,
                        Users = new []
                        {
                            new BasicAuthAuthorizationUser
                            {
                                Login = AppSettings.HangFire.Login,
                                PasswordClear = AppSettings.HangFire.Password
                            }
                        }
                    }), 
                },
                DashboardTitle = "任务调度中心"
            });

            var service = context.ServiceProvider;

            service.UseWallpaperJob();
            service.UseHotNewsJob();
        }
    }
}
using System;
using Aohost.Blog.BackgroundJobs.Jobs.HangFire;
using Aohost.Blog.BackgroundJobs.Jobs.HotNews;
using Aohost.Blog.BackgroundJobs.Jobs.Wallpaper;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Aohost.Blog.BackgroundJobs
{
    public static class BlogBackgroundJobsExtensions
    {
        public static void UseHangFireTest(this IServiceProvider service)
        {
            var job = service.GetService<HangFireTestJob>();

            RecurringJob.AddOrUpdate("定时任务测试", () => job.ExecuteAsync(), CronType.Minute());
        }

        public static void UseWallpaperJob(this IServiceProvider service)
        {
            var job = service.GetService<WallpaperJob>();
            RecurringJob.AddOrUpdate("壁纸数据抓取", ()=>job.ExecuteAsync(), CronType.Hour(1, 3));
        }

        public static void UseHotNewsJob(this IServiceProvider service)
        {
            var job = service.GetRequiredService<HotNewsJob>();
            RecurringJob.AddOrUpdate("每日热点抓取", () => job.ExecuteAsync(), CronType.Hour(1, 2));
        }
    }
}
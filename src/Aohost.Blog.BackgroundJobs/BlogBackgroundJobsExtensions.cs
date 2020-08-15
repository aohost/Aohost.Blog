using System;
using Aohost.Blog.BackgroundJobs.Jobs.HangFire;
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
    }
}
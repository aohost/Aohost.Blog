using System;
using System.Threading.Tasks;

namespace Aohost.Blog.BackgroundJobs.Jobs.HangFire
{
    public class HangFireTestJob:IBackgroundJob
    {
        public async Task ExecuteAsync()
        {
            Console.WriteLine("定时任务测试");

            await Task.CompletedTask;
        }
    }
}
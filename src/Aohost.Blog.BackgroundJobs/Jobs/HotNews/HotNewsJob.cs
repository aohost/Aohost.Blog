using System.Net.Http;
using System.Threading.Tasks;
using Aohost.Blog.Domain.HotNews.Repository;

namespace Aohost.Blog.BackgroundJobs.Jobs.HotNews
{
    public class HotNewsJob:IBackgroundJob
    {
        private readonly IHotNewsRepository _hotNewsRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public HotNewsJob(IHotNewsRepository hotNewsRepository, IHttpClientFactory httpClientFactory)
        {
            _hotNewsRepository = hotNewsRepository;
            _httpClientFactory = httpClientFactory;
        }

        public Task ExecuteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
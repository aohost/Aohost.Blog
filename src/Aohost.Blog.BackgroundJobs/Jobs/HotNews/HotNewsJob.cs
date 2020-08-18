using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aohost.Blog.Application.Contracts.HotNews;
using Aohost.Blog.Domain.HotNews.Repository;
using Aohost.Blog.Domain.Shared.Enum;
using Aohost.Blog.ToolKits.Extensions;
using HtmlAgilityPack;

namespace Aohost.Blog.BackgroundJobs.Jobs.HotNews
{
    public class HotNewsJob : IBackgroundJob
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
            var hotNewsUrls = new List<HotNewsJobItem<string>>
            {
                new HotNewsJobItem<string> {Result = "https://www.cnblogs.com", Source = HotNewsEnum.CnBlogs},
                new HotNewsJobItem<string> {Result = "https://www.v2ex.com/?tab=hot", Source = HotNewsEnum.V2EX},
                new HotNewsJobItem<string>
                    {Result = "https://segmentfault.com/hottest", Source = HotNewsEnum.SegmentFault},
                new HotNewsJobItem<string> {Result = "https://web-api.juejin.im/query", Source = HotNewsEnum.JueJin},
                new HotNewsJobItem<string> {Result = "https://weixin.sogou.com", Source = HotNewsEnum.WeiXin},
                new HotNewsJobItem<string>
                    {Result = "https://www.douban.com/group/explore", Source = HotNewsEnum.DouBan},
                new HotNewsJobItem<string> {Result = "https://www.ithome.com", Source = HotNewsEnum.ITHome},
                new HotNewsJobItem<string> {Result = "https://36kr.com/newsflashes", Source = HotNewsEnum.KR36},
                new HotNewsJobItem<string>
                    {Result = "http://tieba.baidu.com/hottopic/browse/topicList", Source = HotNewsEnum.TieBa},
                new HotNewsJobItem<string> {Result = "http://top.baidu.com/buzz?b=341", Source = HotNewsEnum.BaiDu},
                new HotNewsJobItem<string>
                    {Result = "https://s.weibo.com/top/summary/summary", Source = HotNewsEnum.WeiBo},
                new HotNewsJobItem<string>
                {
                    Result = "https://www.zhihu.com/api/v3/feed/topstory/hot-lists/total?limit=50&desktop=true",
                    Source = HotNewsEnum.ZhiHu
                },
                new HotNewsJobItem<string> {Result = "https://daily.zhihu.com", Source = HotNewsEnum.ZhiHuDaily},
                new HotNewsJobItem<string>
                    {Result = "http://news.163.com/special/0001386F/rank_whole.html", Source = HotNewsEnum.News163},
                new HotNewsJobItem<string> {Result = "https://github.com/trending", Source = HotNewsEnum.GitHub},
                new HotNewsJobItem<string>
                {
                    Result = "https://www.iesdouyin.com/web/api/v2/hotsearch/billboard/word",
                    Source = HotNewsEnum.DouYinHot
                },
                new HotNewsJobItem<string>
                {
                    Result = "https://www.iesdouyin.com/web/api/v2/hotsearch/billboard/aweme",
                    Source = HotNewsEnum.DouYinVideo
                },
                new HotNewsJobItem<string>
                {
                    Result = "https://www.iesdouyin.com/web/api/v2/hotsearch/billboard/aweme/?type=positive",
                    Source = HotNewsEnum.DouYinPositive
                },
            };

            var web = new HtmlWeb();
            var listTask = new List<Task<HotNewsJobItem<object>>>();

            hotNewsUrls.ForEach(item =>
            {
                var task = Task.Run(async () =>
                {
                    var obj = new Object();

                    if (item.Source == HotNewsEnum.JueJin)
                    {
                        using var client = _httpClientFactory.CreateClient();
                        client.DefaultRequestHeaders.Add("User-Agent",
                            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.14 Safari/537.36 Edg/83.0.478.13");
                        client.DefaultRequestHeaders.Add("X-Agent", "Juejin/Web");
                        var data =
                            "{\"extensions\":{\"query\":{ \"id\":\"21207e9ddb1de777adeaca7a2fb38030\"}},\"operationName\":\"\",\"query\":\"\",\"variables\":{ \"first\":20,\"after\":\"\",\"order\":\"THREE_DAYS_HOTTEST\"}}";
                        var buffer = data.SerializeUtf8();
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var httpResponse = await client.PostAsync(item.Result, byteContent);
                        obj = await httpResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        obj = await web.LoadFromWebAsync(item.Result,
                            (item.Source == HotNewsEnum.BaiDu || item.Source == HotNewsEnum.News163)
                                ? Encoding.GetEncoding("GB2312")
                                : Encoding.UTF8);
                    }

                    return new HotNewsJobItem<object>
                    {
                        Result = obj,
                        Source = item.Source
                    };
                });
                listTask.Add(task);
            });

            Task.WaitAll(tasks: listTask.ToArray());
        }
    }
}
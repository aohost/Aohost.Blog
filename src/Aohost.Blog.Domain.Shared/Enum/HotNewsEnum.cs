using System.ComponentModel;

namespace Aohost.Blog.Domain.Shared.Enum
{
    public enum HotNewsEnum
    {
        [Description("博客园")]
        CnBlogs = 1,

        [Description("V2EX")]
        V2EX =2,

        [Description("SegmentFault")]
        SegmentFault = 3,

        [Description("掘金")]
        JueJin = 4,

        [Description("微信热门")]
        WeiXin = 5,

        [Description("豆瓣精选")]
        DouBan = 6,

        [Description("IT之家")]
        ITHome = 7,

        [Description("36氪")]
        KR36 = 8,

        [Description("贴吧")]
        TieBa = 9,

        [Description("百度热搜")]
        BaiDu = 10,

        [Description("微博热榜")]
        WeiBo = 11,

        [Description("知乎热榜")]
        ZhiHu = 12,

        [Description("知乎日报")]
        ZhiHuDaily = 13,

        [Description("网易新闻")]
        News163 = 14,

        [Description("GitHub")]
        GitHub = 15,

        [Description("抖音热点")]
        DouYinHot = 16,

        [Description("抖音视频")]
        DouYinVideo = 17,

        [Description("抖音正能量")]
        DouYinPositive = 18
    }
}
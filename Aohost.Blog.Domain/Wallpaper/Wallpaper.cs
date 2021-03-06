﻿using System;
using Volo.Abp.Domain.Entities;

namespace Aohost.Blog.Domain.Wallpaper
{
    public class Wallpaper:Entity<Guid>
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
﻿using System.Collections.Generic;

namespace Aohost.Blog.Application.Contracts.Blog.Post
{
    public class QueryPostForAdminDto:QueryPostDto
    {
        /// <summary>
        /// Posts
        /// </summary>
        public new IEnumerable<PostBriefForAdminDto> Posts { get; set; }
    }
}

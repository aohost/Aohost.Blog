using System.ComponentModel.DataAnnotations;

namespace Aohost.Blog.Application.Contracts
{
    /// <summary>
    /// 分页输入参数
    /// </summary>
    public class PagingInput
    {
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// 每页限制条数
        /// </summary>
        [Range(10, 30)]
        public int Limit { get; set; } = 10;
    }
}
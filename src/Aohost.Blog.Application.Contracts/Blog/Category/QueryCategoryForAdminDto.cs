using Volo.Abp.IdentityServer.Clients;

namespace Aohost.Blog.Application.Contracts.Blog.Category
{
    /// <summary>
    /// 后台类别管理对象
    /// </summary>
    public class QueryCategoryForAdminDto:QueryCategoryDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
    }
}
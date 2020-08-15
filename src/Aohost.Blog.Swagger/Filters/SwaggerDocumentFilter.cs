using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Aohost.Blog.Swagger.Filters
{
    public class SwaggerDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tags = new List<OpenApiTag> { };

            #region 实现添加自定义描述时，过滤不属于同一个分组的API

            var groupName = context.ApiDescriptions.FirstOrDefault()?.GroupName;

            var apis = context.ApiDescriptions.GetType()
                .GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.GetValue(context.ApiDescriptions) as IEnumerable<ApiDescription>;

            var controller = apis.Where(x => x.GroupName != groupName)
                .Select(x => ((ControllerActionDescriptor) x.ActionDescriptor).ControllerName).Distinct();

            swaggerDoc.Tags = tags.Where(x => !controller.Contains(x.Name)).OrderBy(x => x.Name).ToList();

            #endregion
        }
    }
}
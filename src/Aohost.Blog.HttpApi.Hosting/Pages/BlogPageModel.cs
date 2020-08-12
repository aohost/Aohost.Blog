using Aohost.Blog.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Aohost.Blog.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class BlogPageModel : AbpPageModel
    {
        protected BlogPageModel()
        {
            LocalizationResourceType = typeof(BlogResource);
        }
    }
}
using LazyCache;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace ProductWebApi.Attributes;

public class AddLazyCacheAttribute:ActionFilterAttribute
{
    private static IAppCache? _appcache;
    private static TimeSpan duration;
    private static string? key;


    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        duration = TimeSpan.FromSeconds(100);
        _appcache = context.HttpContext.RequestServices.GetRequiredService<IAppCache>();
        if (context.HttpContext.Request.Path == "/api/Product/Products")
            key = "Products";


        var cachedResult = await _appcache.GetOrAddAsync(key, () => next(), duration);

        if (cachedResult is not null)
            context.Result = cachedResult.Result;
    }

  
}

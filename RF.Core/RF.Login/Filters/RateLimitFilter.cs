using System;
using System.Web.Mvc;
using System.Runtime.Caching;

public class RateLimitFilter : ActionFilterAttribute
{
    private static readonly MemoryCache cache = MemoryCache.Default;

    // Maximum number of allowed requests in the configured time window
    public int MaxRequests { get; set; } = 1000;
    public int WindowSeconds { get; set; } = 60;

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        string user = filterContext.HttpContext.User.Identity.Name;
        if (string.IsNullOrEmpty(user))
            user = filterContext.HttpContext.Request.UserHostAddress;

        // Build a unique cache key to store request counts for this specific user
        string key = "req_" + user;

        int count = 0;

        if (cache.Contains(key))
            count = (int)cache.Get(key);

        // Increment the request counter for this user
        count++;

        CacheItemPolicy policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(WindowSeconds)
        };

        cache.Set(key, count, policy);

        // Block the request if the user exceeds the allowed request limit
        if (count > MaxRequests)
        {
            filterContext.HttpContext.Response.StatusCode = 429;
            filterContext.Result = new ContentResult
            {
                Content = "User blocked: too many requests."
            };
            return;
        }

        // Proceed with normal action execution if request limit not exceeded
        base.OnActionExecuting(filterContext);
    }
}

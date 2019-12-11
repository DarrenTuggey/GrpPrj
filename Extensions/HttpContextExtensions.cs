using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace GroupProject.Extensions
{
    // Extension required for the anti cross site request forgery service
    public static class HttpContextExtensions
    {
        public static string GetAntiForgeryTokenForJs(this HttpContext httpContext)
        {
            var antiForgeryService = (IAntiforgery)httpContext.RequestServices.GetService(typeof(IAntiforgery));
            var tokenSet = antiForgeryService.GetAndStoreTokens(httpContext);

            return tokenSet.RequestToken;
        }
    }
}

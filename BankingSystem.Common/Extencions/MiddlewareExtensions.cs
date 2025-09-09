using BankingSystem.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace BankingSystem.Common.Extencions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}

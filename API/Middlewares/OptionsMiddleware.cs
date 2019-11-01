using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class OptionsMiddleware
    {
        //  Variables
        //  =========

        private readonly RequestDelegate _next;

        //  Constructors
        //  ============

        public OptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //  Methods
        //  =======

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Method != "OPTIONS")
            {
                return _next.Invoke(context);
            }

            return BeginInvoke(context);
        }

        private Task BeginInvoke(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { (string)context.Request.Headers["Origin"] });
                context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
                context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
                context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                context.Response.StatusCode = 200;
                return context.Response.WriteAsync("OK");
            }

            return _next.Invoke(context);
        }
    }

    public static class OptionsMiddlewareExtensions
    {
        //  Methods
        //  =======

        public static IApplicationBuilder UseOptions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OptionsMiddleware>();
        }
    }
}
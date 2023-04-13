namespace Tasks.Middlewares;
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseLogTimeMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LogTimeMiddleware>();
    }
}


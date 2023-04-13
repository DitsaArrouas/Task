using Tasks.Services;
using System.Diagnostics;

namespace Tasks.Middlewares;

public class LogTimeMiddleware 
{
    private readonly ILogService logger;
    private readonly RequestDelegate next;
     public LogTimeMiddleware (RequestDelegate next, ILogService logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async System.Threading.Tasks.Task InvokeAsync(HttpContext ctx)
    {
        var sw = new Stopwatch();
        sw.Start();
        await next(ctx);
        sw.Stop();
        logger.Log(LogLevel.Debug, 
            $"time took to {ctx.Request.Method} request {ctx.Request.Path} : {sw.ElapsedMilliseconds}");
    }
}
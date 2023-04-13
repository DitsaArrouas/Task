using Tasks.Interfaces;
using Tasks.Controllers;
using Tasks.Services;


namespace Tasks.Utilities
{
public static class Helper
{
    public static void AddTasks(this IServiceCollection services)
    {
        services.AddSingleton<ITaskHttp, TaskService>();
        services.AddSingleton<IUserHttp, UserService>();
        services.AddTransient<ILogService, LogService>();
    }
}
}
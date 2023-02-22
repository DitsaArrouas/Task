using Tasks.Interfaces;
using Tasks.Controllers;

namespace Tasks.Utilities
{
public static class Helper
{
    public static void AddTasks(this IServiceCollection services)
    {
        services.AddSingleton<ITaskHttp, TaskService>();
    }
}
}
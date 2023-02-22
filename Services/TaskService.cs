using Tasks.Interfaces;
using Tasks;
using System.Linq;

namespace Tasks.Controllers
{   
    public class TaskService : ITaskHttp
    {    
        private List<Task> tasks = new List<Task>
        {
            new Task { Name="homeWork",Id=1, Done = false},
            new Task { Name="cleanWindows",Id=2, Done = true},
            new Task { Name="shopping",Id=3, Done = false},
            new Task { Name="washDishes",Id=4, Done = true}
        };

        public List<Task> GetAll() => tasks;
        public Task Get(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void Add(Task task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
        }

        public bool Update(int id, Task newTask)
        {
            if (newTask.Id != id)
                return false;
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            task.Name = newTask.Name;
            task.Done = newTask.Done;
            return true;
        }

        public bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            return true;
        }
    }
}
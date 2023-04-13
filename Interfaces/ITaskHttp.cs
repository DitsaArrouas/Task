
namespace Tasks.Interfaces
{
    public interface ITaskHttp
    {
        public List<Task> GetAll(string token);
        //public Task Get(int id, string token);
        public void Add(Task task, string token);
        public bool Update(int id, Task newTask, string token);
        public bool Delete(int id, string token);
    }
}
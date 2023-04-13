
namespace Tasks.Interfaces
{
    public interface IUserHttp
    {
        public List<User> GetAll();
        public User Get(string id);
        public void Add(User task);
        public bool Update(string id, User newTask);
        public bool Delete(string id);
    }
}
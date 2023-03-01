using Tasks.Interfaces;
using Tasks;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;


namespace Tasks.Controllers
{   
    public class TaskService : ITaskHttp
    {    
        List<Task> tasks {get;}
        private IWebHostEnvironment  webHost;
        private string filePath;
        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "Tasks.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                tasks = JsonSerializer.Deserialize<List<Task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(tasks));
        }
        public List<Task> GetAll() => tasks;
        public Task Get(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void Add(Task task)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            tasks.Add(task);
            saveToFile();
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
            saveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;
            tasks.Remove(task);
            saveToFile();
            return true;
        }
    }
}
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
using Tasks.Controllers;
using System.IdentityModel.Tokens.Jwt;


namespace Tasks.Services
{   
    public class TaskService : ITaskHttp
    {    
        List<Task>? tasks {get;}
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
        public string GetIdToken(string idtoken)
        {
            var token = new JwtSecurityToken(jwtEncodedString: idtoken.Split(" ")[1]);
            string id = token.Claims.First(c => c.Type == "id").Value;
            return id;
        }
        public List<Task> GetAll(string token)
        {
            string Id = GetIdToken(token);
            System.Console.WriteLine(Id);
            return tasks.FindAll(t => t.UserId.CompareTo(Id) == 0).ToList();
        }
        // public Task? Get(int id, string token)
        // {
        //     return tasks.FirstOrDefault(t => t.Id == id);
        // }

        public void Add(Task task, string token)
        {
            task.Id = tasks.Max(t => t.Id) + 1;
            task.UserId = GetIdToken(token);
            tasks.Add(task);
            saveToFile();
        }

        public bool Update(int id, Task newTask, string token)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (newTask.Id != id || GetIdToken(token)!= task?.UserId || task == null)
                return false;
            task.Name = newTask.Name;
            task.Done = newTask.Done;
            saveToFile();
            return true;
        }

        public bool Delete(int id, string token)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null || GetIdToken(token)!= task?.UserId)
                return false;
            tasks.Remove(task);
            saveToFile();
            return true;
        }
    }
}
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

namespace Tasks.Services
{   
    public class UserService : IUserHttp
    {    
        public List<User>? users {get;}
        private IWebHostEnvironment  webHost;
        private string filePath;
        public static string token = string.Empty;
        public UserService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "Users.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(users));
        }
        public List<User>? GetAll() => users;
        public User? Get(string id)
        {
            return users?.FirstOrDefault(u => u.Id.CompareTo(id) == 0);
        }

        public void Add(User user)
        {
            users?.Add(user);
            saveToFile();
        }

        public bool Update(string id, User newUser)
        {
            if (newUser.Id.CompareTo(id) != 0)
                return false;
            var user = users?.FirstOrDefault(u => u.Id.CompareTo(id) == 0);
            if (user == null)
                return false;
            user.Name = newUser.Name;
            user.Password = newUser.Password;
            saveToFile();
            return true;
        }

        public bool Delete(string id)
        {
            var user = users?.FirstOrDefault(u => u.Id.CompareTo(id) == 0);
            if (user == null)
                return false;
            users?.Remove(user);
            saveToFile();
            return true;
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, User newTask)
        {
            throw new NotImplementedException();
        }
    }
}
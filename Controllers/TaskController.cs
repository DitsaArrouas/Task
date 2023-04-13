using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private ITaskHttp TaskHttp;
        public TaskController(ITaskHttp TaskHttp)
        {
            this.TaskHttp = TaskHttp;
        }

        [HttpGet]
        [Authorize(Policy = "Agent")]
        public IEnumerable<Task> Get()
        {
            System.Console.WriteLine("get");
            return TaskHttp.GetAll(Request.Headers.Authorization);
        }

        [HttpGet("{id}")]
        // public ActionResult<Task> Get(int id)
        // {
        //     //var p = TaskHttp.Get(id, Request.Headers.Authorization);
        //     if (p == null)
        //         return NotFound();
        //      return p;
        // }

        [HttpPost]
        public ActionResult Post(Task task)
        {
            TaskHttp.Add(task, Request.Headers.Authorization);

            return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Task task)
        {
            if (! TaskHttp.Update(id, task, Request.Headers.Authorization))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            if (! TaskHttp.Delete(id, Request.Headers.Authorization))
                return NotFound();
            return NoContent();            
        }

    }
}

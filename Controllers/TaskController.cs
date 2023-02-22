using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tasks.Interfaces;

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
        public IEnumerable<Task> Get()
        {
            return TaskHttp.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(int id)
        {
            var p = TaskHttp.Get(id);
            if (p == null)
                return NotFound();
             return p;
        }

        [HttpPost]
        public ActionResult Post(Task task)
        {
            TaskHttp.Add(task);

            return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Task task)
        {
            if (! TaskHttp.Update(id, task))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            if (! TaskHttp.Delete(id))
                return NotFound();
            return NoContent();            
        }

    }
}

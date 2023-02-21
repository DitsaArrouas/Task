using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            return TaskService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(int id)
        {
            var p = TaskService.Get(id);
            if (p == null)
                return NotFound();
             return p;
        }

        [HttpPost]
        public ActionResult Post(Task task)
        {
            TaskService.Add(task);

            return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Task task)
        {
            if (! TaskService.Update(id, task))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            if (! TaskService.Delete(id))
                return NotFound();
            return NoContent();            
        }

    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NMTask.Models;
using NMTask.Services;

namespace NMTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        public TaskController()
        {

        }

        [HttpGet]
        public ActionResult<List<Task>> GetAll(
            [FromQuery] int offset = 0,
            [FromQuery] int count = 100
        ) => TaskService.GetAll(offset, count);

        [HttpGet("{id}")]
        public ActionResult<Task> Get(int id)
        {
            Task task = TaskService.Get(id);

            if (task is null)
                return NotFound();

            return task;
        }

        [HttpPost]
        public IActionResult Create(Task task)
        {
            TaskService.Add(task);
            return CreatedAtAction(nameof(Create), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Task task)
        {
            if (id != task.Id)
                return BadRequest();

            Task existingTask = TaskService.Get(id);
            if (existingTask is null)
                return NotFound();

            TaskService.Update(task);

            return CreatedAtAction(nameof(Update), task);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Task task = TaskService.Get(id);

            if (task is null)
                return NotFound();

            TaskService.Delete(id);

            return NoContent();
        }
    }
}
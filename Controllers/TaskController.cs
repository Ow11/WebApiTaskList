using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTaskList.Data;
using WebApiTaskList.Models;
using WebApiTaskList.Services;

namespace WebApiTaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<List<TaskModel>> GetAll(
            [FromQuery] int offset = 0,
            [FromQuery] int count = 100
        ) => _taskService.GetAll(offset, count);

        [HttpGet("{id}")]
        public ActionResult<TaskModel> Get(int id)
        {
            TaskModel task = _taskService.Get(id);

            if (task is null)
                return NotFound();

            return task;
        }

        [HttpPost("List/{listId}")]
        public IActionResult Create(int listId, TaskMinified taskMini)
        {
            if (taskMini.Name == "")
                return BadRequest();

            TaskModel task = new()
            {
                Name = taskMini.Name,
                Description = taskMini.Description
            };

            TaskModel response = _taskService.Add(listId, task);

            if (response is null)
                return BadRequest();

            return CreatedAtAction(nameof(Create), new { id = task.TaskModelId }, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskModel task)
        {
            if (id != task.TaskModelId)
                return BadRequest();

            TaskModel existingTask = _taskService.Update(task);
            if (existingTask is null)
                return NotFound();

            return CreatedAtAction(nameof(Update), task);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TaskModel task = _taskService.Get(id);

            if (task is null)
                return NotFound();

            _taskService.Delete(id);

            return NoContent();
        }
    }
}

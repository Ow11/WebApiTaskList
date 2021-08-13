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
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;
        private readonly ITaskService _taskService;

        public ListController(IListService listService, ITaskService taskService)
        {
            _listService = listService;
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<List<ListDisplayInAll>> GetAll(
            [FromQuery] int offset = 0,
            [FromQuery] int count = 20
        )
        {
            List<ListDisplayInAll> result  = new List<ListDisplayInAll>();

            foreach(var list in _listService.GetAll(offset, count))
            {
                var listDisplayInAll = new ListDisplayInAll
                {
                    Id = list.ListModelId,
                    Name = list.Name,
                    Description = list.Description,
                    UpdatedAt = list.UpdatedAt,
                };

                int tasks = 0;

                if (!(list.TaskModels is null))
                    tasks = list.TaskModels.Count;

                listDisplayInAll.Tasks = tasks;

                result.Add(
                    listDisplayInAll
                );
            }

            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<ListModel> Get(int id)
        {
            var list = _listService.Get(id);

            if (list is null)
                return NotFound();

            return list;
        }

        [HttpPost]
        public IActionResult Create(ListMinified list)
        {
            ListModel result = new ListModel
            {
                Name = list.Name,
                Description = list.Description,
            };

            _listService.Add(result);
            return CreatedAtAction(nameof(Create), new { id = result.ListModelId }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ListModel list)
        {
            if (id != list.ListModelId)
                return BadRequest();

            ListModel existingList = _listService.Get(id);
            if (existingList is null)
                return NotFound();

            _listService.Update(list);

            return CreatedAtAction(nameof(Update), list);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ListModel list = _listService.Get(id);

            if (list is null)
                return NotFound();

            _listService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}/merge/{mergeId}")]
        public IActionResult Merge(int id, int mergeId)
        {
            if (id == mergeId)
                return BadRequest();

            if (_listService.Get(id) is null || _listService.Get(mergeId) is null)
                return NotFound();

            ListModel list = _listService.Merge(id, mergeId);

            return Ok(list);
        }

    }
}

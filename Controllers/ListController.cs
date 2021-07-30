using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NMList.Models;
using NMList.Services;

namespace NMList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase
    {
        public ListController() { }

        [HttpGet]
        public ActionResult<List<LisT>> GetAll() =>
            ListService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<ListDetailed> Get(int id)
        {
            ListDetailed list = ListService.Get(id);

            if (list is null)
                return NotFound();

            return list;
        }

        [HttpPost]
        public IActionResult Create(LisT list)
        {
            ListService.Add(list);
            return CreatedAtAction(nameof(Create), new { id = list.Id }, list);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, LisT list)
        {
            if (id != list.Id)
                return BadRequest();

            LisT existingList = ListService.GetLisT(id);
            if (existingList is null)
                return NotFound();

            ListService.Update(list);

            return CreatedAtAction(nameof(Update), list);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            LisT list = ListService.GetLisT(id);

            if (list is null)
                return NotFound();

            ListService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}/merge/{mergeId}")]
        public IActionResult Merge(int id, int mergeId)
        {
            if (id == mergeId)
                return BadRequest();

            if (ListService.GetLisT(id) is null || ListService.GetLisT(mergeId) is null)
                return NotFound();

            LisT list = ListService.Merge(id, mergeId);

            return Ok(list);
            // return CreatedAtAction(nameof(Update), list);
        }
    }
}
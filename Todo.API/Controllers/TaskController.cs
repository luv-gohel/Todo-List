using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.BAL.Interface;
using Todo.Models.DTO;
using Todo.Models.RequestDTO;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public readonly IManageNoteInterface _manageNoteInterface;
        public TaskController(IManageNoteInterface manageNoteInterface)
        {
            _manageNoteInterface = manageNoteInterface;
        }
        [Route("AddNote")]
        [Route("UpdateNote")]
        [HttpPost]
        public IActionResult AddTask(ListRequest l1)
        {

            var result = _manageNoteInterface.AddUpdateTask(l1);
            if (result.statuscode == 100)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [Route("DeleteNote")]
        [HttpDelete]
        public IActionResult DeleteTask(ListRequest l1)
        {
            var result = _manageNoteInterface.DeleteTask(l1);
            if (result.statuscode == 102)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Route("GetNotes")]
        [HttpGet]
        public IActionResult GetAllTask([FromQuery]requestUserNotes r1)
        {
            var result = _manageNoteInterface.GetAllTask(r1);
            //if (result.statuscode == 100)
            //{
            //    return BadRequest(result);
            //}
            return Ok(result);
        }
    }
}

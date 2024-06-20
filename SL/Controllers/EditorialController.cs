using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialController : ControllerBase
    {
        [HttpGet]
        [Route("Editorial/GetAll")]
        public IActionResult GetAll()
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Editorial.GetAll();
            result.Success = task.Success;
            result.Message = task.Message;

            if(task.Success)
            {
                result.Data = task.editorials;
                return Ok (result);
            }
            else
            {
                result.Error = task.Error;
                return BadRequest (result);
            }
        }
    }
}

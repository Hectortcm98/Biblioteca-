using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {

        [HttpGet]
        [Route("Libro/GetAll")]
        public IActionResult GetAll()
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Libro.GetAll();
            result.Success = task.Success;
            result.Message = task.Message;

            if(task.Success)
            {
                result.Data = task.libros;
                return Ok(result);
            }else
            {
                result.Error = task.Error;
                return BadRequest(result);
            }
        }
        [HttpGet]
        [Route("Libro/GetById/{idLibro}")]
        public IActionResult GetById(int idLibro)
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Libro.GetById(idLibro);
            result.Success = task.Success;
            result.Message = task.Message;

            if (task.Success)
            {
                result.Data = task.Libro;
                return Ok(result);
            }
            else
            {
                result.Error = task.Error;
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("Libro/Add")]
        public IActionResult Add([FromBody] ML.Libro libro)
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Libro.Add(libro);
            result.Success = task.Success;
            result.Message = task.Message;

            if (task.Success)
            {
               
                return Ok(result);
            }
            else
            {
                result.Error = task.Error;
                return BadRequest(result);
            }
        }

        [HttpPut]
        [Route("Libro/Update")]
        public IActionResult Update(ML.Libro libro)
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Libro.Update(libro);
            result.Success = task.Success;
            result.Message = task.Message;

            if (task.Success)
            {
               
                return Ok(result);
            }
            else
            {
                result.Error = task.Error;
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("Libro/Delete/{idLibro}")]
        public IActionResult Delete( int idLibro)
        {
            DTO.Result result = new DTO.Result();
            var task = BL.Libro.Delete(idLibro);
            result.Success = task.Success;
            result.Message = task.Message;

            if (task.Success)
            {
              
                return Ok(result);
            }
            else
            {
                result.Error = task.Error;
                return BadRequest(result);
            }
        }
    }
}

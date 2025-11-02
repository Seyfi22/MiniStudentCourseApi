using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStudentCourseApi.DTOs;
using MiniStudentCourseApi.Services.Interfaces;

namespace MiniStudentCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IGenericService<CourseDto> _courseService;

        public CourseController(IGenericService<CourseDto> courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_courseService.GetAll());     
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_courseService.GetById(id));
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured", details = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedCourse = _courseService.Add(courseDto);
                return CreatedAtAction(nameof(GetById), new { id = addedCourse.Id }, addedCourse);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while adding the course", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_courseService.Update(id, courseDto));
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while updating the course", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _courseService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while deleting the course", details = ex.Message });
            }
        }
    }
}

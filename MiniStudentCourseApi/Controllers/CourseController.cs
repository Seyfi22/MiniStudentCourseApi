using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStudentCourseApi.DTOs.Course;
using MiniStudentCourseApi.Services.Implementations;
using MiniStudentCourseApi.Services.Interfaces;

namespace MiniStudentCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
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

        [HttpPost("add-with-students")]
        public IActionResult AddWithStudents([FromBody] CreateCourseDto createCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedCourse = _courseService.AddWithStudents(createCourseDto);
                return CreatedAtAction(nameof(GetById), new { id = addedCourse.Id }, addedCourse);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while adding the course", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, [FromBody] UpdateCourseDto updateCourseDto)               
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(updateCourseDto.Name))
            {
                if(_courseService.IsCourseNameRegisteredByAnotherAccount(id, updateCourseDto.Name))
                {
                    ModelState.AddModelError("Name", "This course name already exists");
                    return BadRequest(ModelState);
                }
            }

            try
            {
                return Ok(_courseService.UpdateCourse(id, updateCourseDto));
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

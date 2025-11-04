using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStudentCourseApi.DTOs.Student;
using MiniStudentCourseApi.Services.Implementations;
using MiniStudentCourseApi.Services.Interfaces;

namespace MiniStudentCourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_studentService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_studentService.GetById(id));
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

        [HttpPost("add-with-courses")]
        public IActionResult AddWithCourses([FromBody] CreateStudentDto createStudentDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedStudent = _studentService.AddWithCourses(createStudentDto);
                return CreatedAtAction(nameof(GetById), new { id = addedStudent.Id }, addedStudent);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while adding the student", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(_studentService.UpdateStudent(id, updateStudentDto));
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while updating the student", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _studentService.Delete(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occured while deleting the student", details = ex.Message });
            }
        }
    }
}

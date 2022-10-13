using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using JAP_Management.Core.Helpers;
using JAP_Management.Core.Models;
using JAP_Management.Services.Services.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace JAP_Management.Backoffice.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        #region GetStudents
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<StudentModel>>> GetStudents([FromBody] StudentSearchRequestModel studentRequestModel)
        {
            try
            {
                var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var list = await _studentService.GetStudentsAsync(studentRequestModel, userId);

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }
        #endregion

        #region AddStudent
        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<ActionResult<StudentUpsertRequest>> AddStudent([FromBody] StudentUpsertRequest studentModel)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var addedStudent = await _studentService.AddStudentAsync(studentModel);

                if (addedStudent == null)
                    return BadRequest("Student is not added!");

                return Ok(addedStudent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region CommentStudent
        [Authorize(Roles = "Admin")]
        [HttpPost("comment/{id}")]
        public async Task<ActionResult<StudentModel>> CommentStudentAsync([FromRoute] string id, [FromBody] StudentModel model)
        {
            try
            {
                var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var commentedStudent = await _studentService.CommentStudentAsync(id, userId, model.CommentByUser);

                if (commentedStudent == null)
                    return BadRequest("Comment is not added!");

                return Ok(commentedStudent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region GetStudentById
        [Authorize(Roles = "Admin,Student")]
        [HttpGet("{studentId}")]
        public async Task<ActionResult<StudentModel>> GetStudentById([FromRoute] string studentId)
        {
            try
            {
                var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var student = await _studentService.GetStudentById(studentId, userId);

                if (student == null)
                    return NotFound();

                return Ok(student);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region GetStudentByUpsertId
        [Authorize(Roles = "Admin")]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<StudentUpsertRequest>> GetStudentByUpsertId([FromRoute] string id)
        {
            try
            {
                var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var student = await _studentService.GetStudentByUpsertId(id, userId);

                if (student == null)
                    return NotFound();

                return Ok(student);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region UpdateStudent
        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<StudentUpsertRequest>> UpdateStudent([FromRoute] string id, [FromBody] StudentUpsertRequest studentModel)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var list = await _studentService.UpdateStudentAsync(id, studentModel);

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region DeleteStudent
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<StudentModel>> DeleteStudent([FromRoute] string id)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var deletedStudent = await _studentService.DeleteStudentAsync(id);

                return Ok(deletedStudent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion
    }
}
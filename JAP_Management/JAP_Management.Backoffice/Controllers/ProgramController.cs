using JAP_Management.Core.Helpers;
using JAP_Management.Core.Models;
using JAP_Management.Services.Services.Program;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace JAP_Management.Backoffice.Controllers
{
    [Authorize(Roles ="Admin")]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : Controller
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        #region GetPrograms
        [HttpPost]
        public async Task<ActionResult<List<ProgramModel>>> GetPrograms([FromBody] ProgramSearchRequestModel programRequestModel)
        {
            try
            { 
                var list = await _programService.GetProgramsAsync(programRequestModel);

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region AddProgram

        [HttpPost("add")]
        public async Task<ActionResult<ProgramModel>> AddProgram([FromBody] ProgramModel programModels)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var addedProgram = await _programService.AddProgramAsync(programModels);

                if (addedProgram == null)
                    return BadRequest("Program is not added!");

                return Ok(addedProgram);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region GetProgramById

        [HttpGet("{programId:int}")]
        public async Task<ActionResult<ProgramModel>> GetProgramById([FromRoute] int programId)
        {
            try
            {
                var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var program = await _programService.GetProgramById(programId, userId);

                if (program == null)
                    return NotFound();

                return Ok(program);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region UpdateProgram

        [HttpPut("update/{programId:int}")]
        public async Task<ActionResult<ProgramModel>> UpdateProgram([FromRoute] int programId, [FromBody] ProgramModel programModel)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var list = await _programService.UpdateProgramAsync(programId, programModel);

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        //#region DeleteProgram

        //[HttpDelete]
        //public async Task<ActionResult<ProgramModel>> DeleteProgram(int id)
        //{
        //    try
        //    {
        //        //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

        //        var deletedProgram = await _programService.DeleteProgramAsync(id);

        //        return Ok(deletedProgram);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error ->", ex);
        //        return StatusCode(500, "Something went wrong!");
        //    }
        //}

        //#endregion
    }
}

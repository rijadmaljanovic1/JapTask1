using JAP_Management.Core.Helpers;
using JAP_Management.Core.Models;
using JAP_Management.Services.Services.Selection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace JAP_Management.Backoffice.Controllers
{
    [Authorize(Roles = "Admin")]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class SelectionController : Controller
    {
        private readonly ISelectionServicee _selectionService;

        public SelectionController(ISelectionServicee selectionService)
        {
            _selectionService = selectionService;
        }

        #region GetSelections

        [HttpPost]
        public async Task<ActionResult<List<SelectionModel>>> GetSelections([FromBody] SelectionSearchRequestModel selectionRequestModel)
        {
            try
            {
                var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var list = await _selectionService.GetSelectionsAsync(selectionRequestModel);

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }
        #endregion

        #region AddSelection

        [HttpPost("add")]
        public async Task<ActionResult<SelectionUpsertRequest>> AddSelection([FromBody] SelectionUpsertRequest selectionModels)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var addedSelection = await _selectionService.AddSelectionAsync(selectionModels);

                if (addedSelection == null)
                    return BadRequest("Selection is not added!");

                return Ok(addedSelection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region GetSelectionById

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SelectionModel>> GetSelectionById([FromRoute] int id)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var selection = await _selectionService.GetSelectionById(id);

                if (selection == null)
                    return NotFound();

                return Ok(selection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region GetSelectionByUpsertId

        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<SelectionUpsertRequest>> GetSelectionByUpsertId([FromRoute] int id)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var selection = await _selectionService.GetSelectionByUpsertId(id);

                if (selection == null)
                    return NotFound();

                return Ok(selection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region UpdateSelection

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<SelectionUpsertRequest>> UpdateSelection([FromRoute] int id, [FromBody] SelectionUpsertRequest selectionModel)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var list = await _selectionService.UpdateSelectionAsync(id, selectionModel);

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region DeleteSelection

        [HttpDelete]
        public async Task<ActionResult<SelectionModel>> DeleteSelection(int id)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var deletedSelection = await _selectionService.DeleteSelectionAsync(id);

                return Ok(deletedSelection);
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

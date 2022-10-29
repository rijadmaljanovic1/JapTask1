using JAP_Management.Core.Helpers;
using JAP_Management.Core.Models;
using JAP_Management.Services.Services.ProgramItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace JAP_Management.Backoffice.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProgramItemController : Controller
    {
        private readonly IProgramItemService _programItemService;
        public ProgramItemController(IProgramItemService programItemService)
        {
            _programItemService = programItemService;
        }

        [HttpGet]
        public ActionResult<List<ItemsModel>> GetRanks()
        {
            try
            {
                var list = _programItemService.GetItems();

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #region GetItems
        [HttpPost]
        public async Task<ActionResult<List<ItemsModel>>> GetItems([FromBody] ProgramItemSearchRequestModel programItemRequestModel)
        {
            try
            {
                var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var list = await _programItemService.GetItemsAsync(programItemRequestModel, userId);

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }
        #endregion

        #region AddItem
        [HttpPost("add")]
        public async Task<ActionResult<ItemsModel>> AddItem([FromBody] ItemsModel itemsModel)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var addedItem = await _programItemService.AddItemAsync(itemsModel);

                if (addedItem == null)
                    return BadRequest("Student is not added!");

                return Ok(addedItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region GetItemById
        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemsModel>> GetItemById([FromRoute] int itemId)
        {
            try
            {
                var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var item = await _programItemService.GetItemById(itemId, userId);

                if (item == null)
                    return NotFound();

                return Ok(item);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region UpdateItem
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ItemsModel>> UpdateItem([FromRoute] int id, [FromBody] ItemsModel itemModel)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                var list = await _programItemService.UpdateItemAsync(id, itemModel);

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }

        #endregion

        #region DeleteItem
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<StudentModel>> DeleteItem([FromRoute] int id)
        {
            try
            {
                //var userId = JwtHelper.GetUserIdFromToken(HttpContext.User);

                //var deletedStudent = await _programItemService.DeleteStudentAsync(id);

                return Ok();
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

using JAP_Management.Core.Helpers;
using JAP_Management.Core.Models;
using JAP_Management.Services.Services.Ranks;
using JAP_Management.Services.Services.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JAP_Management.Backoffice.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class RankController : Controller
    {
        private readonly IRankService _rankService;
        public RankController(IRankService rankService)
        {
            _rankService = rankService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<RankModel>> GetRanks()
        {
            try
            {
                var list =  _rankService.GetRanks();

                return Ok(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->", ex);
                return StatusCode(500, "Something went wrong!");
            }
        }
    }
}

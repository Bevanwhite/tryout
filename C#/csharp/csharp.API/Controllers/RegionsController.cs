using csharp.API.Data;
using csharp.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace csharp.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RegionsController : ControllerBase
	{
		private readonly WalksDbContext dbContext;

		public RegionsController(WalksDbContext dbContext)
        {
			this.dbContext = dbContext;				
        }
        // Get ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
		public IActionResult GetAll()
		{
			var regions = dbContext.Regions.ToList();
			return Ok(regions);
		}
	}
}

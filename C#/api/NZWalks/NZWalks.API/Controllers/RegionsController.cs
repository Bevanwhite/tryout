using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;
		public RegionsController(NZWalksDbContext dbContext)
		{
			this.dbContext = dbContext;
		}


		// Get All Regions
		// Get: https://localhost:portnumber/api/regions
		[HttpGet]
		public IActionResult GetAll()
		{
			//var regions = new List<Region>
			//{
			//	new Region
			//	{
			//		Id = Guid.NewGuid(),
			//		Name = "Auckland Region",
			//		Code = "AKL",
			//		RegionImageUrl = "https://unsplash.com/photos/a-boardwalk-leading-to-a-steaming-geyser-at-sunset-QgiYyDUcWi0"
			//	},
			//	new Region
			//	{
			//		Id = Guid.NewGuid(),
			//		Name = "Wellington Region",
			//		Code = "WLG",
			//		RegionImageUrl = "https://unsplash.com/photos/a-bride-and-groom-holding-a-bouquet-of-flowers-3QupYkJJEAI"
			//	}
			//};
			// get data from Database - Domain models
			var regions = dbContext.Regions.ToList();

			// Map Domain Models to DTOs
			var regionsDto = new List<RegionDto>();
			foreach (var region in regions)
			{
				regionsDto.Add(new RegionDto()
				{
					Id = region.Id,
					Code = region.Code,
					Name = region.Name,
					RegionImageUrl = region.RegionImageUrl
				});
			}

			// return DTOs
			return Ok(regionsDto);
		}

		// Get Single Region (Get Region by Id)
		// Get: https://localhost:portnumber/api/regions/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		public IActionResult GetById([FromRoute] Guid id)
		{
			// var region = dbContext.Regions.Find(id);
			// another way to get the Region by Id
			var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

			if (region == null)
			{
				return NotFound();
			}

			var regionDto = new RegionDto
			{
				Id = region.Id,
				Code = region.Code,
				Name = region.Name,
				RegionImageUrl = region.RegionImageUrl
			};

			// return the DTO
			return Ok(regionDto);
		}


	}
}

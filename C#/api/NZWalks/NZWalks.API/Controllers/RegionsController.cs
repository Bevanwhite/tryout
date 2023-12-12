using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using Swashbuckle.AspNetCore.Annotations;

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
		// documented Swagger documented
		[SwaggerResponse(200, "Resource found", typeof(RegionDto))]
		[SwaggerResponse(404, "Resource not found")]
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

		// Post to Create New Region
		// Post: https://localhost:portnumber/api/regions
		[HttpPost]
		// documented Swagger documented
		[SwaggerResponse(201, "Resource created successfully", typeof(RegionDto))]
		[SwaggerResponse(400, "One or more validation errors occurred.")]
		public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			// Map or convert DTO to Domain Model
			var regionDomainModel = new Region
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};

			// saving regionModel in the database
			dbContext.Regions.Add(regionDomainModel);
			dbContext.SaveChanges();

			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
		}

		// Update region
		// Put: https://localhost:portnumber/api/regions/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		[SwaggerResponse(404, "Resource not found")]
		[SwaggerResponse(200, "Resource found", typeof(RegionDto))]
		public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Map DTO to Domain model
			regionDomainModel.Code = updateRegionRequestDto.Code;
			regionDomainModel.Name = updateRegionRequestDto.Name;
			regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

			dbContext.SaveChanges();

			// Convert Domain Model to DTO
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = updateRegionRequestDto.Name,
				RegionImageUrl = updateRegionRequestDto.RegionImageUrl
			};

			return Ok(regionDto);
		}

		// Delete Region
		// Delete: https://localhost:portnumber/api/regions/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		[SwaggerResponse(404, "Resource not found")]
		[SwaggerResponse(200, "Resource found", typeof(RegionDto))]
		public IActionResult Delete([FromRoute] Guid id)
		{
			var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Delete the region
			dbContext.Regions.Remove(regionDomainModel);
			dbContext.SaveChanges();

			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return Ok(regionDto);
		}
	}
}

using csharp.API.Data;
using csharp.API.Models.Domain;
using csharp.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public async Task<IActionResult> GetAll()
		{
			// Get Data From Database - Domain models
			var regionsDomain = await dbContext.Regions.ToListAsync();

			// Map Domain Models to DTOs
			var regionsDto = new List<RegionDto>();
			foreach (var regionDomain in regionsDomain)
			{
				regionsDto.Add(new RegionDto()
				{ 
					Id = regionDomain.Id,
					Code = regionDomain.Code,
					Name = regionDomain.Name,
					RegionImageUrl = regionDomain.RegionImageUrl
				});
			};

			// Return DTOs
			return Ok(regionsDto);
		}

		// GET SINGLE REGION(Get Region By ID)
		// GET: https://localhost:portnumber/api/regions/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id) 
		{
			// var region = dbContext.Regions.Find(id); // use in the primary colum
			// Get Region Domain Model from Database
			var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);

			if(regionDomain == null)
			{
				return NotFound();
			}

			// Map/Convert Region Domain Model to Region DTO
			var regionDto = new RegionDto
			{
				Id = regionDomain.Id,
				Code = regionDomain.Code,
				Name = regionDomain.Name,
				RegionImageUrl = regionDomain.RegionImageUrl
			};

			return Ok(regionDto); 
		}

		// POST to Create New Region
		// Post: https://localhost:portnumber/api/regions
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			// Map Or convert dto to domain model
			var regionDomainModel = new Region
			{
				Code = addRegionRequestDto.Code,
				Name = addRegionRequestDto.Name,
				RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};

			// Use Domain Model to Create Region
			await dbContext.Regions.AddAsync(regionDomainModel);
			await dbContext.SaveChangesAsync();

			// Map Domain Model back to Dto
			var regionDto = new RegionDto
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.Id}, regionDto);
		}

		// Update region
		// Put: https://localhost:portnumber/api/regions/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if(regionDomainModel == null)
			{
				return NotFound();
			}

			// Map Dto to Domain model
			regionDomainModel.Code = updateRegionRequestDto.Code;
			regionDomainModel.Name = updateRegionRequestDto.Name;
			regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

			await dbContext.SaveChangesAsync();

			// Convert Domain Model to dto
			var regionDto = new RegionDto
			{
				Id=regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};

			return Ok(regionDto);
		}

		// Delete Region
		// Delete: https://localhost:portnumber/api/regions/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			// Delete region
			dbContext.Regions.Remove(regionDomainModel);
			await dbContext.SaveChangesAsync();

			// return deleted Region back
			// map Domain model to DTO
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

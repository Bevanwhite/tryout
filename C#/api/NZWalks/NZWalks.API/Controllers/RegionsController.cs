using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;
		private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;

		public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.regionRepository = regionRepository;
			this.mapper = mapper;
		}


		// Get All Regions
		// Get: https://localhost:portnumber/api/regions
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			// Get Data From database - domain models
			var regionsDomain = await regionRepository.GetAllAsync();

			// return DTOs
			return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
		}

		// Get Single Region (Get Region by Id)
		// Get: https://localhost:portnumber/api/regions/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		// documented Swagger documented
		[SwaggerResponse(200, "Resource found", typeof(RegionDto))]
		[SwaggerResponse(404, "Resource not found")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			// var region = dbContext.Regions.Find(id);
			// another way to get the Region by Id
			var regionDomain = await regionRepository.GetByIdAsync(id);

			if (regionDomain == null)
			{
				return NotFound();
			}

			// return the DTO back to client
			return Ok(mapper.Map<RegionDto>(regionDomain));
		}

		// Post to Create New Region
		// Post: https://localhost:portnumber/api/regions
		[HttpPost]
		// documented Swagger documented
		[SwaggerResponse(201, "Resource created successfully", typeof(RegionDto))]
		[SwaggerResponse(400, "One or more validation errors occurred.")]
		public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			// Map or convert DTO to Domain Model
			var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

			// saving regionModel in the database
			regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

			var regionDto = mapper.Map<RegionDto>(regionDomainModel);

			return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
		}

		// Update region
		// Put: https://localhost:portnumber/api/regions/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		[SwaggerResponse(404, "Resource not found")]
		[SwaggerResponse(200, "Resource found", typeof(RegionDto))]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);


			regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

			if (regionDomainModel == null)
			{
				return NotFound();
			};

			// Convert Domain Model to DTO and return the items
			return Ok(mapper.Map<RegionDto>(regionDomainModel));
		}

		// Delete Region
		// Delete: https://localhost:portnumber/api/regions/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		[SwaggerResponse(404, "Resource not found")]
		[SwaggerResponse(200, "Resource found", typeof(RegionDto))]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var regionDomainModel = await regionRepository.DeleteAsync(id);

			if (regionDomainModel == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<RegionDto>(regionDomainModel));
		}
	}
}

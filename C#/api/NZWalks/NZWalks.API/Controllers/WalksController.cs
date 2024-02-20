using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		private readonly IWalkRepository walkRepository;
		private readonly IMapper mapper;

		public WalksController(IWalkRepository walkRepository, IMapper mapper)
		{
			this.walkRepository = walkRepository;
			this.mapper = mapper;
		}

		// Create walk
		// Post: https://localhost:portnumber/api/walks
		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			// Map DTO to Domain Model
			var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

			walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

			return Ok(mapper.Map<WalkDto>(walkDomainModel));
		}

		// Get Walks
		// Get: https://localhost:portnumber/api/walks?filterOn=Name&filterQuery=Track
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
		{
			var walksDomainModel = await walkRepository.GetAllAsync();

			return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
		}

		// Get walk by id
		// Get: https://localhost:portnumber/api/walks/{id}
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.GetByIDAsync(id);

			if (walkDomainModel == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<WalkDto>(walkDomainModel));
		}

		//update the Walk
		//Put: https://localhost:portnumber/api/walks/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		[ValidateModel]
		public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
		{
			// Map Dto to Domain model
			var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

			walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

			if (walkDomainModel == null)
			{
				return NotFound();
			}

			// Map Domain Model to Dto
			return Ok(mapper.Map<WalkDto>(walkDomainModel));

		}

		// Delete the Walk By Id
		// Delete: /api/walks/{id}
		[HttpDelete]
		[Route("{id:Guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var walkDomianModel = await walkRepository.DeleteAsync(id);

			if (walkDomianModel == null)
			{
				return NotFound();
			}

			// Map Domain Model to Dto
			return Ok(mapper.Map<WalkDto>(walkDomianModel));
		}
	}
}

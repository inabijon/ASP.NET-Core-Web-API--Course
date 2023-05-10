using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilter;
using NZWalksAPI.Models.Domein;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories.IRepository;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _repo;

        public WalksController(IMapper mapper, IWalkRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // api/walks?filterOn=Name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetAllWalks(
            [FromQuery] string? filterOn, 
            [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000)
        {
            var walks = await _repo.GetALlWalksAsync(
                filterOn, 
                filterQuery, 
                sortBy, 
                isAscending ?? true,
                pageNumber,
                pageSize);

            var walksMapped = _mapper.Map<List<WalkDto>>(walks);

            return Ok(walksMapped);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalk(Guid id)
        {
            var walk = await _repo.GetWalkAsync(id);

            if(walk == null)
            {
                return NotFound();
            }

            return Ok(walk);
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalks([FromBody] AddWalksDto walks)
        {
           
            var walkDomeinModel = _mapper.Map<Walk>(walks);

            var walk = await _repo.CreateWalkAsync(walkDomeinModel);

            var createdWalk = _mapper.Map<WalkDto>(walk);

            return Ok(createdWalk);
           
          
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalk walk)
        {
            var walkDomeinModel = _mapper.Map<UpdateWalk>(walk);

            var updatedWalk = await _repo.UpdateWalkAsync(id, walkDomeinModel);

            if(updatedWalk == null)
            {
                return NotFound();
            }

            return Ok(updatedWalk);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walk = await _repo.DeleteWalkAsync(id);

            if(walk == null)
            {
                return NotFound();
            }

            return Ok(walk);
        }
    }
}

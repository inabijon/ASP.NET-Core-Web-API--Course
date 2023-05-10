using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilter;
using NZWalksAPI.Models.Domein;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories.IRepository;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> logger1;

        public RegionsController(IRegionRepository repo,
            IMapper mapper, ILogger<RegionsController> logger1)
        {
            _repo = repo;
            _mapper = mapper;
            this.logger1 = logger1;
        }

        [HttpGet]
        /*[Authorize(Roles = "Reader")]*/
        public async Task<IActionResult> GetRegions()
        {

            var regions = await _repo.GetAllRegionsAsync();

            var mapped = _mapper.Map<List<RegionDto>>(regions);

            logger1.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regions)}");

            return Ok(mapped);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegion([FromRoute] Guid id)
        {
            var region = await _repo.GetRegionAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<RegionDto>(region);

            return Ok(mapped);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Creater")]
        public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto region)
        {
            var regionModel = _mapper.Map<Region>(region);

            regionModel = await _repo.CreateRegionAsync(regionModel);

            var regionDto = _mapper.Map<RegionDto>(regionModel);

            return CreatedAtAction(nameof(GetRegion), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Creater")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegion updateRegion)
        {
            var regionnDomainModel = _mapper.Map<UpdateRegion>(updateRegion);

            var region = await _repo.UpdateRegionAsync(id, regionnDomainModel);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(regionDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Creater")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await _repo.DeleteRegionAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var returnDto = _mapper.Map<RegionDto>(region);

            return Ok(returnDto);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domein;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories.IRepository;

namespace NZWalksAPI.Repositories.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZContext _context;
        public RegionRepository(NZContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Region>> GetAllRegionsAsync() {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, UpdateRegion region)
        {
            var findRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (findRegion == null)
            {
                return null;
            }
            
            findRegion.Name = region.Name;
            findRegion.Code = region.Code;
            findRegion.RegionImageUrl = region.RegionImageUrl;

            await _context.SaveChangesAsync();

            return findRegion;

        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var regionExist = await _context.Regions.FirstOrDefaultAsync(y => y.Id == id);

            if (regionExist == null) {
                return null;
            }

            _context.Regions.Remove(regionExist);
            await _context.SaveChangesAsync();

            return regionExist;
        }
    }
}

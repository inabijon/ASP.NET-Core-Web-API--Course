using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domein;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories.IRepository;

namespace NZWalksAPI.Repositories.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZContext _context;
        public WalkRepository(NZContext context)
        {
            _context = context;
        }


        //  .Include("Difficulty") ===  .Include(x => x.Difficulty)
        public async Task<IReadOnlyList<Walk>> GetALlWalksAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000)
        {
            var walks = _context.Walks
                .Include("Difficulty")
                .Include("Region")
                .AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false) { 
                
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                  walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)) { 
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;


            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetWalkAsync(Guid id)
        {
            var walk = await _context.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);

            if(walk == null)
            {
                return null;
            }

            return walk;
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, UpdateWalk walk)
        {
            var findWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (findWalk == null)
            {
                return null;
            }

            findWalk.Name = walk.Name;
            findWalk.Description = walk.Description;
            findWalk.LengthInKm = walk.LengthInKm;
            findWalk.DifficultyId = walk.DifficultyId;
            findWalk.RegionId = walk.RegionId;
            findWalk.WalkImageUrl = walk.WalkImageUrl;

            await _context.SaveChangesAsync();

            return findWalk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var findWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if(findWalk == null)
            {
                return null;
            }

            _context.Walks.Remove(findWalk);
            await _context.SaveChangesAsync();

            return findWalk;
        }

    }
}

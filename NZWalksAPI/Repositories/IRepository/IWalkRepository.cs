using NZWalksAPI.Models.Domein;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Repositories.IRepository
{
    public interface IWalkRepository
    {
        Task<IReadOnlyList<Walk>> GetALlWalksAsync(
            string? filterOn = null, 
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000);
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<Walk?> GetWalkAsync(Guid id);
        Task<Walk?> UpdateWalkAsync(Guid id, UpdateWalk walk);
        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}

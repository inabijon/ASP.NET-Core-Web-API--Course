using NZWalksAPI.Models.Domein;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Repositories.IRepository
{
    public interface IRegionRepository
    {
        Task<IReadOnlyList<Region>> GetAllRegionsAsync();
        Task<Region> GetRegionAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid id, UpdateRegion region);
        Task<Region?> DeleteRegionAsync(Guid id);
    }
}

using NZWalksAPI.Models.Domein;

namespace NZWalksAPI.Repositories.IRepository
{
    public interface IIImageRepository
    {
      public Task<Image> Upload(Image image);
    }
}

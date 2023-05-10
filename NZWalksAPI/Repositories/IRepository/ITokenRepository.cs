using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repositories.IRepository
{
    public interface ITokenRepository
    {
        public string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}

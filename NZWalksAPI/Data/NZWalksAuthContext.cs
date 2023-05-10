using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPI.Data
{
    public class NZWalksAuthContext : IdentityDbContext
    {
        public NZWalksAuthContext(DbContextOptions<NZWalksAuthContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "d4456b0f-bcd7-46e8-9b1a-10514df640e2";
            var createrId = "4610fd61-9a2a-4ae8-8ed1-f748e0415f47"; 

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                   Id = readerId,
                   ConcurrencyStamp = readerId,
                   Name = "Reader",
                   NormalizedName = "Reader".ToUpper(),
                },
                new IdentityRole
                {
                    Id = createrId,
                    ConcurrencyStamp = createrId,
                    Name = "Creater",
                    NormalizedName = "Creater".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

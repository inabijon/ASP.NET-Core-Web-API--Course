using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domein;

namespace NZWalksAPI.Data
{
    public class NZContext : DbContext
    {
        public NZContext(DbContextOptions<NZContext> options) : base(options)
        {
        }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // seed data for difficulties
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("983fa0a0-5a8d-42c1-a13b-d7b9b5c3e6b0"),
                    Name = "Easy",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("0118d470-04cb-4425-ace4-ebfd6daaccb0"),
                    Name = "Medium",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("3f80309a-a30a-4a1b-9ad6-4060933d2680"),
                    Name = "Hard",
                },
                new Difficulty()
                {
                    Id = Guid.Parse("ca91c06f-8927-4232-a7a2-0f3280373df2"),
                    Name = "Very Hard",
                }
            };


            // seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // seed data for regions
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}

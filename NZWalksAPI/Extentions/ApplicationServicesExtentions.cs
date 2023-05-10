using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Repositories.IRepository;
using NZWalksAPI.Repositories.Repositories;

namespace NZWalksAPI.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IWalkRepository, WalkRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IIImageRepository, ImageRepository>();

            return services;
        }
    }
}

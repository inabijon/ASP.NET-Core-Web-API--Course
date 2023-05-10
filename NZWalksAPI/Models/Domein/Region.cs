namespace NZWalksAPI.Models.Domein
{
    public class Region: BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}

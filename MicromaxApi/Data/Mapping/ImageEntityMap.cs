using Dapper.FluentMap.Mapping;
using MicromaxApi.Data.Entity;

namespace MicromaxApi.Data.Mapping
{
    public class ImageEntityMap : EntityMap<ImageEntity>
    {
        public ImageEntityMap()
        {
            this.Map(x => x.ImageName).ToColumn("ImageName");
            this.Map(x => x.ImageData).ToColumn("ImageData");
            this.Map(x => x.ImageType).ToColumn("ImageType");
            this.Map(x => x.Location).ToColumn("Location");
            this.Map(x => x.User).ToColumn("User");
            this.Map(x => x.DateCreated).ToColumn("DateCreated");
        }
    }
}

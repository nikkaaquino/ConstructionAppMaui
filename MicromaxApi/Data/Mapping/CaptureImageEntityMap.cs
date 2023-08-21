using Dapper.FluentMap.Mapping;
using MicromaxApi.Data.Entity;

namespace MicromaxApi.Data.Mapping
{
    public class CaptureImageEntityMap : EntityMap<CaptureImageEntity>
    {
        public CaptureImageEntityMap() 
        {
            this.Map(x => x.ImageId).ToColumn("img_id");
            this.Map(x => x.ImageName).ToColumn("img_name");
            this.Map(x => x.ImageData).ToColumn("img");
            this.Map(x => x.CreatedBy).ToColumn("created_by");
            this.Map(x => x.DateCreated).ToColumn("date_created");
        }
    }
}

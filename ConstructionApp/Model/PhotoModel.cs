
using System.Text.Json.Serialization;

namespace ConstructionApp.Model
{
    public class PhotoModel
    {
        public int ImageId { get; set; }

        public string ImageName { get; set; }

      //  public string ImageUrl { get; set; }

        public string CreatedBy { get; set; }
    }

    [JsonSerializable(typeof(List<PhotoModel>))]
    internal sealed partial class PhotoModelContext : JsonSerializerContext
    {

    }
}


using System.Text.Json.Serialization;

namespace ConstructionApp.Model
{
    public class PhotoModel
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageData { get; set; }
        public string Location { get; set; }
        public string User { get; set; }
        public byte[] ImageView { get; set; }
        public string ImageType { get; set; }
    }
}

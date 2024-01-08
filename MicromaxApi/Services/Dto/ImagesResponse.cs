using Newtonsoft.Json;

namespace MicromaxApi.Services.Dto
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ImagesResponse
    {
        [JsonProperty]
        public int ImageId { get; set; }

        [JsonProperty]
        public string ImageName { get; set; }

        [JsonProperty]
        public string ImageData { get; set; }

        [JsonProperty]
        public DateTime? DateCreated { get; set; }

        [JsonProperty]
        public string User { get; set; }

        [JsonProperty]
        public string Location { get; set; }

        //[JsonProperty]
        //public byte[] ImageView { get; set; }

        [JsonProperty]
        public string ImageType { get; set; }
        [JsonProperty]
        public string ImageUrl { get; set; }
    }
}

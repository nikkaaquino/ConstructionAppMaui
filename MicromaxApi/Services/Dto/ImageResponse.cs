using Newtonsoft.Json;

namespace MicromaxApi.Services.Dto
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageResponse
    {
        [JsonProperty]
        public int ImageId { get; set; }

        [JsonProperty]
        public string ImageName { get; set; }

        [JsonProperty]
        public byte[] ImageData { get; set; }

        [JsonProperty]
        public DateTime? DateCreated { get; set; }

        [JsonProperty]
        public string CreatedBy { get; set; }
    }
}

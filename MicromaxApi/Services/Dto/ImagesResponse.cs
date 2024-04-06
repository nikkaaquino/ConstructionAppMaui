using Newtonsoft.Json;

namespace MicromaxApi.Services.Dto
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ImagesResponse
    {
        [JsonProperty]
        public string ImageName { get; set; }

        //[JsonProperty]
        //public byte[] ImageData { get; set; }

        [JsonProperty]
        public DateTime? DateCreated { get; set; }

        [JsonProperty]
        public string User { get; set; }

        [JsonProperty]
        public string Location { get; set; }


        [JsonProperty]
        public string ImageType { get; set; }
    }
}

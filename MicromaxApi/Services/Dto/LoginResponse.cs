using Newtonsoft.Json;

namespace MicromaxApi.Services.Dto
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LoginResponse
    {
        [JsonProperty]
        public string UserId { get; set; }
        [JsonProperty]
        public string UsrPassword { get; set; }
    }
}

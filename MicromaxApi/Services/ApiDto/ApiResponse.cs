using System;
using Newtonsoft.Json;

namespace api.motorstar.Services.ApiDto
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
        }

        [JsonProperty(Order = 1)]
        public T Data { get; set; }

        //[JsonProperty(Order = 2)]
       // public Pagination Paging { get; set; }

    }
}

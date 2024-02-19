using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FcmbInterview.Application.Common.Interfaces.Persistence.Common
{
    public class GenericResponse<T>
    {
        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}

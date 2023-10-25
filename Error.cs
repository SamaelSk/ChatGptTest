using Newtonsoft.Json;

namespace ChatGptTest
{
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("param")]
        public object Param { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}

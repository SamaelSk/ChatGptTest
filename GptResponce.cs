using Newtonsoft.Json;

namespace ChatGptTest
{
    public class GptResponce
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}

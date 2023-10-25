using System.Text;
using Newtonsoft.Json;

namespace ChatGptTest
{
    public class Utilities
    {
        static string answer = string.Empty;


        public static async Task<string> SendMessageAsync(string message, ChatGptSettings settings)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {settings.ApiKey}");

            var requestBody = new
            {
                prompt = message,
                max_tokens = 50,
                temperature = 0.7,
                n = 1
            };
            var requestBodyJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var httpContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(settings.Endpoint, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent))
            {
                answer = "The bot could not answer your question";
            }
            else
            {
                var jsonResponse = JsonConvert.DeserializeObject<GptResponce>(responseContent);
                answer = (jsonResponse != null) ? jsonResponse.Error.Message : string.Empty;
                FileWriter(answer, settings);
            }

            return answer;
        }

        private static async void FileWriter(string responce, ChatGptSettings settings)
        {
            using StreamWriter writer = new StreamWriter(settings.Path, true);
            await writer.WriteLineAsync(responce);
        }
    }
}

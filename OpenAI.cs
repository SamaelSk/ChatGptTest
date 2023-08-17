using System.Text;

namespace ChatGptTest
{
    public class OpenAI
    {
        const string apiKey = "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";
        const string endpoint = "https://api.openai.com/v1/engines/davinci/completions";
        const string path = "Chat.txt";
        static string answer = string.Empty;


        public static async Task<string> SendMessageAsync(string message)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                prompt = message,
                max_tokens = 50,
                temperature = 0.7,
                n = 1
            };
            var requestBodyJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var httpContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(endpoint, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent))
            {
                answer = "The bot could not answer your question";
            }
            else
            {
                dynamic jsonResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                answer = jsonResponse.choices[0].text.Value;
                FileWriter(answer);
            }

            return answer;
        }

        private static async void FileWriter(string responce)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync(responce);
            }
        }
    }
}

using ChatGptTest;
using Microsoft.Extensions.Configuration;
class Program
{
    static async Task Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json")
            .Build();

        var settings = configuration.Get<ChatGptSettings>();

        while (true)
        {
            Console.Write("Enter your question: ");
            var userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("You need to type something");
                continue;
            }

            if (userInput.ToLower() == "quit" || userInput.ToLower() == "exit")
            {
                break;
            }

            var response = await Utilities.SendMessageAsync(userInput, settings);

            Console.WriteLine("ChatGPT: " + response);
        }
    }
}
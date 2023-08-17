using ChatGptTest;

class Program
{
    static async Task Main()
    {
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

            var response = await OpenAI.SendMessageAsync(userInput);

            Console.WriteLine("ChatGPT: " + response);
        }
    }
}
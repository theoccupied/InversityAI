using OpenAI;
using OpenAI.Assistants;
using OpenAI.Chat;
using OpenAI.Models;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Hackathon
{
    internal class Program
    {
        public static bool Running = true;
        public static bool talking = false;
        public static string request = "";
        public static string responseText = "";
        public static bool thinking = false;
        public static bool speaking = false;

        public static int curPerson = 0;

        public static string[] people = new string[]
        {
            "Nichola Tesla",
            "Thomas Edison",
            "Albert Einstein",
            "Alexander the Great",
            "Pythagoras",
            "William Shakespeare",
            "Al Biruni"
        };
        static void Main(string[] args)
        {
            //

            new Program();
        }
        public Program()
        {

            Thread thread = new Thread(new ThreadStart(Test));
            thread.Start();
            MainWindow window = new MainWindow();
            window.Run();
            Running = false;
        }
        async void Test()
        {
            var synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();

            while (Running)
            {
                if (request != "")
                {
                    thinking = true;
                    string API = "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";

                    using var api = new OpenAIClient(API);

                    var messages = new List<Message>
                    {
                        new Message(Role.System, "Speak from the perspective of " + people[curPerson % people.Length] + " explaining your own theories. Give short answers. Have some humour and keep answers brief!"),
                        new Message(Role.User, request)
                    };

                    var chatRequest = new ChatRequest(messages, model: OpenAI.Models.Model.GPT4);
                    var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
                    Console.WriteLine($"{response.FirstChoice.Message.Role}: {response.FirstChoice.Message.Content} | Finish Reason: {response.FirstChoice.FinishDetails}");

                    thinking = false;
                    speaking = true;
                    responseText = ((JsonElement)response.FirstChoice.Message.Content).ToString();
                    synthesizer.Speak(responseText);
                    speaking = false;
                    request = "";
                }
            }
            Console.WriteLine("Hello!");
            
        }
    }
}

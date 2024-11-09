using OpenAI;
using OpenAI.Assistants;
using OpenAI.Chat;
using OpenAI.Models;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text.Json;

namespace Hackathon
{
    internal class Program
    {
        public static bool Running = true;
        public static bool talking = false;
        static void Main(string[] args)
        {
            //

            new Program();
        }
        public Program()
        {

            Thread thread = new Thread(new ThreadStart(test));
            thread.Start();
            MainWindow window = new MainWindow();
            window.Run();
            Running = false;
        }
        async void test()
        {
            while (Running)
            {
                /*
                string API = "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";

                using var api = new OpenAIClient(API);

                var messages = new List<Message>
                {
                    new Message(Role.System, "Speak from the perspective of Nichola Tesla explaining your own theories. Give short answers. Have some humour and keep answers brief!"),
                    new Message(Role.User, "What are your big theories?")
                };

                var chatRequest = new ChatRequest(messages, model: OpenAI.Models.Model.GPT4);
                var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
                Console.WriteLine($"{response.FirstChoice.Message.Role}: {response.FirstChoice.Message.Content} | Finish Reason: {response.FirstChoice.FinishDetails}");
                var synthesizer = new SpeechSynthesizer();
                synthesizer.SetOutputToDefaultAudioDevice();

                synthesizer.Speak(((JsonElement)response.FirstChoice.Message.Content).ToString());
                */
            }
            Console.WriteLine("Hello!");
            
        }
    }
}

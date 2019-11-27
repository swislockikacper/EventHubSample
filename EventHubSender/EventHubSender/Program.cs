using Microsoft.Azure.EventHubs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHubSender
{
    class Program
    {
        private static EventHubClient eventHubClient;
        private const string EventHubConnectionString = "PLACEHOLDER";
        private const string EventHubName = "PLACEHOLDER";

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EventHubConnectionString)
            {
                EntityPath = EventHubName
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            var firstMessageNumber = 1;

            while (true)
            {
                Console.WriteLine("Click enter to send next 100 events or other to exit");

                var option = Console.ReadKey();

                if (option.Key == ConsoleKey.Enter)
                {
                    await SendEventsToEventHub(firstMessageNumber);
                    firstMessageNumber += 100;
                }
                else
                    break;
            }

            await eventHubClient.CloseAsync();
        }

        private static async Task SendEventsToEventHub(int firstMessageNumber)
        {
            for (int i = 0; i < 100; i++)
            {
                var message = $"Message {firstMessageNumber + i}";

                Console.WriteLine($"Sending: {message}");

                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
            }
        }
    }
}

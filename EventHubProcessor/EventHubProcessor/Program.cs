using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System;
using System.Threading.Tasks;

namespace EventHubProcessor
{
    class Program
    {
        private const string EventHubConnectionString = "PLACEHOLDER";
        private const string EventHubName = "PLACEHOLDER";
        private const string StorageContainerName = "PLACEHOLDER";
        private const string StorageAccountName = "PLACEHOLDER";
        private const string StorageAccountKey = "PLACEHOLDER";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            await eventProcessorHost.RegisterEventProcessorAsync<EventProcessor>();

            Console.WriteLine("Receiving... Click to exit");
            Console.ReadLine();

            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}

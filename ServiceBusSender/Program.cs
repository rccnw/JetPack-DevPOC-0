using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusSender
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://jetpack-devpoc-0.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=xGtrXODqlYBXSd2L4anz72XXW99KroL/xVWuhOPSIzs=";
        const string QueueName = "queue1";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            const int numberOfMessages = 10;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
            await SendMessageAsync(numberOfMessages);
            await queueClient.CloseAsync();
        }


        static async Task SendMessageAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i=0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the Queue
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending Message: {messageBody}");

                    // Send the message to the queue.
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {ex.Message}");
            }

        }

    }
}

using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace getQueueLength
{
    class Program
    {
        static async Task GetQueueLength()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                    "<storage-accountname>",
                    "<storage-accountkey>"), 
                    true);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("getcount");

            // Fetch the queue attributes.
            await queue.FetchAttributesAsync();

            // Retrieve the cached approximate message count.
            int? cachedMessageCount = queue.ApproximateMessageCount;

            // Display number of messages.
            Console.WriteLine("Number of messages in queue: " + cachedMessageCount);

            // We'll wait for a little while... 
            // Meanwhile, we'll remove few messages from the 'getcount' Queue so the number of messages will vary.
            Console.ReadLine();

            // Display the number of messages again.
            // It hasn't changed since we didn't fetch the attributes after some messages were removed or added.
            Console.WriteLine("Number of messages in queue - no attributes fetching: " + cachedMessageCount);

            await queue.FetchAttributesAsync();

            // Retrieve the cached approximate message count.
            cachedMessageCount = queue.ApproximateMessageCount;

            // Display the number of messages one more time.
            // Now the count is correct since we've fetched the attributes again.
            Console.WriteLine("Number of messages in queue - no attributes fetching: " + cachedMessageCount);


        }

        static void Main(string[] args)
        {
            GetQueueLength().Wait();
        }
    }
}

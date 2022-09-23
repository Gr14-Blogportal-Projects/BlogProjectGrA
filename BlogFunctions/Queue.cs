using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;


namespace BlogFunctions
{
    public class Queue
    {
        [FunctionName("Queue")]
        public void Run([QueueTrigger("emailqueue", Connection = "AzureWebJobsStorage") ]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");


        }
    }
}

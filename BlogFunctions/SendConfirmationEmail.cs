using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;

namespace BlogFunctions
{
    public static class SendConfirmationEmail
    {
        [FunctionName("SendConfirmationEmail")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, ILogger log)
        {
            try
            {


                log.LogInformation("C# HTTP trigger function processed a request.");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                var configuration = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("local.settings.json", true, true)
                                        .AddEnvironmentVariables()
                                        .Build();

                string connectionString = configuration["AzureWebJobsStorage"];
                string queueString = configuration["QueueName"];
                QueueClient qClient = new QueueClient(connectionString, queueString, new QueueClientOptions { MessageEncoding = QueueMessageEncoding.Base64 });
                qClient.CreateIfNotExists();
                if (qClient.Exists())
                {
                    try
                    {
                        qClient.SendMessage(requestBody);
                    }
                    catch (Exception e)
                    {

                        log.LogInformation($"Message could not be sent to queue (error: {e.Message} ) at: {DateTime.Now}");
                    }


                }

                string responseMessage = "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";


                return new OkObjectResult(responseMessage);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e);
            }
        }
    }
}

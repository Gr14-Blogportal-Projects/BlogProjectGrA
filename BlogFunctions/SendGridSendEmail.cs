// The 'From' and 'To' fields are automatically populated with the values specified by the binding settings.
//
// You can also optionally configure the default From/To addresses globally via host.config, e.g.:
//
// {
//   "sendGrid": {
//      "to": "user@host.com",
//      "from": "Azure Functions <samples@functions.com>"
//   }
// }
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;

namespace BlogFunctions
{
    public class SendGridSendEmail
    {
        [FunctionName("SendGridSendEmail")]
        [return: SendGrid(ApiKey = "SendGridKey", From = "mazarizainab@outlook.com")]
        public SendGridMessage Run([QueueTrigger("emailqueue", Connection = "AzureWebJobsStorage")]CreateEmail order, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed order: {order.Author}");

            SendGridMessage message = new SendGridMessage()
            {
                Subject = $"You created new blog {order.BlogTitle}!"
            };

            message.AddTo(order.Email);
            message.AddContent("text/plain", $"{order.Author}, you made blog ({order.BlogTitle}) at {order.Date}!");
            return message;
        }
    }

    public class CreateEmail
    {
        public string Author { get; set; }
        public string Email { get; set; }
        public string BlogTitle { get; set; }
        public DateTime Date { get; set; }

    }
}

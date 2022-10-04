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
using System.Collections.Generic;
using Microsoft.Azure.Documents;

namespace BlogFunctions
{
    public class SendGridSendCommentsEmail
    {
        [FunctionName("SendGridSendCommentsEmail")]
        [return: SendGrid(ApiKey = "SendGridKey", From = "mazarizainab@outlook.com")]
        public SendGridMessage Run([QueueTrigger("postcommentqueue", Connection = "AzureWebJobsStorage")] CommentEmail commentEmail, ILogger log)
        {
            //log.LogInformation($"C# Queue trigger function processed order: {order.Author}");

            SendGridMessage message = new SendGridMessage()
            {
                Subject = "Updates on your blog posts"
            };

            var content = "Updates on your blog posts!\n";
            foreach (var item in commentEmail.PostComments)
            {
                content += $"Your post \"{item.PostTitle}\" has {item.CommentCount} new comments!\n";
            }


            message.AddTo(commentEmail.Email);
            message.AddContent("text/plain", content);
            return message;
        }
    }
    public class CommentEmail
    {
        public string Email { get; set; }

        public List<PostComment> PostComments { get; set; }
    }

    public class PostComment
    {
        public  User User { get; set; }

        public string PostTitle { get; set; }

        public int CommentCount { get; set; }
    }
}

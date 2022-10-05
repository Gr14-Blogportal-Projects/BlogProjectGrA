using Azure.Storage.Queues;
using BlogTriggers.Models;
using BlogTriggers.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BlogTriggers
{
    public class TimeTriggerEmail
    {
        private readonly ILogger _logger;
        private readonly IPostCommentService _postCommentService;
        public TimeTriggerEmail(ILoggerFactory loggerFactory, IPostCommentService postCommentService)
        {
            _logger = loggerFactory.CreateLogger<TimeTriggerEmail>();
            _postCommentService = postCommentService;   
        }

        [Function("TimeTriggerEmail")]
        public void Run([TimerTrigger("0 0 7 * * *",  RunOnStartup = false)] MyInfo myTimer)
        {
            _logger.LogInformation($"Started timer execution at {DateTime.Now}");
            var commentGrouping = _postCommentService.GetPostsWithRecentComments().GroupBy(pc => pc.User);
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("local.settings.json", true, true)
                    .AddEnvironmentVariables()
                    .Build();

            string connectionString= configuration["AzureWebJobsStorage"];
            string queueString = configuration["AzureQueueName"];

            QueueClient queueClient = new QueueClient(connectionString, queueString, new QueueClientOptions
            { MessageEncoding = QueueMessageEncoding.Base64 });

            queueClient.CreateIfNotExists();
            try
            {
                foreach (var group in commentGrouping)
                {
                    //group is list of post comment, it has key use to groupby  user
                    var content = new CommentEmail
                    {
                        Email = group.Key.Email,
                        PostComments = group.ToList()
                    };
                    queueClient.SendMessage(JsonConvert.SerializeObject(content));
                }

            }
            catch (Exception e)
            {

                _logger.LogInformation($"Message could not be sent to queue (error: {e.Message} ) at: {DateTime.Now}");
            }

            _logger.LogInformation($"Total groups: {commentGrouping.Count()}");

            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class CommentEmail
    {
        public string Email { get; set; }

        public List<PostComment> PostComments { get; set; }
    }

    public class PostComment
    {
        public User User { get; set; }

        public string PostTitle { get; set; }

        public int CommentCount { get; set; }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}

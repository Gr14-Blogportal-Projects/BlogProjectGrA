using BlogTriggers.Data;
using BlogTriggers.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("local.settings.json", true, true)
                    .AddEnvironmentVariables()
                    .Build();
var connectionString = configuration["DefaultConnection"];
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s=>
    {
        s.AddDbContext<FuncDbContext>(options =>
                              options
                              .UseSqlServer(connectionString));
        s.AddScoped<IPostCommentService, PostCommentService>();
    })
    .Build();

host.Run();

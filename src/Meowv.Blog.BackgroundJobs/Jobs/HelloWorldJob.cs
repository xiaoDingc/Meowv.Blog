using log4net;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meowv.Blog.BackgroundJobs.Jobs
{
    public class HelloWorldJob : BackgroundService
    {
        public readonly ILog _Log;

        public HelloWorldJob()
        {
            _Log = LogManager.GetLogger(typeof(HelloWorldJob));

        }
            
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
              var msg = $"CurrentTime:{ DateTime.Now}, Hello World!";
                Console.WriteLine(msg);
                _Log.Info(msg);
                await  Task.Delay(1000,stoppingToken);
            }
        }
    }
}

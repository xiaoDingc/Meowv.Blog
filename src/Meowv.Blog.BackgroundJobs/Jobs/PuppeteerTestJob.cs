using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Meowv.Blog.ToolKits.Extensions;
using Meowv.Blog.ToolKits.Helper;
using MimeKit;
using MimeKit.Utils;
using PuppeteerSharp;


namespace Meowv.Blog.BackgroundJobs.Jobs
{
    public class PuppeteerTestJob : IBackgroundJob
    {
        public async Task ExecuteAsync()
        {
            var path = Path.Combine(Path.GetTempPath(), "meowv.png");

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new string[] { "--no-sandbox" }
            });

            using var page = await browser.NewPageAsync();

            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1920,
                Height = 1080
            });

            //如果这条url下载的内容过长 会导致无结果 以为是错的
            var url = "https://www.cnblogs.com/";

            // await page.GoToAsync(url, WaitUntilNavigation.Networkidle0);
            await page.GoToAsync(url, 0,new WaitUntilNavigation[]
            {
                WaitUntilNavigation.Networkidle0
            });

            var content = await page.GetContentAsync();

           // await page.PdfAsync("baidu.pdf", new PdfOptions());


            await page.ScreenshotAsync(path, new ScreenshotOptions()
            {
                FullPage = true,
                Type = ScreenshotType.Png,
            });

            //发送带图片的Email
          var builder=new BodyBuilder();

            var image = builder.LinkedResources.Add(path);
            
            image.ContentId=MimeUtils.GenerateMessageId();

            builder.HtmlBody = "当前时间:{0}.<img src=\"cid:{1}\"/>".Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), image.ContentId);


            var message=new MimeMessage
            {
                Subject = "【定时任务】每日热点数据抓取任务推送",
                Body = builder.ToMessageBody()
            };

            await EmailHelper.SendEmailAsync(message);
        }
    }
}

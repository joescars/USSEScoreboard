using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Scoreboard.Functions.ViewModels;

namespace Scoreboard.Functions
{
    public static class SendHighlights
    {
        [FunctionName("SendHighlights")]
        public static void Run([QueueTrigger("sedash-mailqueue", Connection = "AzureWebJobsStorage")]string myQueueItem, TraceWriter log)
        {
            log.Info($"SendHighlights Triggered");

            bool isInterceptor = Convert.ToBoolean(Environment.GetEnvironmentVariable("isInterceptor"));
            string interceptorEmail = Environment.GetEnvironmentVariable("interceptorEmail");

            HighlightMessage hm = new HighlightMessage();
            hm = JsonConvert.DeserializeObject<HighlightMessage>(myQueueItem);

            string sendTo = hm.email;

            // If email interceptor is set, send to that addy
            if (isInterceptor)
            {
                sendTo = interceptorEmail;
            }

            log.Info($"Sending to: {sendTo}");

            //SendMail(sendTo, hm.messagebody).Wait();
        }

        static async Task SendMail(string toEmail, string msgBody)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Environment.GetEnvironmentVariable("fromEmail"), Environment.GetEnvironmentVariable("fromName"));
            var subject = Environment.GetEnvironmentVariable("emailSubject");
            var to = new EmailAddress(toEmail);
            var plainTextContent = "";
            var htmlContent = msgBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }


    }
}

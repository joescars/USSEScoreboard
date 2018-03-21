using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace Scoreboard.Functions
{
    public static class EventGridTester
    {
        [FunctionName("EventGridTester")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            TestEvent t = new TestEvent("Bob","Smith","Attorney");
            List<TestEvent> lt = new List<TestEvent>();
            lt.Add(t);
            
            string key = Environment.GetEnvironmentVariable("eventGridKey");
            string endpoint = Environment.GetEnvironmentVariable("eventGridEndpoint");

            var content = JsonConvert.SerializeObject(lt);
            log.Info(content);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("aeg-sas-key", key);
            var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(endpoint, httpContent);
            var resultText = await result.Content.ReadAsStringAsync();
            log.Info($"Response: {resultText}.");

            return req.CreateResponse(HttpStatusCode.OK);
        }

        public class TestEvent : EventGridEvent
        {

            public TestEvent()
            {
                Id = Guid.NewGuid().ToString();
                EventType = "TestEvent";
                EventTime = DateTime.Now;
            }

            public TestEvent(string fname, string lname, string desc) : this()
            {
                Subject = "Test Subject";
                Data = new PayloadData
                {
                    FirstName = fname,
                    LastName = lname,
                    Description = desc
                };
            }
        }

        public class PayloadData
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Description { get; set; }
        }

        
    }
}

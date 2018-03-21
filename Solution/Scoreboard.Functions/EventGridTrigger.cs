using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.EventGrid.Models;

namespace Scoreboard.Functions
{
    public static class EventGridTrigger
    {

        [FunctionName("EventGridHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var messages = await req.Content.ReadAsAsync<JArray>();

            // If the request is for subscription validation, send back the validation code.
            if (messages.Count > 0 && string.Equals((string)messages[0]["eventType"],
                "Microsoft.EventGrid.SubscriptionValidationEvent",
                System.StringComparison.OrdinalIgnoreCase))
            {
                log.Info("Validate request received");
                return req.CreateResponse<object>(new
                {
                    validationResponse = messages[0]["data"]["validationCode"]
                });
            }

            // The request is not for subscription validation, so it's for one or more events.
            foreach (JObject message in messages)
            {
                // Handle one event.
                EventGridEvent eventGridEvent = message.ToObject<EventGridEvent>();
                log.Info($"Subject: {eventGridEvent.Subject}");
                log.Info($"Time: {eventGridEvent.EventTime}");
                log.Info($"Event data: {eventGridEvent.Data.ToString()}");
            }

            return req.CreateResponse(HttpStatusCode.OK);
        }



    }
}

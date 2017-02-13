#r "Newtonsoft.Json"
#r "SendGrid"

using System.Net;
using Newtonsoft.Json;
using System.Configuration;
using SendGrid.Helpers.Mail;

public static Mail Run(HttpRequestMessage req, TraceWriter log, out Mail message)
{
    log.Info("C# HTTP trigger function processed a request.");

    //Retrieve Object and Convert
    string myData = req.Content.ReadAsStringAsync().Result;
    HighlightMessage hm = new HighlightMessage();
    hm = JsonConvert.DeserializeObject<HighlightMessage>(myData);

    log.Info($"Sending to: {hm.email}");

    Email from = new Email("admin@sedash.azurewebsites.net","Southeast Dashboard");
    string subject = "South East Weekly Highlights";
    Email to = new Email(hm.email);
    Content content = new Content("text/html", hm.messagebody);
    message = new Mail(from, subject, to, content);

    return message;
    
}

public class HighlightMessage {

    public string messagebody {get;set;}
    public string email {get;set;}

}
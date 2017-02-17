#r "Newtonsoft.Json"

using System;
using System.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Net;
using System.Text;

public static async void Run(TimerInfo myTimer, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}"); 
    string highlights = await GetHighlights(); 
    var usersToSend = await GetUsers();
    foreach (DashboardUser d in usersToSend)
    {
        //await SendReport(highlights, d.Email);
        log.Info($"Sending Email to {d.Email}");
    }
    log.Info("Function Complete");  
}

private static Task completedTask = Task.FromResult(false);
static Task SendReport(string body, string email)
{
    string SendHighlightsURL = ConfigurationManager.AppSettings["SendHighlightsURL"];
    WebRequest request = WebRequest.Create(SendHighlightsURL);
    
    request.Method = "POST";

    HighlightMessage hm = new HighlightMessage();
    hm.email = email;
    hm.messagebody = body;

    string postData = JsonConvert.SerializeObject(hm);
    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
    request.ContentType = "application/json";
    request.ContentLength = byteArray.Length;

    Stream dataStream = request.GetRequestStream();
    dataStream.Write(byteArray, 0, byteArray.Length);
    dataStream.Close();

    WebResponse response = request.GetResponse();
    dataStream = response.GetResponseStream();
    StreamReader reader = new StreamReader(dataStream);
    string responseFromServer = reader.ReadToEnd();

    reader.Close();
    dataStream.Close();
    response.Close();   
    
    return completedTask;
}

static async Task<string> GetHighlights()
{
    string body = "<!DOCTYPE html>";
    body += "<html><head><style type=\"text/css\">body{font-family:Arial;font-size:13px;}</style></head><body>";
    
    using (var db = new HighlightContext())
    {
        // Runs on Monday to set for items ending 
        // previous Sunday or Friday
        DateTime startDate = DateTime.Today.AddDays(-1);
        DateTime endDate = DateTime.Today.AddDays(-1);

        body += "<h3>Highlights for Week Ending " + startDate.ToShortDateString() + "</h3>";
        body += "<hr size=\"1\">";

        var myQuery = await db.Highlights
        .Include(h => h.UserProfile)
        .Where(h => h.DateEnd >= startDate && h.DateEnd <= endDate)
        .Select(h => new HighlightSearchResult
        {
            HighlightId = h.HighlightId,
            FullName = h.UserProfile.FirstName + " " + h.UserProfile.LastName,
            Body = h.Body,
            DateStart = h.DateStart,
            DateEnd = h.DateEnd,
            DateCreated = h.DateCreated
        })
        .OrderBy(h => h.DateCreated)
        .OrderBy(h => h.FullName)
        .ToListAsync();

        foreach (HighlightSearchResult h in myQuery)
        {
            body += "<strong><h4>" + h.FullName + " - " + h.DateStart.ToShortDateString() + "->" + h.DateEnd.ToShortDateString() + "</h4></strong>";
            body += h.Body;
            body += "<hr size=\"1\">";
        }

    }
    body += "</body></html>";    
    return body;
}

static async Task<List<DashboardUser>> GetUsers()
{
    using (var db = new HighlightContext())
    {
        var myUsers = await db.DashboardUsers
            .ToListAsync();
        return myUsers;
    }
}

[Table("Highlight")]
public class Highlight
{
    public int HighlightId { get; set; }
    public string Body { get; set; }            
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

}

[Table("AspNetUsers")]
public class DashboardUser
{
    // Only mapp the fields we need to use
    public string Id { get; set; }
    public string Email { get; set; }
}

[Table("UserProfile")]
public class UserProfile
{
    public int UserProfileId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }
}

public class HighlightSearchResult
{
    public int HighlightId { get; set; }
    public string FullName { get; set; }
    public string Body { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public DateTime DateCreated { get; set; }

}

public class HighlightMessage {

    public string messagebody {get;set;}
    public string email {get;set;}

}

public class HighlightContext : DbContext
{
    public HighlightContext()
        : base("name=HighlightContext")
    {
    }
    public virtual DbSet<Highlight> Highlights { get; set; }
    public virtual DbSet<UserProfile> UserProfiles { get; set; }
    public virtual DbSet<DashboardUser> DashboardUsers { get; set; }
}


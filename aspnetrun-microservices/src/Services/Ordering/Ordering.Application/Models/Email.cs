namespace Ordering.Application.Models;
#nullable enable
public class Email
{
    public Email()
    {
        To = string.Empty;
        Subject = string.Empty;
        Body = string.Empty;
    }

    public Email(string? to, string? body,string? subject)
    {
        To = to;
        Subject = subject;
        Body = body;
    }

    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    
}
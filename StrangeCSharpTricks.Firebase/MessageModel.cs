namespace StrangeCSharpTricks.Firebase;

public class MessageModel
{
    public int Id { get; set; }
    public int FromUserId { get; set; }
    public int ToUserId { get; set; }
    public string MessageBody { get; set; }
    public DateTime SendDate { get; set; }= DateTime.Now;
    public string Title { get; set; }
}
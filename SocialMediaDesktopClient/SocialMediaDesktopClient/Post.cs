using System;
using SocialMediaClient;




public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public PostStatus Status { get; set; }

    public override string ToString()
    {
        return $"Title: {Title}, Content: {Content}, Created On: {CreatedOn}";
    }
    public enum PostStatus
    {
        PENDING,
        PUBLISHED,
        REMOVED
    }

}


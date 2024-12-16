using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SocialMediaClient
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; } 
        public string Content { get; set; }
        public DateTime SentOn { get; set; }

        public override string ToString()
        {
            return $"To User: {ReceiverId}, Sent On: {SentOn}, Content: {Content}";
        }
    }
}


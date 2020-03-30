using System;

namespace ChatDemo.Models
{
    public class ChatMessage
    {
        public string Text { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public bool IsInbound { get; set; }
    }
}
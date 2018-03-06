using System;
using System.Collections.Generic;
using System.Security.Cryptography;



namespace SRChat
{
    [Serializable]
    public class MessageRecipient
    {
        
        
        public MessageRecipient()
        {
            chatRoomIds = new List<string>();
        }
        public string messageRecipientId { get; set; }
        public string messageRecipientName { get; set; }
        public string connectionId { get; set; }

        
        public List<string> chatRoomIds { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.App_Code
{
    [Serializable]
    /**
     * Classe que cuida da Mensagem
     */
    public class ChatMessage
    {
        public ChatMessage()
        {
        }
        /// <summary>
        /// Id da Mensagem
        /// </summary>
        public string chatMessageId { get; set; }
        /// <summary>
        /// Id da conversa
        /// </summary>
        public string conversationId { get; set; }
        /// <summary>
        /// Id do envio
        /// </summary>
        public string senderId { get; set; }
        /// <summary>
        /// Nome de quem envio a mensagem 
        /// </summary>
        public string senderName { get; set; }
        /// <summary>
        /// Texto da mensagem
        /// </summary>
        public string messageText { get; set; }
        /// <summary>
        /// Texto da mensagem formatado
        /// </summary>
        public string displayPrefix { get { return string.Format("[{0}] {1}:", timestamp.ToShortTimeString(), senderName); } }
        /// <summary>
        /// Data da mensagem
        /// </summary>
        public DateTime timestamp { get; set; }
    }
}

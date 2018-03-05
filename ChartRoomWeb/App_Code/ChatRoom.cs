using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.App_Code
{
    /// <summary>
    /// Classe que trata de uma sala de bate-papo
    /// </summary>
    [Serializable]
    public class ChatRoom
    {
        /// <summary>
        /// Id da sala
        /// </summary>
        public string chatRoomId { get; set; }
        /// <summary>
        /// Informa quem inciailizou a sala
        /// </summary>
        public string chatRoomInitiatedBy { get; set; }
        /// <summary>
        /// Informa para quem a sala foi inicializada
        /// </summary>
        public string chatRoomInitiatedTo { get; set; }
        /// <summary>
        /// Lista as mensagens recebidas
        /// </summary>
        public List<MessageRecipient> messageRecipients { get; set; }
        /// <summary>
        /// Incializa uma classe ChatRoom
        /// </summary>
        public ChatRoom()
        {
            chatRoomId = Guid.NewGuid().ToString();
            messageRecipients = new List<MessageRecipient>();
        }
    }
}

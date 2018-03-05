using System;
using System.Collections.Generic;

namespace ChatRoomCore.App_Code
{
    /// <summary>
    /// Classe que cuida de uma Mensagem Recebida
    /// </summary>
    [Serializable]
    public class MessageRecipient
    {
        /// <summary>
        /// Cria um objeto Mensagem Recebida
        /// </summary>
        public MessageRecipient()
        {
            chatRoomIds = new List<string>();
        }
        /// <summary>
        /// Id da mensagem
        /// </summary>
        public string messageRecipientId { get; set; }
        /// <summary>
        /// Nome da mensagem 
        /// </summary>
        public string messageRecipientName { get; set; }
        /// <summary>
        /// Id da conexão
        /// </summary>
        public string connectionId { get; set; }
        /// <summary>
        /// Id´s das salas 
        /// </summary>
        public List<string> chatRoomIds { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.App_Code
{
    /// <summary>
    /// Classe cuida das mensagens recebidas 
    /// </summary>
    [Serializable]
    public class OnlineContacts
    {
        /// <summary>
        /// Lista de mensagens recebidas
        /// </summary>
        public List<MessageRecipient> messageRecipients { get; set; }
        /// <summary>
        /// Cria um objeto do tipo Online contatso
        /// </summary>
        public OnlineContacts()
        {
            messageRecipients = new List<MessageRecipient>();
        }
    }
}

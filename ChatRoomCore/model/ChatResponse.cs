using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.model
{
    /// <summary>
    /// Classe de mensagens de resposta
    /// </summary>
    [Serializable]
    public class ChatResponse
    {
        /// <summary>
        /// Lista de usuários
        /// </summary>
        public List<ChatUser> Users { get; set; }
        /// <summary>
        /// Lista de mensagens 
        /// </summary>
        public List<ChatMessagem> Messagems { get; set; }
        /// <summary>
        /// Id da última mensagem
        /// </summary>
        public int LastMessageId { get; set; }

        /// <summary>
        /// Cria um chatResponse limpo
        /// </summary>
        public ChatResponse()
        {
            Users = new List<ChatUser>();
            Messagems = new List<ChatMessagem>();
        }
        /// <summary>
        /// Cira um chatresponse a partir dos paramentros repassados
        /// </summary>
        /// <param name="users">Lista de usuários</param>
        /// <param name="messagems">Lista de Mensagens</param>
        public ChatResponse(List<ChatUser> users, List<ChatMessagem> messagems)
        {
            Users = users;
            Messagems = messagems;
        }
        /// <summary>
        /// Cira um ChatResponse a partir dos paramentros repassados, com uma lista de mensagens limpa
        /// </summary>
        /// <param name="users">Lista de usuários</param>
        public ChatResponse(List<ChatUser> users)
        {
            Users = users;
            Messagems = new List<ChatMessagem>();
        }

        /// <summary>
        /// Cria um chatResponse a partir dos paramentros repassados, com uma lista de usuários limpa
        /// </summary>
        /// <param name="messagems">Lista de usuários</param>
        public ChatResponse(List<ChatMessagem> messagems)
        {
            Users = new List<ChatUser>();
            Messagems = messagems;
        }

    }
}

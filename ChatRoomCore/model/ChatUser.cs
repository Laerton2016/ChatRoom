using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChatRoomCore.model
{
    /// <summary>
    /// Classe encarregada dos dados de um usuário de chat
    /// </summary>
    [Serializable]
    public class ChatUser
    {
        /// <summary>
        /// Última atividade do usuário
        /// </summary>
        [XmlIgnore]
        public DateTime LastActividade { get; set; }
        /// <summary>
        /// Id do usuário
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Nome do usuário
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// Cria um usuário do chat com os paramentros repassados
        /// </summary>
        /// <param name="lastActividade">Última atividade do usuário</param>
        /// <param name="userId">Id do usuário</param>
        /// <param name="userName">Nome do usuário</param>
        public ChatUser(DateTime lastActividade, int userId, string userName)
        {
            LastActividade = lastActividade;
            UserId = userId;
            UserName = userName;
        }
        /// <summary>
        /// Cria um usuário de chat com os dados em branco
        /// </summary>
        public ChatUser()
        {
        }

        /// <summary>
        /// E-mail do usuário
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; }
    }
}

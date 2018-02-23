using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.model
{
    /// <summary>
    /// Classe que gera uma mensagem a ser publicada em uma sala de mensagem
    /// </summary>
    [Serializable]
    public class ChatMessagem
    {
        /// <summary>
        /// Conteúdo da mensagem
        /// </summary>
        public String MessageBody { get; set; }
        /// <summary>
        /// Nome do usuário da mensagem
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        /// Data da publicação da mensagem
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Id de identificação da mensagem
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Constroe uma mensagem com os dados repassados como parametro
        /// </summary>
        /// <param name="messageBody">Conteúdo da mensagem</param>
        /// <param name="userName">Nome do usuário que enviou a mensagem</param>
        /// <param name="date">Data de sua publicação</param>
        /// <param name="id">Id da mensagem</param>
        public ChatMessagem(int id, string messageBody, string userName, DateTime date)
        {
            MessageBody = messageBody;
            UserName = userName;
            Date = date;
            Id = id;
        }
        /// <summary>
        /// Construtor padrão de uma mensagem
        /// </summary>
        public ChatMessagem()
        {
        }
    }
}

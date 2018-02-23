using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.model
{
    /// <summary>
    /// Classe de Usuário que partcipa de um chat 
    /// </summary>
    public class ChatUser:  IDisposable
    {
        /// <summary>
        /// Id do usuário
        /// </summary>
        public UInt32 UserId { get; set; }
        /// <summary>
        /// Nome do Usuário
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        /// Status de ativo do usuário
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Última vizualisação do úsuário
        /// </summary>
        public DateTime LastSeen { get; set; }
        /// <summary>
        /// Int da última mensagem recebida.
        /// </summary>
        public int LastMenssageRecived { get; set; }
        /// <summary>
        /// Controi um usuário 
        /// </summary>
        /// <param name="userId">Id do usuário </param>
        /// <param name="userName">Nome do usuário</param>
        public ChatUser(UInt32 userId, string userName)
        {
            this.UserId = userId;
            this.IsActive = false;
            this.LastSeen = DateTime.MinValue;
            this.UserName = userName;
            this.LastMenssageRecived = 0;
        }
        /// <summary>
        /// Desfaz um usuário
        /// </summary>
        public void Dispose()
        {
            this.UserId = 0;
            this.IsActive = false;
            this.LastSeen = DateTime.MinValue;
            this.UserName = "";
            this.LastMenssageRecived = 0;
        }
    }
}

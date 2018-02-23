using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.model
{
    /// <summary>
    /// Classe encarregada dos dados de um Chat
    /// </summary>
    [Serializable]
    public class ChatRoom
    {
        private Dictionary<int, ChatUser> _users;
        /// <summary>
        /// Usuário ativos na sala de bate papo
        /// </summary>
        public Dictionary<int, ChatUser> Users
        {
            get { return _users; }
        }
        /// <summary>
        /// A última vez que o sistema validou um usuário ativo
        /// Para evitar todo o processo de validação se os usuários forem válidos mais de uma vez.
        /// </summary>
        public DateTime LastUserChange { get; set; }

        /// <summary>
        /// Método encarregado de checar se há usuários inatívos, se verdadeiros ele remove da lista.
        /// </summary>
        /// <param name="maxInterval">Tempo limite máximo</param>
        public void ValidateUsers(TimeSpan maxInterval)
        {
            List<int> toDelete = new List<int>();
            foreach (System.Collections.Generic.KeyValuePair<int, ChatUser> keyValue
            in Users)
            {
                if (DateTime.Now.Subtract(keyValue.Value.LastActividade)> maxInterval) toDelete.Add(keyValue.Key);
            }

            foreach (int userId in toDelete)
            {
                Users.Remove(userId);
            }
            LastUserChange = DateTime.Now;
        }

        /// <summary>
        /// Adiciona um novo usuário a sala
        /// </summary>
        /// <param name="user">Usuário que entra na sala</param>
        public void EnterRoom(ChatUser user)
        {
            if (!this.Users.ContainsKey(user.UserId))
            {
                Users.Add(user.UserId, user);
            }
            else
            {
                Users[user.UserId].LastActividade = DateTime.Now;
            }
        }

        /// <summary>
        /// Remove o usuário da sala
        /// </summary>
        /// <param name="userId"></param>
        public void LeaveRoom(int userId)
        {
            Users.Remove(userId);
            LastUserChange = DateTime.Now;
            
        }

        /// <summary>
        /// Controe uma sala de bate papo
        /// </summary>
        public ChatRoom()
        {
            _users = new Dictionary<int, ChatUser>();
            LastUserChange = DateTime.Now;
        }
    }
}

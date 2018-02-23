using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.model
{
    /// <summary>
    /// Sala de bate papo
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Id da Sala
        /// </summary>
        public int Id { get; set; }
        private ArrayList _lklMesagens = new ArrayList();
        private List<ChatUser> _lklUsers = new List<ChatUser>();
        /// <summary>
        /// Entra na sala bate papo
        /// </summary>
        /// <param name="user">Usuário</param>
        public void Enter(ChatUser user)
        {
            _lklUsers.Add(user);
            _lklMesagens.Add(new Message(user.UserName, MsgType.Join));
        }
        /// <summary>
        /// Sai da sala de bate papo
        /// </summary>
        /// <param name="user">Usuário</param>
        public void Exit(ChatUser user)
        {
            _lklUsers.Remove(user);
            _lklMesagens.Add(new Message(user.UserName, MsgType.Left));
        }
        /// <summary>
        /// Publica mensagem
        /// </summary>
        /// <param name="msg">Mensagem a ser publicada</param>
        public void PublicMessage(Message msg)
        {
            _lklMesagens.Add(msg);
        }
        /// <summary>
        /// Lista todas as mensagens
        /// </summary>
        /// <returns></returns>
        public ArrayList GelAllMessages()
        {
            return _lklMesagens;
        }
        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns></returns>
        public List<ChatUser> GetAllUser()
        {
            return _lklUsers;
        }

        public Message LastMessage()
        {
            return (Message) _lklMesagens[_lklMesagens.Count - 1];
        }
    }
}

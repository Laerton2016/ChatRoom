using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.model
{
    /// <summary>
    /// Classe de Mensagem
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Usuário da mensagem
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Conteúdo da mensagem
        /// </summary>
        public string MSG { get; set; }
        /// <summary>
        /// Typo de mensagem
        /// </summary>
        public MsgType Type { get; set; }
        /// <summary>
        /// Cria uma mensagem
        /// </summary>
        /// <param name="user">Usuário da mensagem</param>
        /// <param name="msg">Conteúdo da mensagem</param>
        /// <param name="type">Tipo da mensagem</param>
        public Message(string user, string msg, MsgType type)
        {
            User = user;
            this.MSG = msg;
            this.Type = type;
        }

        public override string ToString()
        {
            switch (this.Type)
            {
                case MsgType.Msg:
                        return this.User + " diz: " + this.MSG;
                case MsgType.Join:
                    return this.User + " juntou-se a sala";
                case MsgType.Left:
                    return this.User + " você deixou a sala";
            }

            return "";
        }
        /// <summary>
        /// Controi um amensagem em branco
        /// </summary>
        /// <param name="_user">Usuário da mensagem</param>
        /// <param name="_type">Tipo de mensagem</param>
        public Message(String _user, MsgType _type): this(_user, "", _type) { }

        /// <summary>
        /// Cria uma mensagem em branco sem usuário
        /// </summary>
        /// <param name="_type">Tipo da mensagem</param>
        public Message(MsgType _type) : this("", "", _type)
        {
        }
    }
    /// <summary>
    /// Enum de tipo de mensagem
    /// </summary>
    public enum MsgType { Msg, Start, Join, Left}
}

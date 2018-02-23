using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomCore.model
{
    public class ChatRoomCollection : Dictionary<int, ChatRoom>
    {
        public ChatRoom Get(int key)
        {
            if (this[key] == null) // se não encontrar a sala com o id deve ser criada
            {
                this[key] = new ChatRoom();
            }

            return this[key];
        }

        /// <summary>
        /// Gets  de chatroom baseado na chave repassada, caso não exista criará uma nova chatroom com a chave
        /// </summary>
        /// <param name="key">Chave</param>
        /// <returns>Sala</returns>
        public new ChatRoom this[int key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    base[key] = new ChatRoom();
                }

                return base[key];
            }
            set { base[key] = value; }
        }




    }
}

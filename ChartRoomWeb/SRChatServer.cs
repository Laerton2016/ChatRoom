using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using ChatRoomCore.App_Code;
using SignalR.Hubs;


namespace ChartRoomWeb
{
    /// <summary>
    /// Classe servidora do serviço de chat
    /// </summary>
    [HubName("sRChatServer")]
    public class SRChatServer : Hub
    {
        #region Private Variables
        /// <summary>
        /// Lista de usuarios no chat
        /// </summary>
        private static readonly ConcurrentDictionary<string, MessageRecipient> _chatUsers = new ConcurrentDictionary<string, MessageRecipient>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// Lista de sala de chat
        /// </summary>
        private static readonly ConcurrentDictionary<string, ChatRoom> _chatRooms = new ConcurrentDictionary<string, ChatRoom>(StringComparer.OrdinalIgnoreCase);
        #endregion

        #region Public Methods
        /// <summary>
        /// Conectar usuário 
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        /// <param name="userName">Nome do usuário</param>
        /// <returns></returns>
        public bool Connect(string userId, string userName)
        {
            try
            {
                
                if (string.IsNullOrEmpty(userId) | string.IsNullOrEmpty(userName))
                {
                    return false;
                }
                if (GetChatUserByUserId(userId) == null)
                {
                    AddUser(userId, userName);
                }
                else
                {
                    ModifyUser(userId, userName);
                }
                SendOnlineContacts();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problemas para conectar com o servidor do chat!");
            }
        }
        /// <summary>
        /// Desconecta do servidor
        /// </summary>
        /// <returns>Tipo Task</returns>
        public override Task Disconnect()
        {
            try
            {
                DeleteUser(Context.ConnectionId);
                return null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problema para desconectar do servidor");
            }
        }
        /// <summary>
        /// Incializa um chat 
        /// </summary>
        /// <param name="fromUserId">Id do usuário de origem</param>
        /// <param name="fromUserName">Nome do usuário de origem</param>
        /// <param name="toUserId">Id do usuário de destino</param>
        /// <param name="toUserName">Nome do usuário de destino</param>
        /// <returns>Status da conexão, True para conectado e False para não conectado</returns>
        public bool InitiateChat(string fromUserId, string fromUserName, string toUserId, string toUserName)
        {
            try
            {
                if (string.IsNullOrEmpty(fromUserId) || string.IsNullOrEmpty(fromUserName) || string.IsNullOrEmpty(toUserId) || string.IsNullOrEmpty(toUserName))
                {
                    return false;
                }

                var fromUser = GetChatUserByUserId(fromUserId);
                var toUser = GetChatUserByUserId(toUserId);

                if (fromUser != null && toUser != null)
                {
                    if (!CheckIfRoomExists(fromUser, toUser))
                    {
                        
                        ChatRoom chatRoom = new ChatRoom();
                        chatRoom.chatRoomInitiatedBy = fromUser.messageRecipientId;
                        chatRoom.chatRoomInitiatedTo = toUser.messageRecipientId;

                        chatRoom.messageRecipients.Add(fromUser);
                        chatRoom.messageRecipients.Add(toUser);

                        
                        //criando e salvando uma mensagem de inicialização de chat pelo id de origem
                        ChatMessage chatMessage = new ChatMessage();
                        chatMessage.messageText = "Chat Ininiciado";
                        chatMessage.senderId = fromUser.messageRecipientId;
                        chatMessage.senderName = fromUser.messageRecipientName;

                        fromUser.chatRoomIds.Add(chatRoom.chatRoomId);
                        toUser.chatRoomIds.Add(chatRoom.chatRoomId);

                        
                        //Cria um SignalR Group para esta sala de chat e adiciona os usuários conectados a ela.
                        Groups.Add(fromUser.connectionId, chatRoom.chatRoomId);
                        Groups.Add(toUser.connectionId, chatRoom.chatRoomId);

                        
                        //Adiciona a sala de chat a lista 
                        if (_chatRooms.TryAdd(chatRoom.chatRoomId, chatRoom))
                        {
                            
                            //Client UI para gerenciar essa sala
                            Clients[fromUser.connectionId].initiateChatUI(chatRoom);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problema para iniciar o chat!");
            }
        }
        /// <summary>
        /// Encerra o chat
        /// </summary>
        /// <param name="chatMessage">Mensagem para o encerramento</param>
        /// <returns>Booleano de confirmação</returns>
        public bool EndChat(ChatMessage chatMessage)
        {
            try
            {
                ChatRoom chatRoom;
                if (_chatRooms.TryGetValue(chatMessage.conversationId, out chatRoom))
                {
                    if (_chatRooms[chatRoom.chatRoomId].chatRoomInitiatedBy == chatMessage.senderId)
                    {
                        chatMessage.messageText = string.Format("{0} saiu da sala. Chat encerrado!", chatMessage.senderName);
                        if (_chatRooms.TryRemove(chatRoom.chatRoomId, out chatRoom))
                        {
                            Clients[chatRoom.chatRoomId].receiveEndChatMessage(chatMessage);
                            foreach (MessageRecipient messageReceipient in chatRoom.messageRecipients)
                            {
                                if (messageReceipient.chatRoomIds.Contains(chatRoom.chatRoomId))
                                {
                                    messageReceipient.chatRoomIds.Remove(chatRoom.chatRoomId);
                                    Groups.Remove(messageReceipient.connectionId, chatRoom.chatRoomId);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageRecipient messageRecipient = GetChatUserByUserId(chatMessage.senderId);
                        if (messageRecipient != null && messageRecipient.chatRoomIds.Contains(chatRoom.chatRoomId))
                        {
                            chatRoom.messageRecipients.Remove(messageRecipient);
                            messageRecipient.chatRoomIds.Remove(chatRoom.chatRoomId);
                            if (chatRoom.messageRecipients.Count < 2)
                            {
                                chatMessage.messageText = string.Format("{0} saiu da sala. Chat encerrado!", chatMessage.senderName);
                                if (_chatRooms.TryRemove(chatRoom.chatRoomId, out chatRoom))
                                {
                                    Clients[chatRoom.chatRoomId].receiveEndChatMessage(chatMessage);
                                    foreach (MessageRecipient messageReceipient in chatRoom.messageRecipients)
                                    {
                                        if (messageReceipient.chatRoomIds.Contains(chatRoom.chatRoomId))
                                        {
                                            messageReceipient.chatRoomIds.Remove(chatRoom.chatRoomId);
                                            Groups.Remove(messageReceipient.connectionId, chatRoom.chatRoomId);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                chatMessage.messageText = string.Format("{0} saiu da sala.", chatMessage.senderName);
                                Groups.Remove(messageRecipient.connectionId, chatRoom.chatRoomId);
                                Clients[messageRecipient.connectionId].receiveEndChatMessage(chatMessage);
                                Clients[chatRoom.chatRoomId].receiveLeftChatMessage(chatMessage);
                                Clients[chatRoom.chatRoomId].updateChatUI(chatRoom);
                            }
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Problema para sair da sala!");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problemas para sair da sala!");
            }
        }
        /// <summary>
        /// Envia uma mensagem
        /// </summary>
        /// <param name="chatMessage">Mensagem a ser envaida</param>
        /// <returns>Boleano de confirmação</returns>
        public bool SendChatMessage(ChatMessage chatMessage)
        {
            try
            {
                ChatRoom chatRoom;
                if (_chatRooms.TryGetValue(chatMessage.conversationId, out chatRoom))
                {
                    chatMessage.chatMessageId = Guid.NewGuid().ToString();
                    chatMessage.timestamp = DateTime.Now;
                    Clients[chatMessage.conversationId].receiveChatMessage(chatMessage, chatRoom);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException("Problema para enviar a mensagem!");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problema para enviar a mensagem!");
            }
        }/// <summary>
        /// Envia para os conatos on-line
        /// </summary>
        /// <returns>Booleano de confirmação</returns>
        private bool SendOnlineContacts()
        {
            try
            {
                OnlineContacts onlineContacts = new OnlineContacts();
                foreach (var item in _chatUsers)
                {
                    onlineContacts.messageRecipients.Add(item.Value);
                }
                Clients.onGetOnlineContacts(onlineContacts);
                return false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Problema para enviar para os contatos!");
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Vrifica se os usuários existem na sala
        /// </summary>
        /// <param name="fromUser">Usuário de origem</param>
        /// <param name="toUser">Usuário de destino</param>
        /// <returns>Booleano de confirmação</returns>
        private Boolean CheckIfRoomExists(MessageRecipient fromUser, MessageRecipient toUser)
        {
            foreach (string chatRoomId in fromUser.chatRoomIds)
            {
                Int32 count = (from mr in _chatRooms[chatRoomId].messageRecipients
                               where mr.messageRecipientId == toUser.messageRecipientId
                               select mr).Count();
                if (count > 0)
                {
                    return true;
                }
            }
            foreach (string chatRoomId in toUser.chatRoomIds)
            {
                Int32 count = (from mr in _chatRooms[chatRoomId].messageRecipients
                               where mr.messageRecipientId == fromUser.messageRecipientId
                               select mr).Count();
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Adiciona um usuário
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        /// <param name="userName">Nome do usuário</param>
        /// <returns></returns>
        private MessageRecipient AddUser(string userId, string userName)
        {
            var user = new MessageRecipient();
            user.messageRecipientId = userId;
            user.messageRecipientName = userName;
            user.connectionId = Context.ConnectionId;
            _chatUsers[userId] = user;
            return user;
        }

        /// <summary>
        /// Modifica dados do usuário
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        /// <param name="userName">Nome do usuário</param>
        /// <returns></returns>
        private MessageRecipient ModifyUser(string userId, string userName)
        {
            var user = GetChatUserByUserId(userId);
            user.messageRecipientName = userName;
            user.connectionId = Context.ConnectionId;
            _chatUsers[userId] = user;
            return user;
        }
        /// <summary>
        /// Deleta um usuário
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        /// <param name="userName">Nome do usuário</param>
        /// <returns></returns>
        private Boolean DeleteUser(string userId, string userName)
        {
            var user = GetChatUserByUserId(userId);
            if (user != null && _chatUsers.ContainsKey(user.messageRecipientId))
            {
                MessageRecipient messageRecipient;
                return _chatUsers.TryRemove(user.messageRecipientId, out messageRecipient);
            }
            return false;
        }
        /// <summary>
        /// Deleta um usuário
        /// </summary>
        /// <param name="connectionId">Id da conexão</param>
        /// <returns></returns>
        private Boolean DeleteUser(string connectionId)
        {
            var returnValue = false;
            var user = GetChatUserByConnectionId(connectionId);
            if (user != null && _chatUsers.ContainsKey(user.messageRecipientId))
            {
                MessageRecipient messageRecipient;
                returnValue = _chatUsers.TryRemove(user.messageRecipientId, out messageRecipient);

                
                //Remove de todos os grupos e salas
                foreach (string chatRoomId in messageRecipient.chatRoomIds)
                {
                    _chatRooms[chatRoomId].messageRecipients.Remove(messageRecipient);

                    Groups.Remove(messageRecipient.connectionId, chatRoomId);

                    
                    //notifica que o usuário saiu da sala
                    ChatMessage chatMessage = new ChatMessage();
                    chatMessage.conversationId = chatRoomId;
                    chatMessage.senderId = messageRecipient.messageRecipientId;
                    chatMessage.senderName = messageRecipient.messageRecipientName;
                    if (_chatRooms[chatRoomId].chatRoomInitiatedBy == messageRecipient.messageRecipientId)
                    {
                        chatMessage.messageText = string.Format("{0} saiu da sala. Chat encerrado!", messageRecipient.messageRecipientName);
                        ChatRoom chatRoom;

                        if (_chatRooms.TryRemove(chatRoomId, out chatRoom))
                        {
                            foreach (MessageRecipient messageReceipient in chatRoom.messageRecipients)
                            {
                                if (messageReceipient.chatRoomIds.Contains(chatRoomId))
                                {
                                    messageReceipient.chatRoomIds.Remove(chatRoomId);
                                }
                            }
                            Clients[chatRoomId].receiveEndChatMessage(chatMessage);
                        }
                    }
                    else
                    {
                        if (_chatRooms[chatRoomId].messageRecipients.Count() < 2)
                        {
                            chatMessage.messageText = string.Format("{0} saiu da sala. Chat encerrado!", messageRecipient.messageRecipientName);
                            ChatRoom chatRoom;
                            if (_chatRooms.TryRemove(chatRoomId, out chatRoom))
                            {
                                foreach (MessageRecipient messageReceipient in chatRoom.messageRecipients)
                                {
                                    if (messageReceipient.chatRoomIds.Contains(chatRoomId))
                                    {
                                        messageReceipient.chatRoomIds.Remove(chatRoomId);
                                    }
                                }
                                Clients[chatRoomId].receiveEndChatMessage(chatMessage);
                            }
                        }
                        else
                        {
                            chatMessage.messageText = string.Format("{0} saiu da sala.", messageRecipient.messageRecipientName);
                            Clients[chatRoomId].receiveLeftChatMessage(chatMessage);
                        }
                    }
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Retorna Usuário a partir de seu id
        /// </summary>
        /// <param name="userId">Id do usuário</param>
        /// <returns></returns>
        private MessageRecipient GetChatUserByUserId(string userId)
        {
            return _chatUsers.Values.FirstOrDefault(u => u.messageRecipientId == userId);
        }
        /// <summary>
        /// Retorna um usuário a partir de um id de conexão
        /// </summary>
        /// <param name="connectionId">id da conexão</param>
        /// <returns></returns>
        private MessageRecipient GetChatUserByConnectionId(string connectionId)
        {
            return _chatUsers.Values.FirstOrDefault(u => u.connectionId == connectionId);
        }

        #endregion
    }
}
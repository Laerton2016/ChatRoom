using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ChatRoomCore.Data;
using  ChatRoomCore.model;

namespace ChatRoomWeb
{
    public class ChatManager
    {
        private ApplicationWrapper _application;

        public ApplicationWrapper CurrentApplication
        {
            get
            {
                if (_application == null)
                {
                    _application = new ApplicationWrapper(HttpContext.Current.Application);
                }
                return _application;
            }

            set { _application = value; }
        }

        private SessionWrapper _session;

        public SessionWrapper CurrentSession
        {
            get
            {
                if (_session == null) _session = new SessionWrapper(HttpContext.Current.Session);
                return _session;
            }
            set { _session = value; }
        }

        

        public TimeSpan ChatUsersMaxInterval
        {
            get { return new TimeSpan(0, 0, 5); }
        }

        private ChatRoomCollection ChatRooms
        {
            get { return CurrentApplication.ChatRooms; }
        }

        private ChatRoom CurrentRoom
        {
            get
            {
                if (RoomId == 0)
                {
                    throw new ArgumentException("Não foi encontrada uma sala com esse id. Sessão perdida.");
                }

                return ChatRooms[RoomId];
            }
        }

        public DateTime RoomUserDate
        {
            get
            {
                return CurrentSession.RoomUserDate;
            }

            set { CurrentSession.RoomUserDate = value; }
        }

        private ChatUser CurrentUser
        {
            get { return CurrentRoom.Users[CurrentSession.User.UserId]; }
            set { CurrentRoom.Users[CurrentSession.User.UserId] = value; }
        }

        private void SetLastActivity()
        {
            CurrentUser.LastActividade = DateTime.Now;
        }

        private int RoomId
        {
            get { return CurrentSession.RoomId; }
            set { CurrentSession.RoomId = value; }
        }

        public int? LastMassageId
        {
            get { return CurrentSession.LastMessageId; }
            set { CurrentSession.LastMessageId = value; }
        }

        public ChatResponse CheckUsers()
        {
            ChatResponse response = new ChatResponse();
            ChatRoom room = new ChatRoom();
            if (room.LastUserChange > this.RoomUserDate)
            {
                foreach (KeyValuePair<int, ChatUser> keyValuePair in room.Users)
                {
                    response.Users.Add(keyValuePair.Value);   
                }

                this.RoomUserDate = room.LastUserChange;
            }

            return response;
        }

        private void CheckUsers(ChatResponse response)
        {
            response.Users = CheckUsers().Users;
        }

        public int EnterRoom(int roomId)
        {
            RoomId = roomId;
            CurrentRoom.ValidateUsers(ChatUsersMaxInterval);
            //CorrentRoom.EnterRoom(CurrentSession.User.UserId, CurrentSession.User.UserName)
            if (LastMassageId == null)
            {
                ChatDataAccess da = new ChatDataAccess();
                LastMassageId = da.GetLastMessage();
            }

            return (int) LastMassageId;
        }

        public ChatResponse CheckMessages(int lastMessagem)
        {
            ChatResponse response  = new ChatResponse();
            SetLastActivity();
            ChatDataAccess da = new ChatDataAccess();
            List<ChatMessagem> messagems = da.GetRoomMessages(lastMessagem, RoomId);
            if (messagems.Count > 0)
            {
                response.Messagems.AddRange(messagems);
                response.LastMessageId = messagems[messagems.Count - 1].Id;
                CheckUsers(response);
            }

            return response;
        }

        public ChatResponse SendMessage(string message, int lastMessageId)
        {
            SetLastActivity();
            ChatDataAccess da = new ChatDataAccess();
            da.MessageInsert(RoomId, message, DateTime.Now, CurrentSession.User.UserId, false);
            CurrentRoom.ValidateUsers(ChatUsersMaxInterval);
            return CheckMessages(lastMessageId);
        }
    }

}

using System;
using System.Web.SessionState;
using ChatRoomCore.model;

namespace ChatRoomWeb

{
    public class SessionWrapper
    {
        public SessionWrapper(HttpSessionState session)
        {
            this.Session = session;

        }

        protected virtual HttpSessionState Session
        {
            get;
            set;
        }

        public SessionWrapper()
        {
        }

        #region User

        private const string keyUser = "User";

        public virtual ChatUser User //Mudança realizada de SiteUser para ChatUser para não criar dois tipos usuários
        {
            get { return (ChatUser) Session[keyUser]; }
            set { Session[keyUser] = value; }
        }

        #endregion

        #region Chat

        #region RoomUsersDate

        private const string keyRoomUsersDate = "GotRoomUsers";

        public virtual DateTime RoomUserDate
        {
            get
            {
                if (Session[keyRoomUsersDate] == null)
                {
                    Session[keyRoomUsersDate] = DateTime.MinValue;
                }

                return (DateTime) Session[keyRoomUsersDate];
            }

            set { Session[keyRoomUsersDate] = value; }
        }

        #endregion
        #region RoomId

        private const string keyRoomId = "RoomId";

        public virtual int RoomId
        {
            get { return (int) Session[keyRoomId]; }
            set { Session[keyRoomId] = value; }
        }

        #endregion
        #region LastMessageId

        private const string keyLastMessageId = "LastMessaId:";

        public virtual int? LastMessageId
        {
            get
            {
                return (int?) Session[keyLastMessageId + RoomId.ToString()];

            }
            set { Session[keyLastMessageId + RoomId.ToString()] = value; }
        }
        #endregion
        #endregion
    }
}
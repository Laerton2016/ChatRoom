using System.Web;
using ChatRoomCore.model;

namespace ChatRoomWeb
{
    public class ApplicationWrapper
    {
        private HttpApplicationState application;

        public ApplicationWrapper(HttpApplicationState application)
        {
            this.application = application;
        }

        public virtual HttpApplicationState Application { get; set; }

        public ApplicationWrapper()
        {
        }

        private const string keyChatRooms = "ChatRooms";

        public virtual ChatRoomCollection ChatRooms
        {
            get {
                if (Application[keyChatRooms] == null)
                {
                    Application[keyChatRooms] = new ChatRoomCollection();

                }

                return (ChatRoomCollection) Application[keyChatRooms];
            }
        }
    }
}
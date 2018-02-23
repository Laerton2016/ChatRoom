using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomCore.model;
using MySql.Data.MySqlClient;

namespace ChatRoomCore.Data
{
    public class ChatDataAccess
    {
        /// <summary>
        /// Método de insere uma mensagem no banco de dados.
        /// </summary>
        /// <param name="roomId">Id da sala </param>
        /// <param name="menssage">Mensagem a ser publicada</param>
        /// <param name="date">Data da mensagem</param>
        /// <param name="userId">Id do usuário da mensagem</param>
        /// <param name="isSystem">Se a mensagem é gerada pelo sistema</param>
        public void MessageInsert(int roomId, string menssage, DateTime date, int userId, bool isSystem)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SPChatMessagesInsert";
            command.Parameters.Add(new MySqlParameter("@RoomId", roomId));
            command.Parameters.Add(new MySqlParameter("@MessageBody", menssage));
            command.Parameters.Add(new MySqlParameter("@MessageDate", date));
            command.Parameters.Add(new MySqlParameter("@UserId", userId));
            command.Parameters.Add(new MySqlParameter("@IsSystem", isSystem));
            try
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command.Connection.Close();
            }
        }
        /// <summary>
        /// Método retorna todas as mensagens de um sala de bate-papo
        /// </summary>
        /// <param name="lastMessageId">Id da última mensagem</param>
        /// <param name="roomId">Id da sala</param>
        /// <returns></returns>
        public List<ChatMessagem> GetRoomMessages(int lastMessageId, int roomId)
        {
            List<ChatMessagem> messagems = new List<ChatMessagem>();
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand()
            {
                
                Connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString),
                CommandType = CommandType.StoredProcedure,
                CommandText = "SPChatMessagesGet",
                Parameters =
                    {
                        new MySqlParameter("@LastMessageId", lastMessageId),
                        new MySqlParameter("@RoomId", roomId)
                    }
    
            };
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                messagems.Add(new ChatMessagem(Convert.ToInt32(dr["MessageId"]), dr["MessageBody"].ToString(), dr["UserName"].ToString(), Convert.ToDateTime(dr["MessageDate"])));
            }

            return messagems;

        }

        public int GetLastMessage()
        {
            DataTable dt = new DataTable();
            MySqlCommand command = new MySqlCommand()
            {
                Connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString),
                CommandType =  CommandType.StoredProcedure,
                CommandText = "SPChatMessagesGetLastMessage"
            };

            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(dt);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}
